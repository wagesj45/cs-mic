using System.Runtime.CompilerServices;
using System.Text;
using System.IO;
using GeneratedInterpreter = global::CSMic.Interpreter;

namespace CSMic
{
    /// <summary> The interpreter that parses user input at runtime into strongly typed .Net values. </summary>
    public class InputInterpreter
    {
        #region Members

        /// <summary> The numeric value store. </summary>
        private decimal numericValue = 0;
        /// <summary> The string value store. </summary>
        private string stringValue = string.Empty;
        /// <summary> The time taken for the last entry the parser interpreted. </summary>
        private TimeSpan lastExecutionTime = TimeSpan.Zero;

        #region Variable stores
        /// <summary> (Immutable) The numeric variables. </summary>
        private readonly Dictionary<string, decimal> numericVariables;
        /// <summary> (Immutable) The numeric array variables. </summary>
        private readonly Dictionary<string, decimal[]> numericArrayVariables;
        /// <summary> (Immutable) The expression variables. </summary>
        private readonly Dictionary<string, string> expressionVariables; 
        #endregion

        /// <summary> (Immutable) The function registry. </summary>
        private readonly Dictionary<string, ICodedFunction> functions;

        /// <summary>
        ///  (Immutable) Tracks expression variables currently being evaluated to prevent recursion.
        /// </summary>
        private readonly List<string> evaluationStack;
        /// <summary> Shared recursion tracker used across nested evaluations. </summary>
        private sealed class RecursionTracker { 
            /// <summary> The number of recursive trips detected. </summary>
            public int Hits; 
        }
        /// <summary> (Immutable) The recursion tracker. </summary>
        private readonly RecursionTracker recursion;

        #endregion

        #region Constructors

        /// <summary> Default constructor. </summary>
        public InputInterpreter()
        {
            numericVariables = new Dictionary<string, decimal>(StringComparer.Ordinal);
            numericArrayVariables = new Dictionary<string, decimal[]>(StringComparer.Ordinal);
            expressionVariables = new Dictionary<string, string>(StringComparer.Ordinal);
            functions = new Dictionary<string, ICodedFunction>(StringComparer.Ordinal);
            evaluationStack = new List<string>();
            recursion = new RecursionTracker();
        }

        /// <summary> Internal constructor to create a child interpreter that shares stores. </summary>
        /// <param name="parent"> The parent <see cref="InputInterpreter"/>. </param>
        internal InputInterpreter(InputInterpreter parent)
        {
            this.numericVariables = parent.numericVariables;
            this.numericArrayVariables = parent.numericArrayVariables;
            this.expressionVariables = parent.expressionVariables;
            this.functions = parent.functions;
            // Share the evaluation stack so recursion is tracked across nested parses
            this.evaluationStack = parent.evaluationStack;
            // Share recursion hit counter across nested evaluations
            this.recursion = parent.recursion;
        }

        #endregion

        #region Properties

        /// <summary> Gets the numeric value. </summary>
        /// <value> The numeric value. </value>
        public decimal NumericValue
        {
            get
            {
                return this.numericValue;
            }
        }

        /// <summary> Gets the string value. </summary>
        /// <value> The string value. </value>
        public string StringValue
        {
            get
            {
                return this.stringValue;
            }
        }

        /// <summary> Gets the last execution time. </summary>
        /// <value> The last execution time. </value>
        public TimeSpan LastExecutionTime
        {
            get
            {
                return this.lastExecutionTime;
            }
        }

        /// <summary> Gets the variables. </summary>
        /// <value> The encapsulated variables. </value>
        public IEnumerable<Variable> Variables
        {
            get
            {
                return this.numericVariables.Select(nv => new Variable(VariableType.Numeric, nv.Key, nv.Value))
                    .Concat(this.expressionVariables.Select(nv => new Variable(VariableType.Expression, nv.Key, nv.Value)))
                    .Concat(this.numericArrayVariables.Select(nv => new Variable(VariableType.NumericArray, nv.Key, nv.Value)))
                    .AsEnumerable();
            }
        }
        #endregion

        #region Output Plumbing

        /// <summary> Hydrates long-lived output variables for delayed access. </summary>
        /// <param name="numericValue"> The numeric value store. </param>
        /// <param name="stringValue"> The string value store. </param>
        internal void ProduceOutput(decimal numericValue, string stringValue)
        {
            this.numericValue = numericValue;
            this.stringValue = stringValue;
        }

        /// <summary> Hydrates long-lived output variables for delayed access. </summary>
        /// <param name="functionValue"> The function value to be converted. </param>
        internal void ProduceOutput(FunctionValue functionValue)
        {
            switch (functionValue.Type)
            {
                case FunctionValueType.Numeric:
                    decimal numericValue = Convert.ToDecimal(functionValue.Value);
                    ProduceOutput(numericValue, string.Empty);
                    break;
                case FunctionValueType.String:
                    if (functionValue.Value is string s)
                        ProduceOutput(0, s);
                    else
                        ProduceOutput(0, string.Empty);
                    break;
                case FunctionValueType.None:
                default:
                    ProduceOutput(0, string.Empty);
                    break;
            }
        }

        #endregion

        #region Variable APIs

        /// <summary> Attempts to get a numeric value from the variable store with a given name. </summary>
        /// <param name="name"> The name. </param>
        /// <param name="value"> [out] The value. </param>
        /// <returns> True if it succeeds, false if it fails. </returns>
        internal bool TryGetNumeric(string name, out decimal value)
            => numericVariables.TryGetValue(name, out value);

        /// <summary> Attempts to get a numeric array from the variable store with a given name. </summary>
        /// <param name="name"> The name. </param>
        /// <param name="values"> [out] The values. </param>
        /// <returns> True if it succeeds, false if it fails. </returns>
        internal bool TryGetNumericArray(string name, out decimal[] values)
            => numericArrayVariables.TryGetValue(name, out values!);

        /// <summary> Attempts to get an expression from the variable store with a given name. </summary>
        /// <param name="name"> The name. </param>
        /// <param name="expr"> [out] The expression. </param>
        /// <returns> True if it succeeds, false if it fails. </returns>
        internal bool TryGetExpression(string name, out string expr)
        {
            if (expressionVariables.TryGetValue(name, out expr!))
            {
                return true;
            }
            expr = string.Empty;
            return false;
        }

        /// <summary>
        ///  Recursion tracking helpers managed by the parser when evaluating expression variables.
        /// </summary>
        /// <param name="name"> The name of the variable to check. </param>
        /// <returns> True if currently evaluating, false if not. </returns>
        internal bool IsEvaluating(string name) => evaluationStack.Contains(name);

        /// <summary> Begins evaluating a named expression. </summary>
        /// <param name="name"> The name of the expression variable. </param>
        /// <returns> The current recurrsion depth. </returns>
        internal int BeginEvaluating(string name)
        {
            int depth = evaluationStack.Count;
            evaluationStack.Add(name);
            return depth;
        }

        /// <summary> Ends evaluating a named expression. </summary>
        /// <param name="depth"> The recurrsion depth. </param>
        internal void EndEvaluating(int depth)
        {
            while (evaluationStack.Count > depth)
            {
                evaluationStack.RemoveAt(evaluationStack.Count - 1);
            }
        }

        /// <summary> Recursion hit scoping across a single top-level expression evaluation. </summary>
        /// <returns> An int. </returns>
        internal int BeginRecursionScope()
        {
            return recursion.Hits;
        }

        /// <summary> Returns true if recursion occurred within this scope. </summary>
        /// <param name="startHits"> The initial recurrsive hit counter. </param>
        /// <returns> True if recurrsion occurred within this scope, false otherwise. </returns>
        internal bool EndRecursionScope(int startHits)
        {
            return recursion.Hits > startHits;
        }

        /// <summary> Mark a recursion hit. </summary>
        internal void MarkRecursionHit()
        {
            recursion.Hits++;
        }

        /// <summary> Assign a numeric value to a variable. </summary>
        /// <param name="name"> The name of the variable. </param>
        /// <param name="value"> [out] The value. </param>
        internal void AssignNumeric(string name, decimal value)
        {
            numericVariables[name] = value;
            // Remove conflicting bindings
            expressionVariables.Remove(name);
            numericArrayVariables.Remove(name);
        }

        /// <summary> Assign a numeric array set to a variable. </summary>
        /// <param name="name"> The name of the variable. </param>
        /// <param name="values"> [out] The array values. </param>
        internal void AssignNumericArray(string name, decimal[] values)
        {
            numericArrayVariables[name] = values;
            // Remove conflicting bindings
            numericVariables.Remove(name);
            expressionVariables.Remove(name);
        }

        /// <summary> Assign an expression to a variable. </summary>
        /// <param name="name"> The name of the variable. </param>
        /// <param name="expressionText"> The expression text. </param>
        internal void AssignExpression(string name, string expressionText)
        {
            expressionVariables[name] = expressionText;
            // Remove conflicting numeric value
            numericVariables.Remove(name);
            numericArrayVariables.Remove(name);
        }

        #endregion

        #region Expression Evaluation

        /// <summary> Evaluates an expression. </summary>
        /// <param name="expressionText"> The expression text. </param>
        /// <returns> The result of the expression. </returns>
        internal FunctionValue EvaluateExpression(string expressionText)
        {
            // Create a child interpreter sharing stores, so ProduceOutput doesn't affect parent state
            var child = new InputInterpreter(this);
            using var ms = new MemoryStream(Encoding.UTF8.GetBytes(expressionText));
            var scanner = new GeneratedInterpreter.Scanner(ms);
            var parser = new GeneratedInterpreter.Parser(scanner)
            {
                Interpreter = child
            };
            try
            {
                parser.Parse();
                return parser.Result;
            }
            finally
            {
                // no-op: evaluation stack is managed by the parser around calls
            }
        }

        /// <summary> Interpret input and return a numeric result. </summary>
        /// <param name="input"> The input text to parse. </param>
        /// <returns> A decimal value representing the immediate result. </returns>
        /// <remarks> This is the primary developer-facing API. </remarks>
        public decimal Interpret(string input)
        {
            DateTime start = DateTime.Now;
            try
            {
                using var ms = new MemoryStream(Encoding.UTF8.GetBytes(input ?? string.Empty));
                var scanner = new GeneratedInterpreter.Scanner(ms);
                var parser = new GeneratedInterpreter.Parser(scanner)
                {
                    Interpreter = this
                };
                parser.Parse();

                if (parser.errors.count > 0)
                {
                    // Soft error: set numeric to 0 and report a parse error message
                    ProduceOutput(0m, parser.errors.errMsgFormat);
                }
                else
                {
                    ProduceOutput(parser.Result);
                }
            }
            catch (Exception ex)
            {
                // Soft error: never throw, capture message
                ProduceOutput(0m, ex.Message);
            }
            finally
            {
                DateTime end = DateTime.Now;
                lastExecutionTime = end - start;
            }

            return this.numericValue;
        }

        #endregion

        #region Functions

        /// <summary> Registers a function described by <see cref="ICodedFunction"/> interface. </summary>
        /// <param name="function"> The function. </param>
        /// <seealso cref="FunctionArgument"/>
        /// <seealso cref="FunctionValueType"/>
        /// <seealso cref="FunctionValue"/>
        public void RegisterFunction(ICodedFunction function)
        {
            functions[function.Name] = function;
        }

        /// <summary> Executes a named function. </summary>
        /// <param name="name"> The name. </param>
        /// <param name="args"> A variable-length parameters list containing arguments. </param>
        /// <returns> The result of the function execution. </returns>
        /// <seealso cref="FunctionArgument"/>
        /// <seealso cref="FunctionValueType"/>
        /// <seealso cref="FunctionValue"/>
        /// <seealso cref="ICodedFunction"/>
        internal FunctionValue ExecuteFunction(string name, params FunctionArgument[] args)
        {
            if (functions.TryGetValue(name, out var fn))
            {
                try
                {
                    return fn.Execute(args);
                }
                catch (Exception ex)
                {
                    // Surface function errors to the interpreter's message channel
                    ProduceOutput(0m, ex.Message);
                    return new FunctionValue(FunctionValueType.None, null);
                }
            }
            return new FunctionValue(FunctionValueType.None, null);
        }

        #endregion
    }
}
