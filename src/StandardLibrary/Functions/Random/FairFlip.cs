using CSMic;
using CSMic.StandardLibrary.Functions.Random;

namespace CSMic.StandardLibrary.Functions.Random
{
    /// <summary>
    /// Represents the standard-library <c>flip</c> function.
    /// </summary>
    /// <remarks>
    /// The <c>flip</c> function returns a fair boolean result from a 50/50 random trial.
    /// </remarks>
    public class FairFlip : RandomBase, ICodedFunction
    {
        /// <summary>
        /// Gets the expression-language name used to invoke this function.
        /// </summary>
        /// <value><c>flip</c>.</value>
        public string Name
        {
            get
            {
                return "flip";
            }
        }

        /// <summary>
        /// Gets the argument signature expected by the <c>flip</c> function.
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
        /// Executes the <c>flip</c> function.
        /// </summary>
        /// <param name="args">
        /// The evaluated arguments supplied to the function. No arguments are expected.
        /// </param>
        /// <returns>
        /// A boolean <see cref="FunctionValue"/> containing <c>true</c> or <c>false</c> with equal probability.
        /// </returns>
        public FunctionValue Execute(params FunctionArgument[] args)
        {
            return base.Execute(args, (_args) =>
            {
                if(NextDecimal() < 0.5m)
                {
                    return FunctionValue.TRUE;
                }

                return FunctionValue.FALSE;
            });
        }
    }
}
