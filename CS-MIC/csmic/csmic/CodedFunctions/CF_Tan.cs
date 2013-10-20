using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csmic.CodedFunctions
{
    /// <summary>
    /// A coded implementation of the tangent function.
    /// </summary>
    class CF_Tan : ICodedFunction
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
            get { return "tan"; }
        }

        /// <summary>
        /// Executes a code block.
        /// </summary>
        /// <param name="args">The arguments used in the code block.</param>
        /// <returns>The tangent of the argument.</returns>
        public decimal Execute(params decimal[] args)
        {
            decimal output = 0;
            if (args.Length == this.NumExpectedArguments)
            {
                decimal input = args[0];
                output = (decimal)Math.Tan((double)input);
            }
            return output;
        }

        #endregion
    }
}
