using System;
using System.Collections.Generic;
using System.Text;

namespace CSMic.StandardLibrary.Functions
{
    /// <summary>
    /// Represents the standard-library <c>lerp</c> function.
    /// </summary>
    /// <remarks>
    /// The <c>lerp</c> function evaluates two numeric endpoints and an interpolation amount, then returns the value
    /// at that position between the endpoints.
    /// </remarks>
    public class Lerp: FunctionBase, ICodedFunction
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
        /// Gets the argument signature expected by the <c>lerp</c> function.
        /// </summary>
        /// <value>Three numeric arguments named <c>start</c>, <c>end</c>, and <c>ammount</c>.</value>
        public override IEnumerable<FunctionArgument> ExpectedArguments
        {
            get
            {
                yield return new FunctionArgument("start", FunctionValue.NUMBER);
                yield return new FunctionArgument("end", FunctionValue.NUMBER);
                yield return new FunctionArgument("ammount", FunctionValue.NUMBER);
            }
        }

        /// <summary>
        /// Executes the <c>lerp</c> function.
        /// </summary>
        /// <param name="args">
        /// The evaluated arguments supplied to the function. Exactly three numeric arguments are expected.
        /// </param>
        /// <returns>
        /// A numeric <see cref="FunctionValue"/> containing the linearly interpolated value.
        /// </returns>
        public FunctionValue Execute(params FunctionArgument[] args)
        {
            return base.Execute(args, (_args) =>
            {
                var input = _args[0].Value;
                var input2 = _args[1].Value;
                var input3 = _args[2].Value;

                decimal start = Convert.ToDecimal(input.Value);
                decimal end = Convert.ToDecimal(input2.Value);
                decimal ammount = Convert.ToDecimal(input3.Value);

                if (start == end)
                {
                    return new FunctionValue(FunctionValueType.Numeric, start);
                }

                var lerp = start + (ammount * (end - start));

                return new FunctionValue(FunctionValueType.Numeric, lerp);
            });
        }
    }
}
