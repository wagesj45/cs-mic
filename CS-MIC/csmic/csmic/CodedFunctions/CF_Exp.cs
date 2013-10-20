using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csmic.CodedFunctions
{
    /// <summary>
    /// A coded implementation of the exponential function based on the constant e.
    /// </summary>
    class CF_Exp : ICodedFunction
    {
        #region ICodedFunction Members

        /// <summary>
        /// Expects 1 argument.
        /// </summary>
        public int NumExpectedArguments
        {
            get { return 1; }
        }

        /// <summary>
        /// The name of the function.
        /// </summary>
        public string FunctionName
        {
            get { return "exp"; }
        }

        /// <summary>
        /// Executes a code block.
        /// </summary>
        /// <param name="args">The arguments used in the code block.</param>
        /// <returns>The constant e raised to the power of the argument.</returns>
        public decimal Execute(params decimal[] args)
        {
            decimal output = 0;
            if (args.Length == this.NumExpectedArguments)
            {
                decimal input = args[0];
                output = (decimal)Math.Exp((double)input);
            }
            return output;
        }

        #endregion
    }
}
