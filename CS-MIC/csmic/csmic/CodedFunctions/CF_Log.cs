using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csmic.CodedFunctions
{
    /// <summary>
    /// A coded implementation of the log function.
    /// </summary>
    class CF_Log : ICodedFunction
    {
        #region ICodedFunction Members

        /// <summary>
        /// Expects 2 arguments.
        /// </summary>
        public int NumExpectedArguments
        {
            get { return 2; }
        }

        /// <summary>
        /// The name of the function.
        /// </summary>
        public string FunctionName
        {
            get { return "log"; }
        }

        /// <summary>
        /// Executes a code block.
        /// </summary>
        /// <param name="args">The arguments used in the code block.</param>
        /// <returns>The log of the first argument to the base of the second argument.</returns>
        public decimal Execute(params decimal[] args)
        {
            decimal output = 0;
            if (args.Length == this.NumExpectedArguments)
            {
                decimal input = args[0];
                decimal logBase = args[1];
                try
                {
                    output = (decimal)Math.Log((double)input, (double)logBase);
                }
                catch
                {
                    output = decimal.MinValue;
                }
            }
            return output;
        }

        #endregion
    }
}
