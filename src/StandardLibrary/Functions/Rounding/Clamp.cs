namespace CSMic.StandardLibrary.Functions.Rounding
{
    /// <summary>
    /// Represents the standard-library <c>clamp</c> function.
    /// </summary>
    /// <remarks>
    /// The <c>clamp</c> function evaluates a numeric value and limits it to the supplied bounds.
    /// </remarks>
    public class Clamp : FunctionBase, ICodedFunction
    {

        /// <summary>
        /// Gets the expression-language name used to invoke this function.
        /// </summary>
        /// <value><c>clamp</c>.</value>
        public string Name
        {
            get
            {
                return "clamp";
            }
        }

        /// <summary>
        /// Gets the argument signature expected by the <c>clamp</c> function.
        /// </summary>
        /// <value>Three numeric arguments named <c>value</c>, <c>low</c>, and <c>high</c>.</value>
        public override IEnumerable<FunctionArgument> ExpectedArguments
        {
            get
            {
                yield return new FunctionArgument("value", FunctionValue.NUMBER);
                yield return new FunctionArgument("low", FunctionValue.NUMBER);
                yield return new FunctionArgument("high", FunctionValue.NUMBER);
            }
        }

        /// <summary>
        /// Executes the <c>clamp</c> function.
        /// </summary>
        /// <param name="args">
        /// The evaluated arguments supplied to the function. Exactly three numeric arguments are expected.
        /// </param>
        /// <returns>
        /// A numeric <see cref="FunctionValue"/> containing the input value constrained to the supplied range.
        /// </returns>
        public FunctionValue Execute(params FunctionArgument[] args)
        {
            return Execute(args, (_args) =>
            {
                var inputValue = _args[0].Value;
                decimal value = Convert.ToDecimal(inputValue.Value);
                var inputLow = _args[1].Value;
                decimal low = Convert.ToDecimal(inputLow.Value);
                var inputHigh = _args[2].Value;
                decimal high = Convert.ToDecimal(inputHigh.Value);

                return new FunctionValue(FunctionValueType.Numeric, value < low ? low : value > high ? high : value);
            });
        }
    }
}
