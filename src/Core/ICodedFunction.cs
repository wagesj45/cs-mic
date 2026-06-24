using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSMic
{
    /// <summary>
    /// Interface for a coded function that can be created at compiletime and invoked at runtime.
    /// </summary>
    public interface ICodedFunction
    {
        #region Properties

        /// <summary>   Gets the name of the function. </summary>
        /// <value> The name. </value>
        string Name { get; }

        /// <summary>   Gets the expected arguments of the function. </summary>
        /// <value> The expected arguments. </value>
        IEnumerable<FunctionArgument> ExpectedArguments { get; }

        /// <summary>   Gets the return value of the function. </summary>
        /// <value> The return value. </value>
        FunctionValue ReturnValue { get; }

        #endregion

        #region Methods

        /// <summary>   Executes the function with the given arguments. </summary>
        /// <param name="args"> A variable-length parameters list containing arguments. </param>
        /// <returns>   A FunctionValue representing the result of the function execution. </returns>
        FunctionValue Execute(params FunctionArgument[] args);

        #endregion
    }
}
