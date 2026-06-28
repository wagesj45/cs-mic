using System;
using System.Collections.Generic;
using System.Text;

namespace CSMic.StandardLibrary.Functions
{
    /// <summary>
    /// Represents the standard-library <c>lerp</c> smooth-step function.
    /// </summary>
    /// <remarks>
    /// This function evaluates a value between two edges, clamps it to the range from <c>0</c> through <c>1</c>, and
    /// applies the smooth-step polynomial <c>x * x * (3 - 2 * x)</c>.
    /// </remarks>
    public class SmoothStep : FunctionBase, ICodedFunction
    {
        /// <summary>
        /// Gets the expression-language name used to invoke this function.
        /// </summary>
        /// <value><c>lerp</c>.</value>
        public string Name
        {
            get
            {
                return "lerp";
            }
        }

        /// <summary>
        /// Gets the argument signature expected by the <c>lerp</c> smooth-step function.
        /// </summary>
        /// <value>Three numeric arguments named <c>startEdge</c>, <c>endEdge</c>, and <c>value</c>.</value>
        public override IEnumerable<FunctionArgument> ExpectedArguments
        {
            get
            {
                yield return new FunctionArgument("startEdge", FunctionValue.NUMBER);
                yield return new FunctionArgument("endEdge", FunctionValue.NUMBER);
                yield return new FunctionArgument("value", FunctionValue.NUMBER);
            }
        }

        /// <summary>
        /// Executes the <c>lerp</c> smooth-step function.
        /// </summary>
        /// <param name="args">
        /// The evaluated arguments supplied to the function. Exactly three numeric arguments are expected.
        /// </param>
        /// <returns>
        /// A numeric <see cref="FunctionValue"/> containing the smoothed interpolation ratio.
        /// </returns>
        public FunctionValue Execute(params FunctionArgument[] args)
        {
            return base.Execute(args, (_args) =>
            {
                var input = _args[0].Value;
                var input2 = _args[1].Value;
                var input3 = _args[2].Value;

                decimal startEdge = Convert.ToDecimal(input);
                decimal endEdge = Convert.ToDecimal(input2);
                decimal value = Convert.ToDecimal(input3);

                var normalization = Math.Clamp((value - startEdge) / (endEdge - startEdge), 0, 1);
                var polynomialization = normalization * normalization * (3 - (2 * normalization));

                return new FunctionValue(FunctionValueType.Numeric, polynomialization);
            });
        }
    }
}
