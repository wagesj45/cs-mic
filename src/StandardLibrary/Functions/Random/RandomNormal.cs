using CSMic;
using CSMic.StandardLibrary.Functions.Random;

namespace CSMic.StandardLibrary.Functions.Random
{
    /// <summary>
    /// Represents the standard-library <c>randn</c> function.
    /// </summary>
    /// <remarks>
    /// The <c>randn</c> function returns a pseudo-random decimal sampled from a normal distribution.
    /// </remarks>
    public class RandomNormal : RandomBase, ICodedFunction
    {
        /// <summary>
        /// Gets the expression-language name used to invoke this function.
        /// </summary>
        /// <value><c>randn</c>.</value>
        public string Name
        {
            get
            {
                return "randn";
            }
        }

        /// <summary>
        /// Gets the argument signature expected by the <c>randn</c> function.
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
        /// Executes the <c>randn</c> function.
        /// </summary>
        /// <param name="args">
        /// The evaluated arguments supplied to the function. No arguments are expected.
        /// </param>
        /// <returns>
        /// A numeric <see cref="FunctionValue"/> containing a pseudo-random decimal sampled from a normal distribution.
        /// </returns>
        public FunctionValue Execute(params FunctionArgument[] args)
        {
            return base.Execute(args, (_args) =>
            {
                return new FunctionValue(FunctionValueType.Numeric, NextDecimalNormal());
            });
        }
    }
}
