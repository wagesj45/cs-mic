using System;
using System.Collections.Generic;
using System.Text;

namespace CSMic.StandardLibrary.Functions
{
    /// <summary>
    /// Represents the standard-library <c>normalize</c> function.
    /// </summary>
    /// <remarks>
    /// The <c>normalize</c> function evaluates a numeric value and returns its position in the supplied range as a
    /// zero-based ratio.
    /// </remarks>
    public class Normalize : FunctionBase, ICodedFunction
    {
        /// <summary>
        /// Gets the expression-language name used to invoke this function.
        /// </summary>
        /// <value><c>normalize</c>.</value>
        public string Name
        {
            get
            {
                return "normalize";
            }
        }

        /// <summary>
        /// Gets the argument signature expected by the <c>normalize</c> function.
        /// </summary>
        /// <value>Three numeric arguments named <c>value</c>, <c>minimum</c>, and <c>maximum</c>.</value>
        public override IEnumerable<FunctionArgument> ExpectedArguments
        {
            get
            {
                yield return new FunctionArgument("value", FunctionValue.NUMBER);
                yield return new FunctionArgument("minimum", FunctionValue.NUMBER);
                yield return new FunctionArgument("maximum", FunctionValue.NUMBER);
            }
        }

        /// <summary>
        /// Executes the <c>normalize</c> function.
        /// </summary>
        /// <param name="args">
        /// The evaluated arguments supplied to the function. Exactly three numeric arguments are expected.
        /// </param>
        /// <returns>
        /// A numeric <see cref="FunctionValue"/> containing the normalized ratio, or <c>0</c> when the minimum and
        /// maximum bounds are equal.
        /// </returns>
        public FunctionValue Execute(params FunctionArgument[] args)
        {
            return base.Execute(args, (_args) =>
            {
                var input = _args[0].Value;
                var input2 = _args[1].Value;
                var input3 = _args[2].Value;

                decimal number = Convert.ToDecimal(input.Value);
                decimal minimum = Convert.ToDecimal(input2.Value);
                decimal maximum = Convert.ToDecimal(input3.Value);

                if (minimum == maximum)
                {
                    return FunctionValue.ZERO;
                }

                var normalization = (number - minimum) / (maximum - minimum);

                return new FunctionValue(FunctionValueType.Numeric, normalization);
            });
        }
    }
}
