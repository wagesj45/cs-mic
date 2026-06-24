namespace CSMic.StandardLibrary.Functions.Angle
{
    /// <summary>
    /// Represents the standard-library <c>wrapangle</c> function.
    /// </summary>
    /// <remarks>
    /// The <c>wrapangle</c> function evaluates a numeric value and wraps it into the requested period.
    /// </remarks>
    public class WrapAngle : FunctionBase, ICodedFunction
    {
        /// <summary>
        /// Gets the expression-language name used to invoke this function.
        /// </summary>
        /// <value><c>wrapangle</c>.</value>
        public string Name
        {
            get
            {
                return "wrapangle";
            }
        }

        /// <summary>
        /// Gets the argument signature expected by the <c>wrapangle</c> function.
        /// </summary>
        /// <value>
        /// Three numeric arguments named <c>value</c>, <c>periodStart</c>, and <c>periodEnd</c>.
        /// </value>
        public override IEnumerable<FunctionArgument> ExpectedArguments
        {
            get
            {
                yield return new FunctionArgument("value", FunctionValue.NUMBER);
                yield return new FunctionArgument("periodStart", FunctionValue.NUMBER);
                yield return new FunctionArgument("periodEnd", FunctionValue.NUMBER);
            }
        }

        /// <summary>
        /// Executes the <c>wrapangle</c> function.
        /// </summary>
        /// <param name="args">
        /// The evaluated arguments supplied to the function. Exactly three numeric arguments are expected.
        /// </param>
        /// <returns>
        /// A numeric <see cref="FunctionValue"/> containing the wrapped value within the supplied period.
        /// </returns>
        public FunctionValue Execute(params FunctionArgument[] args)
        {
            return Execute(args, (_args) =>
            {
                var inputValue = _args[0].Value;
                decimal value = Convert.ToDecimal(inputValue.Value);
                var inputPeriodStart = _args[1].Value;
                decimal periodStart = Convert.ToDecimal(inputPeriodStart.Value);
                var inputPeriodEnd = _args[2].Value;
                decimal periodEnd = Convert.ToDecimal(inputPeriodEnd.Value);

                // Perform modulo and shift into [periodStart, periodEnd)
                decimal width = periodEnd - periodStart;
                decimal modulus = value % width;
                modulus = modulus < 0 ? modulus + width : modulus;

                return new FunctionValue(FunctionValueType.Numeric, periodStart + modulus);
            });
        }
    }
}
