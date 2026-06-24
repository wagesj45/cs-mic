namespace CSMic.StandardLibrary.Functions.Rounding
{
    /// <summary>
    /// Represents the standard-library <c>truncate</c> function.
    /// </summary>
    /// <remarks>
    /// The <c>truncate</c> function evaluates a numeric value and removes its fractional component.
    /// </remarks>
    public class Truncate : FunctionBase, ICodedFunction
    {

        /// <summary>
        /// Gets the expression-language name used to invoke this function.
        /// </summary>
        /// <value><c>truncate</c>.</value>
        public string Name
        {
            get
            {
                return "truncate";
            }
        }

        /// <summary>
        /// Gets the argument signature expected by the <c>truncate</c> function.
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
        /// Executes the <c>truncate</c> function.
        /// </summary>
        /// <param name="args">
        /// The evaluated arguments supplied to the function. Exactly one numeric argument is expected.
        /// </param>
        /// <returns>
        /// A numeric <see cref="FunctionValue"/> containing the input value with the fractional component removed.
        /// </returns>
        public FunctionValue Execute(params FunctionArgument[] args)
        {
            return Execute(args, (_args) =>
            {
                var input = _args[0].Value;
                decimal value = Convert.ToDecimal(input.Value);

                return new FunctionValue(FunctionValueType.Numeric, Math.Truncate(value));
            });
        }
    }
}
