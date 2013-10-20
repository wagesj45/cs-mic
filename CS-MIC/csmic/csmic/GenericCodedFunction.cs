using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csmic
{
    /// <summary>
    /// A generically coded implementation of the ICodedFunction interface.
    /// </summary>
    internal class GenericCodedFunction : ICodedFunction
    {
        #region Members

        /// <summary> Number of expected arguments. </summary>
        private int numExpectedArguments;
        /// <summary> Name of the function. </summary>
        private string functionName;
        /// <summary> The method body. </summary>
        private Func<decimal[], decimal> methodBody;

        #endregion

        #region Constructor

        /// <summary> Constructor. </summary>
        /// <param name="functionName">         Name of the function. </param>
        /// <param name="NumExpectedArguments"> Number of expected arguments. </param>
        /// <param name="methodBody">           The method body. </param>
        internal GenericCodedFunction(string functionName, int numExpectedArguments, Func<decimal[], decimal> methodBody)
        {
            this.functionName = functionName;
            this.numExpectedArguments = numExpectedArguments;
            this.methodBody = methodBody;
        } 
        
        #endregion

        #region ICodedFunction Members

        /// <summary> Gets the number of expected arguments. </summary>
        /// <value> The total number of expected arguments. </value>
        public int NumExpectedArguments
        {
            get
            {
                return this.numExpectedArguments;
            }
        }

        /// <summary> Gets the name of the function. </summary>
        /// <value> The name of the function. </value>
        public string FunctionName
        {
            get 
            { 
                return this.functionName; 
            }
        }

        /// <summary> Executes a code block that computes the value of the function. </summary>
        /// <param name="args"> A variable-length parameters list containing arguments. </param>
        /// <returns> The decimal value computed by the function. </returns>
        public decimal Execute(params decimal[] args)
        {
            if (this.methodBody != null)
            {
                return this.methodBody(args);
            }

            throw new MissingMethodException(this.GetType().Name, this.functionName);
        } 
        
        #endregion
    }
}
