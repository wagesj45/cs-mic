namespace CSMic.StandardLibrary.Functions.Rounding
{
    /// <summary>
    /// Represents the standard-library <c>ceiling</c> function.
    /// </summary>
    /// <remarks>
    /// The <c>ceiling</c> function evaluates a numeric value and rounds it toward positive infinity.
    /// </remarks>
    public class Ceiling : FunctionBase, ICodedFunction
    {

        /// <summary>
        /// Gets the expression-language name used to invoke this function.
        /// </summary>
        /// <value><c>ceiling</c>.</value>
        public string Name
        {
            get
            {
                return "ceiling";
            }
        }

        /// <summary>
        /// Gets the argument signature expected by the <c>ceiling</c> function.
        /// </summary>
        /// <value>A single numeric argument named <c>value</c>.</value>
        public override IEnumerable<FunctionArgument> ExpectedArguments
        {
            get
            {
                yield return new FunctionArgument("value", FunctionValue.NUMBER);
            }
        }

        /// <summary>
        /// Executes the <c>ceiling</c> function.
        /// </summary>
        /// <param name="args">
        /// The evaluated arguments supplied to the function. Exactly one numeric argument is expected.
        /// </param>
        /// <returns>
        /// A numeric <see cref="FunctionValue"/> containing the smallest integer greater than or equal to the input.
        /// </returns>
        public FunctionValue Execute(params FunctionArgument[] args)
        {
            return Execute(args, (_args) =>
            {
                var input = _args[0].Value;
                decimal value = Convert.ToDecimal(input.Value);

                return new FunctionValue(FunctionValueType.Numeric, Math.Ceiling(value));
            });
        }
    }
}
