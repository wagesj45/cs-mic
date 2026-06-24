namespace CSMic.StandardLibrary.Functions.Rounding
{
    /// <summary>
    /// Represents the standard-library <c>floor</c> function.
    /// </summary>
    /// <remarks>
    /// The <c>floor</c> function evaluates a numeric value and rounds it toward negative infinity.
    /// </remarks>
    public class Floor : FunctionBase, ICodedFunction
    {

        /// <summary>
        /// Gets the expression-language name used to invoke this function.
        /// </summary>
        /// <value><c>floor</c>.</value>
        public string Name
        {
            get
            {
                return "floor";
            }
        }

        /// <summary>
        /// Gets the argument signature expected by the <c>floor</c> function.
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
        /// Executes the <c>floor</c> function.
        /// </summary>
        /// <param name="args">
        /// The evaluated arguments supplied to the function. Exactly one numeric argument is expected.
        /// </param>
        /// <returns>
        /// A numeric <see cref="FunctionValue"/> containing the largest integer less than or equal to the input.
        /// </returns>
        public FunctionValue Execute(params FunctionArgument[] args)
        {
            return Execute(args, (_args) =>
            {
                var input = _args[0].Value;
                decimal value = Convert.ToDecimal(input.Value);

                return new FunctionValue(FunctionValueType.Numeric, Math.Floor(value));
            });
        }
    }
}
