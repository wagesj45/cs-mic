using CSMic;
using CSMic.StandardLibrary.Functions.Random;

namespace CSMic.StandardLibrary.Functions.Random
{
    /// <summary>
    /// Represents the standard-library <c>rand</c> function.
    /// </summary>
    /// <remarks>
    /// The <c>rand</c> function returns a uniformly distributed pseudo-random decimal.
    /// </remarks>
    public class RandomUniform : RandomBase, ICodedFunction
    {
        /// <summary>
        /// Gets the expression-language name used to invoke this function.
        /// </summary>
        /// <value><c>rand</c>.</value>
        public string Name
        {
            get
            {
                return "rand";
            }
        }

        /// <summary>
        /// Gets the argument signature expected by the <c>rand</c> function.
        /// </summary>
        /// <value>This function takes no arguments.</value>
        public override IEnumerable<FunctionArgument> ExpectedArguments
        {
            get
            {
                yield break;
            }
        }

        /// <summary>
        /// Executes the <c>rand</c> function.
        /// </summary>
        /// <param name="args">
        /// The evaluated arguments supplied to the function. No arguments are expected.
        /// </param>
        /// <returns>
        /// A numeric <see cref="FunctionValue"/> containing a pseudo-random decimal in the half-open interval [0, 1).
        /// </returns>
        public FunctionValue Execute(params FunctionArgument[] args)
        {
            return base.Execute(args, (_args) =>
            {
                return new FunctionValue(FunctionValueType.Numeric, NextDecimal());
            });
        }
    }
}
