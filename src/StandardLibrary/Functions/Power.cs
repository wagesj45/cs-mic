using System;
using System.Collections.Generic;
using System.Text;

namespace CSMic.StandardLibrary.Functions
{
    /// <summary>
    /// Represents the standard-library <c>pow</c> function.
    /// </summary>
    /// <remarks>
    /// The <c>pow</c> function evaluates a numeric base and exponent, then returns the base raised to that exponent.
    /// </remarks>
    public class Power : FunctionBase, ICodedFunction
    {
        /// <summary>
        /// Gets the expression-language name used to invoke this function.
        /// </summary>
        /// <value><c>pow</c>.</value>
        public string Name
        {
            get
            {
                return "pow";
            }
        }

        /// <summary>
        /// Gets the argument signature expected by the <c>pow</c> function.
        /// </summary>
        /// <value>Two numeric arguments named <c>base</c> and <c>exponent</c>.</value>
        public override IEnumerable<FunctionArgument> ExpectedArguments
        {
            get
            {
                yield return new FunctionArgument("base", FunctionValue.NUMBER);
                yield return new FunctionArgument("exponent", FunctionValue.NUMBER);
            }
        }

        /// <summary>
        /// Executes the <c>pow</c> function.
        /// </summary>
        /// <param name="args">
        /// The evaluated arguments supplied to the function. Exactly two numeric arguments are expected.
        /// </param>
        /// <returns>
        /// A numeric <see cref="FunctionValue"/> containing the base raised to the supplied exponent.
        /// </returns>
        public FunctionValue Execute(params FunctionArgument[] args)
        {
            return base.Execute(args, (_args) =>
            {
                var input = _args[0].Value;
                var input2 = _args[1].Value;
                double _base = Convert.ToDouble(input.Value);
                double exponent = Convert.ToDouble(input2.Value);

                return new FunctionValue(FunctionValueType.Numeric, Math.Pow(_base, exponent));
            });
        }
    }
}
