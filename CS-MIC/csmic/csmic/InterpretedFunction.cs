using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csmic
{
    /// <summary>
    /// Represents a user defined function that can be executed by the interpreter.
    /// </summary>
    public class InterpretedFunction
    {
        #region Members

        /// <summary>
        /// The name of the function.
        /// </summary>
        private string name;
        /// <summary>
        /// The number of expected arguments.
        /// </summary>
        private int numExpectedArguments;
        /// <summary>
        /// The set of instructions to be passed to the internal inputInterpreter.
        /// </summary>
        private MacroOperation script;
        /// <summary>
        /// A set of arguments used in computation of the function.
        /// </summary>
        private InterpretedFunctionArgument[] arguments;
        /// <summary>
        /// The internal macro builder used for computation.
        /// </summary>
        private MacroBuilder macroFunction;
        /// <summary>
        /// The internal input inputInterpreter that macro builder will use for computation.
        /// </summary>
        private InputInterpreter interpreter;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new interpreted function.
        /// </summary>
        /// <param name="name">The name of the fuction.</param>
        /// <param name="script">The node to be used in computation.</param>
        /// <param name="args">A set of argument names to be used in computation.</param>
        internal InterpretedFunction(string name, MacroOperation script, params string[] args)
        {
            this.name = name;
            this.script = script;
            this.macroFunction = null;
            this.arguments = new InterpretedFunctionArgument[args.Length];
            this.interpreter = new InputInterpreter();
            this.numExpectedArguments = args.Length;

            for (int i = 0; i < args.Length; i++)
            {
                this.arguments[i] = new InterpretedFunctionArgument(args[i], 0);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the function.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        /// <summary>
        /// Gets the number of expected arguments for the function.
        /// </summary>
        public int NumExpectedArguments
        {
            get
            {
                return this.numExpectedArguments;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Computes the value of the function.
        /// </summary>
        /// <param name="args">The arguments used for computation.</param>
        /// <returns>The decimal value computed by the function.</returns>
        public string Compute(params decimal[] args)
        {
            if (args.Length != this.numExpectedArguments)
            {
                return "0";
            }

            if (args.Length == this.arguments.Length)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    this.arguments[i].Value = args[i];
                }
            }
            
            foreach (InterpretedFunctionArgument argument in this.arguments)
            {
                this.interpreter.Interpret(string.Format("{0} :: {1}", argument.Name, argument.Value));
            }

            this.macroFunction = new MacroBuilder(this.script, this.interpreter);
            
            return this.macroFunction.FinalOutput;
        }

        #endregion

        /// <summary>
        /// Because a function's internal pattern may be different, we must manually check to see if the function
        /// names are the same.
        /// </summary>
        /// <param name="obj">The object to test.</param>
        /// <returns>True if the functions are the same.</returns>
        public override bool Equals(object obj)
        {
            InterpretedFunction fun = obj as InterpretedFunction;
            if(fun != null)
            {
                return fun.name == this.name;
            }

            return false;
        }

        /// <summary> Serves as a hash function for a particular type. </summary>
        /// <returns> A hash code for the current <see cref="T:System.Object" />. </returns>
        public override int GetHashCode()
        {
            return this.script.GetHashCode() & this.name.GetHashCode();
        }
    }
}
