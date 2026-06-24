namespace CSMic.StandardLibrary.Functions.Angle
{
    /// <summary>
    /// Represents the standard-library <c>radians</c> function.
    /// </summary>
    /// <remarks>
    /// The <c>radians</c> function evaluates a numeric expression in degrees and returns the equivalent angle in radians.
    /// </remarks>
    public class Radians : FunctionBase, ICodedFunction
    {

        /// <summary>
        /// Gets the expression-language name used to invoke this function.
        /// </summary>
        /// <value><c>radians</c>.</value>
        public string Name
        {
            get
            {
                return "radians";
            }
        }

        /// <summary>
        /// Gets the argument signature expected by the <c>radians</c> function.
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
        /// Executes the <c>radians</c> function.
        /// </summary>
        /// <param name="args">
        /// The evaluated arguments supplied to the function. Exactly one numeric argument is expected.
        /// </param>
        /// <returns>
        /// A numeric <see cref="FunctionValue"/> containing the input angle converted to radians.
        /// </returns>
        public FunctionValue Execute(params FunctionArgument[] args)
        {
            return Execute(args, (_args) =>
            {
                var input = _args[0].Value;
                decimal value = Convert.ToDecimal(input.Value);

                // Convert degrees to radians (compatible with .NET Standard)
                return new FunctionValue(FunctionValueType.Numeric, (double)value * (Math.PI / 180.0));
            });
        }
    }
}
