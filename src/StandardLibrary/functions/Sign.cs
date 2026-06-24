using CSMic;


namespace CSMic.StandardLibrary.Functions
{
    /// <summary>
    /// Represents the standard-library <c>sign</c> function.
    /// </summary>
    /// <remarks>
    /// The <c>sign</c> function evaluates a numeric expression and returns:
    /// <list type="bullet">
    ///   <item><description><c>1</c> when the value is greater than or equal to zero.</description></item>
    ///   <item><description><c>-1</c> when the value is less than zero.</description></item>
    /// </list>
    /// </remarks>
    public class Sign : FunctionBase, ICodedFunction
    {
        /// <summary> (Immutable) The return value representing "positive". </summary>
        private const decimal POSITIVE = 1;
        /// <summary> (Immutable) The return value representing "negative". </summary>
        private const decimal NEGATIVE = -1;

        /// <summary>
        /// Gets the expression-language name used to invoke this function.
        /// </summary>[
        /// <value><c>sign</c>.</value>
        public string Name
        {
            get
            {
                return "sign";
            }
        }

        /// <summary>
        /// Gets the argument signature expected by the <c>sign</c> function.
        /// </summary>
        /// <value>
        /// A single numeric argument named <c>value</c>.
        /// </value>
        public override IEnumerable<FunctionArgument> ExpectedArguments
        {
            get
            {
                yield return new FunctionArgument("value", FunctionValue.NUMBER);
            }
        }

        /// <summary>
        /// Executes the <c>sign</c> function.
        /// </summary>
        /// <param name="args">
        /// The evaluated arguments supplied to the function. Exactly one numeric argument is expected.
        /// </param>
        /// <returns>
        /// A numeric <see cref="FunctionValue"/> containing <c>1</c> when the input value is greater than
        /// or equal to zero; otherwise <c>-1</c>.
        /// </returns>
        public FunctionValue Execute(params FunctionArgument[] args)
        {
            return base.Execute(args, (_args) =>
            {
                var input = _args[0].Value;
                decimal number = Convert.ToDecimal(input.Value);

                return new FunctionValue(FunctionValueType.Numeric, number >= 0 ? POSITIVE : NEGATIVE);
            });
        }
    }
}
