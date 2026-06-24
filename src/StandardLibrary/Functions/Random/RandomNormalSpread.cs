using CSMic;
using CSMic.StandardLibrary.Functions.Random;

namespace CSMic.StandardLibrary.Functions.Random
{
    /// <summary>
    /// Represents the standard-library <c>randns</c> function.
    /// </summary>
    /// <remarks>
    /// The <c>randns</c> function evaluates two numeric bounds and returns a pseudo-random decimal derived from a normal distribution.
    /// </remarks>
    public class RandomNormalSpread : RandomBase, ICodedFunction
    {
        /// <summary>
        /// Gets the expression-language name used to invoke this function.
        /// </summary>
        /// <value><c>randns</c>.</value>
        public string Name
        {
            get
            {
                return "randns";
            }
        }

        /// <summary>
        /// Gets the argument signature expected by the <c>randns</c> function.
        /// </summary>
        /// <value>Two numeric arguments named <c>lower</c> and <c>upper</c>.</value>
        public override IEnumerable<FunctionArgument> ExpectedArguments
        {
            get
            {
                yield return new FunctionArgument("lower", FunctionValue.NUMBER);
                yield return new FunctionArgument("upper", FunctionValue.NUMBER);
            }
        }

        /// <summary>
        /// Executes the <c>randns</c> function.
        /// </summary>
        /// <param name="args">
        /// The evaluated arguments supplied to the function. Exactly two numeric arguments are expected.
        /// </param>
        /// <returns>
        /// A numeric <see cref="FunctionValue"/> containing a pseudo-random decimal derived from a normal distribution.
        /// </returns>
        public FunctionValue Execute(params FunctionArgument[] args)
        {
            return base.Execute(args, (_args) =>
            {
                var inputLower = _args[0].Value;
                var inputUpper = _args[1].Value;
                decimal lower = Convert.ToDecimal(inputLower.Value);
                decimal upper = Convert.ToDecimal(inputUpper.Value);

                if(upper <= lower)
                {
                    return FunctionValue.ZERO;
                }

                return new FunctionValue(FunctionValueType.Numeric, NextDecimalNormal() * (upper - lower));
            });
        }
    }
}
