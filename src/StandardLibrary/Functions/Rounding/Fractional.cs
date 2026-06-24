namespace CSMic.StandardLibrary.Functions.Rounding
{
    /// <summary>
    /// Represents the standard-library <c>frac</c> function.
    /// </summary>
    /// <remarks>
    /// The <c>frac</c> function evaluates a numeric value and returns its fractional component.
    /// </remarks>
    public class Fractional : FunctionBase, ICodedFunction
    {

        /// <summary>
        /// Gets the expression-language name used to invoke this function.
        /// </summary>
        /// <value><c>frac</c>.</value>
        public string Name
        {
            get
            {
                return "frac";
            }
        }

        /// <summary>
        /// Gets the argument signature expected by the <c>frac</c> function.
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
        /// Executes the <c>frac</c> function.
        /// </summary>
        /// <param name="args">
        /// The evaluated arguments supplied to the function. Exactly one numeric argument is expected.
        /// </param>
        /// <returns>
        /// A numeric <see cref="FunctionValue"/> containing the fractional component of the input value.
        /// </returns>
        public FunctionValue Execute(params FunctionArgument[] args)
        {
            return Execute(args, (_args) =>
            {
                var input = _args[0].Value;
                decimal value = Convert.ToDecimal(input.Value);

                return new FunctionValue(FunctionValueType.Numeric, value - Math.Truncate(value));
            });
        }
    }
}
