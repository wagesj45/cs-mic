namespace CSMic.StandardLibrary.Functions.Angle
{
    /// <summary>
    /// Represents the standard-library <c>degrees</c> function.
    /// </summary>
    /// <remarks>
    /// The <c>degrees</c> function evaluates a numeric expression in radians and returns the equivalent angle in degrees.
    /// </remarks>
    public class Degrees : FunctionBase, ICodedFunction
    {

        /// <summary>
        /// Gets the expression-language name used to invoke this function.
        /// </summary>
        /// <value><c>degrees</c>.</value>
        public string Name
        {
            get
            {
                return "degrees";
            }
        }

        /// <summary>
        /// Gets the argument signature expected by the <c>degrees</c> function.
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
        /// Executes the <c>degrees</c> function.
        /// </summary>
        /// <param name="args">
        /// The evaluated arguments supplied to the function. Exactly one numeric argument is expected.
        /// </param>
        /// <returns>
        /// A numeric <see cref="FunctionValue"/> containing the input angle converted to degrees.
        /// </returns>
        public FunctionValue Execute(params FunctionArgument[] args)
        {
            return Execute(args, (_args) =>
            {
                var input = _args[0].Value;
                decimal value = Convert.ToDecimal(input.Value);

                // Convert radians to degrees (compatible with .NET Standard)
                return new FunctionValue(FunctionValueType.Numeric, (double)value * (180.0 / Math.PI));
            });
        }
    }
}
