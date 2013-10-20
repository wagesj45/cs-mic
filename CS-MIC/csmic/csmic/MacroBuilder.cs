using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using csmic.Scripting;
using System.Runtime.Remoting.Messaging;

namespace csmic
{
    /// <summary>
    /// A builder object that executes macro scripts.
    /// </summary>
    public class MacroBuilder
    {
        #region Members

        /// <summary>
        /// The input inputInterpreter.
        /// </summary>
        private InputInterpreter interpreter;
        /// <summary> The script to run as a macro. </summary>
        private string script;
        /// <summary>
        /// The output as a list of strings.
        /// </summary>
        private List<string> output;
        /// <summary>
        /// The time for execution.
        /// </summary>
        private TimeSpan executionTime;
        /// <summary>
        /// The root macro operation.
        /// </summary>
        private MacroOperation rootOperation;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new builder object that executes a given macro script.
        /// </summary>
        /// <param name="script">A list of strings representing the macro.</param>
        /// <param name="inputInterpreter">The InputInterpreter to be used.</param>
        public MacroBuilder(string script, InputInterpreter inputInterpreter)
        {
            this.output = new List<string>();
            this.executionTime = new TimeSpan();

            this.script = script;
            this.interpreter = inputInterpreter;
        }

        /// <summary>
        /// Creates a new builder object that executes a given macro script.
        /// </summary>
        /// <param name="script">A list of strings representing the macro.</param>
        /// <param name="inputInterpreter">The InputInterpreter to be used.</param>
        internal MacroBuilder(MacroOperation script, InputInterpreter inputInterpreter)
        {
            this.output = new List<string>();
            this.executionTime = new TimeSpan();
            DateTime timeStart = DateTime.Now;

            this.interpreter = inputInterpreter;

            this.rootOperation = script;

            ExecuteOperation(this.rootOperation);

            DateTime timeEnd = DateTime.Now;
            this.executionTime = timeEnd - timeStart;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a list of strings representing the output.
        /// </summary>
        public List<string> Output
        {
            get
            {
                return this.output;
            }
        }

        /// <summary>
        /// Gets the execution time of the last script computation.
        /// </summary>
        public TimeSpan LastExecutionTime
        {
            get
            {
                return this.executionTime;
            }
        }

        /// <summary>
        /// Gets the decimal value last computed by the macrobuilder.
        /// </summary>
        public string FinalOutput
        {
            get
            {
                return this.interpreter.Output;
            }
        }

        #endregion

        #region Public Methods

        /// <summary> Runs this macro. </summary>
        public void Run()
        {
            DateTime timeStart = DateTime.Now;

            ASCIIEncoding encoder = new ASCIIEncoding();
            Parser p = new Parser(new Scanner(new MemoryStream(encoder.GetBytes(this.script))));
            p.Parse();

            this.rootOperation = p.Root;

            ExecuteOperation(p.Root);

            DateTime timeEnd = DateTime.Now;
            this.executionTime = timeEnd - timeStart;
        }

        #endregion

        #region Private Methods

        private void ExecuteOperation(MacroOperation operation)
        {
            switch (operation.OperationType)
            {
                case OperationType.If:
                    if (operation.Input.Count == 1)
                    {
                        this.interpreter.Interpret(operation.Input[0]);
                        if (this.interpreter.Decimal == 1)
                        {
                            foreach (MacroOperation op in operation.Children)
                            {
                                ExecuteOperation(op);
                            }
                        }
                    }
                    break;
                case OperationType.Else:
                    foreach (MacroOperation op in operation.Children)
                    {
                        ExecuteOperation(op);
                    }
                    break;
                case OperationType.IfElse:
                    if (operation.Children.Count == 2 && operation.Children[0].Input.Count == 1)
                    {
                        MacroOperation ifOp = operation.Children[0];
                        MacroOperation elseOp = operation.Children[1];
                        this.interpreter.Interpret(ifOp.Input[0]);
                        if (this.interpreter.Decimal == 1)
                        {
                            foreach (MacroOperation op in ifOp.Children)
                            {
                                ExecuteOperation(op);
                            }
                        }
                        else
                        {
                            foreach (MacroOperation op in elseOp.Children)
                            {
                                ExecuteOperation(op);
                            }
                        }
                    }
                    break;
                case OperationType.While:
                    if (operation.Input.Count == 1)
                    {
                        this.interpreter.Interpret(operation.Input[0]);
                        while (this.interpreter.Decimal == 1)
                        {
                            foreach (MacroOperation op in operation.Children)
                            {
                                ExecuteOperation(op);
                            }
                            this.interpreter.Interpret(operation.Input[0]);
                        }
                    }
                    break;
                case OperationType.For:
                    if (operation.Input.Count == 3)
                    {
                        this.interpreter.Interpret(operation.Input[0]);
                        this.interpreter.Interpret(operation.Input[1]);
                        int loopCount = this.interpreter.Int;
                        while (this.interpreter.Int == 1)
                        {
                            foreach (MacroOperation op in operation.Children)
                            {
                                ExecuteOperation(op);
                            }
                            this.interpreter.Interpret(operation.Input[2]);
                            this.interpreter.Interpret(operation.Input[1]);
                        }
                    }
                    break;
                case OperationType.FunctionDeclaration:
                    if (operation.Input.Count > 1)
                    {
                        string name = operation.Input[0];
                        operation.Input.RemoveAt(0);
                        StringBuilder builder = new StringBuilder();
                        operation.OperationType = OperationType.Unknown;
                        InterpretedFunction function = new InterpretedFunction(name, operation, operation.Input.ToArray());
                        if (this.interpreter.InterpretedFunctions.Contains(function))
                        {
                            this.interpreter.InterpretedFunctions[this.interpreter.InterpretedFunctions.IndexOf(function)] = function;
                        }
                        else
                        {
                            this.interpreter.InterpretedFunctions.Add(function);
                        }
                    }
                    break;
                case OperationType.Echo:
                    if (operation.Children.Count == 1)
                    {
                        MacroOperation op = operation.Children[0];
                        if (op.OperationType == OperationType.Statement)
                        {
                            this.interpreter.Interpret(op.Input[0]);
                            this.output.Add(this.interpreter.Output);
                        }
                    }
                    break;
                case OperationType.Say:
                    if (operation.Input.Count == 1)
                    {
                        this.output.Add(operation.Input[0]);
                    }
                    break;
                case OperationType.Display:
                    StringBuilder sb = new StringBuilder();
                    foreach (MacroOperation op in operation.Children)
                    {
                        if (op.OperationType == OperationType.Statement)
                        {
                            if (op.Input.Count == 1)
                            {
                                this.interpreter.Interpret(op.Input[0]);
                                sb.Append(this.interpreter.Output);
                            }
                        }
                        else if (op.OperationType == OperationType.String)
                        {
                            if (op.Input.Count == 1)
                            {
                                sb.Append(op.Input[0]);
                            }
                        }
                    }
                    this.output.Add(sb.ToString());
                    break;
                case OperationType.Statement:
                    if (operation.Input.Count == 1)
                    {
                        this.interpreter.Interpret(operation.Input[0]);
                    }
                    break;
                case OperationType.String:
                    //Should not reach this state.
                    break;
                case OperationType.Unknown:
                    if (operation.Children.Count > 0)
                    {
                        foreach (MacroOperation op in operation.Children)
                        {
                            ExecuteOperation(op);
                        }
                    }
                    break;
                default:
                    //CRAP.
                    break;
            }
        }

        #endregion

        #region Asynchronous

        /// <summary> Executes the asynchronous operation. </summary>
        /// <param name="input">    The input. </param>
        /// <param name="callback"> The callback. </param>
        public void RunAsync(string input, Action<MacroBuilder> callback)
        {
            MacroAsyncDelegate del = new MacroAsyncDelegate(RunAsyncThreadingWork);
            del.BeginInvoke(input, (result) =>
            {
                AsyncResult returned = result as AsyncResult;
                if (returned != null)
                {
                    MacroAsyncDelegate end = returned.AsyncDelegate as MacroAsyncDelegate;
                    if (end != null)
                    {
                        MacroBuilder returnValue = end.EndInvoke(result);
                        callback(returnValue);
                    }
                }
            }, null);
        }

        /// <summary> Executes the asynchronous threading work operation. </summary>
        /// <param name="input"> The input. </param>
        /// <returns> . </returns>
        private MacroBuilder RunAsyncThreadingWork(string input)
        {
            Run();
            return this;
        }

        /// <summary> Macro asynchronous delegate. </summary>
        /// <param name="input"> The input. </param>
        /// <returns> . </returns>
        private delegate MacroBuilder MacroAsyncDelegate(string input);

        #endregion
    }
}
