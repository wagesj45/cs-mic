using System.Runtime.CompilerServices;
using System.Text;
using System.IO;

namespace CSMic
{
    public class InputInterpreter
    {
        #region Members

        private decimal numericValue = 0;
        private string stringValue = string.Empty;
        private TimeSpan lastExecutionTime = TimeSpan.Zero;

        // Variable stores
        private readonly Dictionary<string, decimal> numericVariables;
        private readonly Dictionary<string, decimal[]> numericArrayVariables;
        private readonly Dictionary<string, string> expressionVariables;

        // Function registry
        private readonly Dictionary<string, ICodedFunction> functions;

        // Tracks expression variables currently being evaluated to prevent recursion
        private readonly List<string> evaluationStack;
        // Shared recursion tracker across nested evaluations
        private sealed class RecursionTracker { public int Hits; }
        private readonly RecursionTracker recursion;

        #endregion

        #region Constructors

        public InputInterpreter()
        {
            numericVariables = new Dictionary<string, decimal>(StringComparer.Ordinal);
            numericArrayVariables = new Dictionary<string, decimal[]>(StringComparer.Ordinal);
            expressionVariables = new Dictionary<string, string>(StringComparer.Ordinal);
            functions = new Dictionary<string, ICodedFunction>(StringComparer.Ordinal);
            evaluationStack = new List<string>();
            recursion = new RecursionTracker();
        }

        // Internal constructor to create a child interpreter that shares stores
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

        public decimal NumericValue
        {
            get
            {
                return this.numericValue;
            }
        }

        public string StringValue
        {
            get
            {
                return this.stringValue;
            }
        }

        public TimeSpan LastExecutionTime
        {
            get
            {
                return this.lastExecutionTime;
            }
        }

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

        internal void ProduceOutput(decimal numericValue, string stringValue)
        {
            this.numericValue = numericValue;
            this.stringValue = stringValue;
        }

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

        internal bool TryGetNumeric(string name, out decimal value)
            => numericVariables.TryGetValue(name, out value);

        internal bool TryGetNumericArray(string name, out decimal[] values)
            => numericArrayVariables.TryGetValue(name, out values!);

        internal bool TryGetExpression(string name, out string expr)
        {
            if (expressionVariables.TryGetValue(name, out expr!))
            {
                return true;
            }
            expr = string.Empty;
            return false;
        }

        // Recursion tracking helpers managed by the parser when evaluating expression variables
        internal bool IsEvaluating(string name) => evaluationStack.Contains(name);

        internal int BeginEvaluating(string name)
        {
            int depth = evaluationStack.Count;
            evaluationStack.Add(name);
            return depth;
        }

        internal void EndEvaluating(int depth)
        {
            while (evaluationStack.Count > depth)
            {
                evaluationStack.RemoveAt(evaluationStack.Count - 1);
            }
        }

        // Recursion hit scoping across a single top-level expression evaluation
        internal int BeginRecursionScope()
        {
            return recursion.Hits;
        }

        // Returns true if recursion occurred within this scope
        internal bool EndRecursionScope(int startHits)
        {
            return recursion.Hits > startHits;
        }

        internal void MarkRecursionHit()
        {
            recursion.Hits++;
        }

        internal void AssignNumeric(string name, decimal value)
        {
            numericVariables[name] = value;
            // Remove conflicting bindings
            expressionVariables.Remove(name);
        }

        internal void AssignNumericArray(string name, decimal[] values)
        {
            numericArrayVariables[name] = values;
        }

        internal void AssignExpression(string name, string expressionText)
        {
            expressionVariables[name] = expressionText;
            // Remove conflicting numeric value
            numericVariables.Remove(name);
        }

        #endregion

        #region Expression Evaluation

        internal FunctionValue EvaluateExpression(string expressionText)
        {
            // Create a child interpreter sharing stores, so ProduceOutput doesn't affect parent state
            var child = new InputInterpreter(this);
            using var ms = new MemoryStream(Encoding.UTF8.GetBytes(expressionText));
            var scanner = new CSMic.Interpreter.Scanner(ms);
            var parser = new CSMic.Interpreter.Parser(scanner)
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

        // Primary developer-facing API: interpret input and return numeric result
        public decimal Interpret(string input)
        {
            DateTime start = DateTime.Now;
            try
            {
                using var ms = new MemoryStream(Encoding.UTF8.GetBytes(input ?? string.Empty));
                var scanner = new CSMic.Interpreter.Scanner(ms);
                var parser = new CSMic.Interpreter.Parser(scanner)
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

        public void RegisterFunction(ICodedFunction function)
        {
            functions[function.Name] = function;
        }

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
