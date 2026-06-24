using CSMic;
using CSMic.StandardLibrary.Functions.Random;

namespace CSMic.StandardLibrary.Functions.Random
{
    /// <summary>
    /// Represents the standard-library <c>bern</c> function.
    /// </summary>
    /// <remarks>
    /// The <c>bern</c> function evaluates a probability and returns a boolean result using a Bernoulli trial.
    /// </remarks>
    public class Bernoulli : RandomBase, ICodedFunction
    {
        /// <summary>
        /// Gets the expression-language name used to invoke this function.
        /// </summary>
        /// <value><c>bern</c>.</value>
        public string Name
        {
            get
            {
                return "bern";
            }
        }

        /// <summary>
        /// Gets the argument signature expected by the <c>bern</c> function.
        /// </summary>
        /// <value>A single numeric argument named <c>p</c>.</value>
        public override IEnumerable<FunctionArgument> ExpectedArguments
        {
            get
            {
                yield return new FunctionArgument("p", FunctionValue.NUMBER);
            }
        }

        /// <summary>
        /// Executes the <c>bern</c> function.
        /// </summary>
        /// <param name="args">
        /// The evaluated arguments supplied to the function. Exactly one numeric argument is expected.
        /// </param>
        /// <returns>
        /// A boolean <see cref="FunctionValue"/> containing <c>true</c> when the Bernoulli trial succeeds; otherwise <c>false</c>.
        /// </returns>
        public FunctionValue Execute(params FunctionArgument[] args)
        {
            return base.Execute(args, (_args) =>
            {
                var input = _args[0].Value;
                decimal pValue = Convert.ToDecimal(input.Value);

                if(pValue < 0m || pValue > 1m)
                {
                    throw new ArgumentOutOfRangeException(nameof(pValue), "The p value must be between 0 and 1.");
                }

                if(NextDecimal() < pValue)
                {
                    return FunctionValue.TRUE;
                }

                return FunctionValue.FALSE;
            });
        }
    }
}
