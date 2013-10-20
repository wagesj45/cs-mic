using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csmic
{
    /// <summary>
    /// Implements a function that is coded in the .Net environment.
    /// </summary>
    /// <remarks>This interface is required to implement a method or function 
    /// that can be used by the CS-MIC inputInterpreter. It is worth noting that the 
    /// function's name will be the text that is used in the inputInterpreter as the 
    /// executable text.</remarks>
    public interface ICodedFunction
    {
        #region Properties

        /// <summary>
        /// Gets the number of expected arguments.
        /// </summary>
        int NumExpectedArguments { get; }
        /// <summary>
        /// Gets the name of the function.
        /// </summary>
        /// <remarks>The input inputInterpreter will use this function name as 
        /// executable text, expecting an opening and closing parenthesis following 
        /// it.</remarks>
        string FunctionName { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Executes a code block that computes the value of the function.
        /// </summary>
        /// <param name="args">An array of arguments passed to the function.</param>
        /// <returns>The decimal value computed by the function.</returns>
        /// <remarks>Any code block is valid. Error handling must be done by the 
        /// developer, as the inputInterpreter cannot determine if there is an error.</remarks>
        /// <example>
        /// This example shows how to implement the sine function through the interface's 
        /// Execute() function.
        /// <code>
        /// public decimal Execute(params decimal[] args)
        ///{
        ///    //Set up an output variable.
        ///    decimal output = 0;
        ///    
        ///    //Check to see if the number or arguments recieved 
        ///    //is equal to the number of arguments expected.
        ///    if (args.Length == this.NumExpectedArguments)
        ///    {
        ///        //Grab the argument and set a local variable for clarity.
        ///        decimal input = args[0];
        ///        
        ///        //Set the output as a sine of the input.
        ///        output = (decimal)Math.Sin((double)input);
        ///    }
        ///    
        ///    //Return the output. The function will return the sine if the arguments 
        ///    //matched what was expected, and will return 0 otherwise. Returning 0 on 
        ///    //errors is the standard in CS-MIC.
        ///    return output;
        ///}
        ///</code>
        ///</example>
        decimal Execute(params decimal[] args);

        #endregion
    }
}
