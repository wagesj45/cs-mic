using CSMic;
using CSMic.StandardLibrary.Functions.Random;

namespace CSMic.StandardLibrary.Functions.Random
{
    /// <summary>
    /// Represents the standard-library <c>rands</c> function.
    /// </summary>
    /// <remarks>
    /// The <c>rands</c> function evaluates two numeric bounds and returns a uniformly distributed pseudo-random decimal between them.
    /// </remarks>
    public class RandomUniformSpread : RandomBase, ICodedFunction
    {
        /// <summary>
        /// Gets the expression-language name used to invoke this function.
        /// </summary>
        /// <value><c>rands</c>.</value>
        public string Name
        {
            get
            {
                return "rands";
            }
        }

        /// <summary>
        /// Gets the argument signature expected by the <c>rands</c> function.
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
        /// Executes the <c>rands</c> function.
        /// </summary>
        /// <param name="args">
        /// The evaluated arguments supplied to the function. Exactly two numeric arguments are expected.
        /// </param>
        /// <returns>
        /// A numeric <see cref="FunctionValue"/> containing a uniformly distributed pseudo-random decimal between the supplied bounds.
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

                return new FunctionValue(FunctionValueType.Numeric, lower + NextDecimal() * (upper - lower));
            });
        }
    }
}
