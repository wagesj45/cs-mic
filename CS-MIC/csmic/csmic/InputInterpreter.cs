using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading;
using csmic.CodedFunctions;
using csmic.Interpreter;
using csmic.ComputableEngine;
using System.Runtime.Remoting.Messaging;

// namespace: csmic
//
// summary:	.
namespace csmic
{
    /// <summary>
    /// An interpreter object that reads user input and evaluates the code.
    /// </summary>
    /// <remarks>The interpreter does not support exceptions by design. Instead, invalid 
    /// calculations, parameters, etc. will result in a result of zero.
    /// <code>
    /// InputInterpreter interpreter = new InputInterpreter();
    /// interpreter.Interpret("1/0"); // The result will be 0, not an exception.
    /// </code>
    /// </remarks>
    public class InputInterpreter
    {
        #region Members

        /// <summary>
        /// The output generated.
        /// </summary>
        private string output;
        /// <summary>
        /// The variables assigned.
        /// </summary>
        internal Dictionary<string, Variable> variables;
        /// <summary>
        /// The time for execution.
        /// </summary>
        private TimeSpan executionTime;
        /// <summary>
        /// The verbose message of the calculation.
        /// </summary>
        private string message;
        /// <summary>
        /// The list of coded functions that can be executed.
        /// </summary>
        private List<ICodedFunction> codedFunctions;
        /// <summary>
        /// The list of user defined functions that can be executed.
        /// </summary>
        private List<InterpretedFunction> interpretedFunctions;
        /// <summary>
        /// The private calculated value.
        /// </summary>
        private decimal calculatedValue;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new InputInterpreter.
        /// </summary>
        public InputInterpreter()
        {
            this.output = string.Empty;
            this.variables = new Dictionary<string, Variable>();
            this.executionTime = new TimeSpan();
            this.codedFunctions = new List<ICodedFunction>();
            this.interpretedFunctions = new List<InterpretedFunction>();
            LoadDefaultCodedFunctions();
        }

        /// <summary>
        /// Creates a new InputInterpreter from an original.
        /// </summary>
        /// <param name="original">The orginal input interpreter to copy.</param>
        public InputInterpreter(InputInterpreter original)
        {
            this.output = original.output;
            this.variables = original.variables;
            this.executionTime = original.executionTime;
            this.codedFunctions = original.codedFunctions;
            this.interpretedFunctions = original.interpretedFunctions;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the message that represents the InputInterpreters output.
        /// </summary>
        public string Output
        {
            get
            {
                return this.output;
            }
        }

        /// <summary>
        /// Gets the verbose message that is generated with a calculation.
        /// </summary>
        public string Message
        {
            get
            {
                return this.message;
            }
        }

        /// <summary>
        /// Gets the value of the output as a decimal.
        /// </summary>
        public decimal Decimal
        {
            get
            {
                return this.calculatedValue;
            }
        }

        /// <summary>
        /// Gets the value of the output cast as an int.
        /// </summary>
        public int Int
        {
            get
            {
                return (int)decimal.Round(this.calculatedValue);
            }
        }

        /// <summary>
        /// Gets the value of the output cast as a float.
        /// </summary>
        public float Float
        {
            get
            {
                return (float)this.calculatedValue;
            }
        }

        /// <summary>
        /// Gets the value of the output cast as a double.
        /// </summary>
        public double Double
        {
            get
            {
                return (double)this.calculatedValue;
            }
        }

        /// <summary>
        /// Gets the value (cast as a long) converted to its binary equivalent.
        /// </summary>
        public string Binary
        {
            get
            {
                return Convert.ToString((long)this.calculatedValue, 2).PadLeft(64, '0');
            }
        }

        /// <summary>
        /// Gets the execution time of the last calculation.
        /// </summary>
        public TimeSpan LastExecutionTime
        {
            get
            {
                return this.executionTime;
            }
        }

        /// <summary>
        /// Gets or sets a list of coded functions that the interpreter supports.
        /// </summary>
        public List<ICodedFunction> CodedFunctions
        {
            get
            {
                return this.codedFunctions;
            }
            set
            {
                this.codedFunctions = value;
            }
        }

        /// <summary>
        /// Gets or sets a list of user generated interpreted functions that the interpreter supports.
        /// </summary>
        /// <value> The interpreted functions. </value>
        public List<InterpretedFunction> InterpretedFunctions
        {
            get
            {
                return this.interpretedFunctions;
            }
            set
            {
                this.interpretedFunctions = value;
            }
        }

        /// <summary> Gets the variables. </summary>
        /// <value> The variables. </value>
        public Dictionary<string, Variable> Variables
        {
            get
            {
                return this.variables;
            }
            set
            {
                this.variables = value;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Interprets and executes given input.
        /// </summary>
        /// <param name="input">The input to interpret and execute.</param>
        public void Interpret(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return;
            }

            DateTime timeStart = DateTime.Now;
            this.message = string.Empty;

            UTF8Encoding encoder = new UTF8Encoding();
            Parser p = new Parser(new Scanner(new MemoryStream(encoder.GetBytes(input))));
            p.Interpreter = this;
            try
            {
                p.Parse();
                this.calculatedValue = p.CalculatedValue;
                if (p.errors.count > 0)
                {
                    ProduceOutput(this.calculatedValue, p.errors.builder.ToString());
                }
            }
            catch (Exception e)
            {
                this.calculatedValue = 0;
                ProduceOutput(this.calculatedValue, e.Message);
            }

            DateTime timeEnd = DateTime.Now;
            this.executionTime = timeEnd - timeStart;
        }

        /// <summary>
        /// Computes an expression and returns the result as a decimal.
        /// </summary>
        /// <param name="expression">The expression to be calculated.</param>
        /// <returns>The value that was computed.</returns>
        public decimal ComputeExpression(string expression)
        {
            this.Interpret(expression);
            return this.calculatedValue;
        }

        /// <summary>
        /// Assigns a decimal value to a variable.
        /// </summary>
        /// <param name="name">The name of the variable.</param>
        /// <param name="value">The value of the variable.</param>
        /// <returns>True if the variable was set, false otherwise.</returns>
        internal bool Assign(string name, decimal value)
        {
            Variable v = new Variable();
            v.Type = VariableType.Decimal;
            v.Value = value;

            if (!this.variables.ContainsKey(name))
            {
                this.variables.Add(name, v);
            }
            else
            {
                this.variables[name] = v;
            }

            return true;
        }

        /// <summary>
        /// Assigns a decimal value to a variable.
        /// </summary>
        /// <param name="name">The name of the variable.</param>
        /// <param name="expression">The expression of the variable.</param>
        /// <returns>True if the variable was set, false otherwise.</returns>
        internal bool Assign(string name, string expression)
        {
            Variable v = new Variable();
            v.Type = VariableType.Equation;
            v.Value = expression;

            if (!this.variables.ContainsKey(name))
            {
                this.variables.Add(name, v);
            }
            else
            {
                this.variables[name] = v;
            }

            return true;
        }

        /// <summary>
        /// Assigns a decimal value to a variable.
        /// </summary>
        /// <param name="name">The name of the variable.</param>
        /// <param name="values">The values of the variable.</param>
        /// <returns>True if the variable was set, false otherwise.</returns>
        internal bool Assign(string name, decimal[] values)
        {
            Variable v = new Variable();
            v.Type = VariableType.Array;
            v.Value = values;

            if (!this.variables.ContainsKey(name))
            {
                this.variables.Add(name, v);
            }
            else
            {
                this.variables[name] = v;
            }

            return true;
        }

        /// <summary>
        /// Executes a function stored in the interpreter.
        /// </summary>
        /// <param name="name">The name of the function to execute.</param>
        /// <param name="args">The arguments to pass to the function.</param>
        /// <returns>The value computed from the function execution.</returns>
        internal decimal ExecuteFunction(string name, decimal[] args)
        {
            foreach (ICodedFunction codedFunction in this.codedFunctions)
            {
                if (codedFunction.FunctionName == name)
                {
                    return codedFunction.Execute(args);
                }
            }

            foreach (InterpretedFunction interpretedFunction in this.interpretedFunctions)
            {
                if (interpretedFunction.Name == name)
                {
                    string answer = interpretedFunction.Compute(args);
                    decimal parsed = 0;
                    if (decimal.TryParse(answer, out parsed))
                    {
                        return parsed;
                    }
                }
            }

            return 0;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Loads the default coded functions supported by the interpreter.
        /// </summary>
        private void LoadDefaultCodedFunctions()
        {
            this.codedFunctions.Add(new CF_Sin());
            this.codedFunctions.Add(new CF_Cos());
            this.codedFunctions.Add(new CF_Tan());
            this.codedFunctions.Add(new CF_Round());
            this.codedFunctions.Add(new CF_Sqrt());
            this.codedFunctions.Add(new CF_Abs());
            this.codedFunctions.Add(new CF_Exp());
            this.codedFunctions.Add(new CF_Log());
            this.codedFunctions.Add(new CF_Precision());
        }

        /// <summary>
        /// Produces output given a single object.
        /// </summary>
        /// <param name="output">The object representing the output.</param>
        internal void ProduceOutput(object output)
        {
            if (output is bool)
            {
                bool o = (bool)output;
                if (o)
                {
                    this.calculatedValue = 1;
                }
                else
                {
                    this.calculatedValue = 0;
                }
            }
            this.output = string.Format("{0}", output);
        }

        /// <summary>
        /// Produces output given an object and a message.
        /// </summary>
        /// <param name="output">The object representing the output.</param>
        /// <param name="message">The message to be displayed with the output.</param>
        private void ProduceOutput(object output, string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                this.output = string.Format("{0}", output);
                this.message = message;
            }
            else
            {
                ProduceOutput(output);
                this.message = string.Empty;
            }
        }

        #endregion

        #region Asynchronous

        /// <summary> Interpret an input asynchronously. </summary>
        /// <param name="input">    The input to interpret and execute. </param>
        /// <param name="callback"> The callback. </param>
        public void InterpretAsync(string input, Action<InputInterpreter> callback)
        {
            InterpretAsyncDelegate del = new InterpretAsyncDelegate(InterpretAsyncThreadingWork);
            del.BeginInvoke(input, (result) => 
            {
                AsyncResult returned = result as AsyncResult;
                if (returned != null)
                {
                    InterpretAsyncDelegate end = returned.AsyncDelegate as InterpretAsyncDelegate;
                    if (end != null)
                    {
                        InputInterpreter returnValue = end.EndInvoke(result);
                        callback(returnValue);
                    }
                }
            }, null);
        }

        /// <summary> Interpret asynchronous threading work. </summary>
        /// <param name="input">    The input to interpret and execute. </param>
        private InputInterpreter InterpretAsyncThreadingWork(string input)
        {
            Interpret(input);
            return this;
        }

        /// <summary> Interpret asynchronous delegate. </summary>
        /// <param name="input"> The input. </param>
        /// <returns> . </returns>
        private delegate InputInterpreter InterpretAsyncDelegate(string input);

        #endregion

        #region Computable

        /// <summary> Converts this object to a computable. </summary>
        /// <returns> This object as a Computable. </returns>
        public Computable ToComputable()
        {
            return new Computable(this.Decimal.ToString(), this);
        }

        /// <summary> Treats the current object as a computable and performs an action in that context. </summary>
        /// <param name="action"> The action to execute as a computable object. </param>
        /// <returns> This object as a Computable, after the given action. </returns>
        public Computable AsComputable(Func<Computable, Computable> action)
        {
            return action(this.ToComputable());
        }

        #endregion
    }
}
