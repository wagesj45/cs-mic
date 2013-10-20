using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csmic.CodedFunctions
{
    /// <summary>
    /// A coded implementation of a precision function.
    /// </summary>
    class CF_Precision : ICodedFunction
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
            get { return "precision"; }
        }

        /// <summary>
        /// Executes a code block.
        /// </summary>
        /// <param name="args">The arguments used in the code block.</param>
        /// <returns>The first argument to the precision of the argument.</returns>
        public decimal Execute(params decimal[] args)
        {
            decimal output = 0;
            if (args.Length == this.NumExpectedArguments)
            {
                decimal input = args[0];
                decimal precision = args[1];
                output = (decimal)Math.Round(input, (int)precision);
            }
            return output;
        }

        #endregion
    }
}
