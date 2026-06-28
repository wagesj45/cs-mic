using System;
using System.Collections.Generic;
using System.Text;

namespace CSMic.StandardLibrary.Functions
{
    /// <summary>
    /// Represents the standard-library <c>sqrt</c> function.
    /// </summary>
    /// <remarks>
    /// The <c>sqrt</c> function evaluates a numeric expression and returns its square root.
    /// </remarks>
    public class SquareRoot: FunctionBase, ICodedFunction
    {
        /// <summary>
        /// Gets the expression-language name used to invoke this function.
        /// </summary>
        /// <value><c>sqrt</c>.</value>
        public string Name
        {
            get
            {
                return "sqrt";
            }
        }

        /// <summary>
        /// Gets the argument signature expected by the <c>sqrt</c> function.
        /// </summary>
        /// <value>A single numeric argument named <c>value</c>.</value>
        public override IEnumerable<FunctionArgument> ExpectedArguments
        {
            get
            {
                yield return new FunctionArgument("value", FunctionValue.NUMBER);
            }
        }

        /// <summary>
        /// Executes the <c>sqrt</c> function.
        /// </summary>
        /// <param name="args">
        /// The evaluated arguments supplied to the function. Exactly one numeric argument is expected.
        /// </param>
        /// <returns>
        /// A numeric <see cref="FunctionValue"/> containing the square root of the input.
        /// </returns>
        public FunctionValue Execute(params FunctionArgument[] args)
        {
            return base.Execute(args, (_args) =>
            {
                var input = _args[0].Value;
                double number = Convert.ToDouble(input.Value);

                return new FunctionValue(FunctionValueType.Numeric, Math.Sqrt(number));
            });
        }
    }
}
