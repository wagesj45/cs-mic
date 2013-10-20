using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csmic
{
    /// <summary> Coded function factory. </summary>
    /// <remarks> 
    /// This class generates new coded functions dynamically.
    /// </remarks>
    public static class CodedFunctionFactory
    {
        /// <summary> Creates a new ICodedFunction interface object that implements the dynamic method described. </summary>
        /// <param name="functionName">         Name of the function. </param>
        /// <param name="numExpectedArguments"> Number of expected arguments. </param>
        /// <param name="methodBody">           The method body. </param>
        /// <returns> An ICodedFunction interface object. </returns>
        public static ICodedFunction Create(string functionName, int numExpectedArguments, Func<decimal[], decimal> methodBody)
        {
            return new GenericCodedFunction(functionName, numExpectedArguments, methodBody);
        }
    }
}
