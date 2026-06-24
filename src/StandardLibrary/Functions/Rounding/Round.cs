namespace CSMic.StandardLibrary.Functions.Rounding
{
    /// <summary>
    /// Represents the standard-library <c>round</c> function.
    /// </summary>
    /// <remarks>
    /// The <c>round</c> function evaluates a numeric value and rounds it to the requested precision.
    /// </remarks>
    public class Round : FunctionBase, ICodedFunction
    {

        /// <summary>
        /// Gets the expression-language name used to invoke this function.
        /// </summary>
        /// <value><c>round</c>.</value>
        public string Name
        {
            get
            {
                return "round";
            }
        }

        /// <summary>
        /// Gets the argument signature expected by the <c>round</c> function.
        /// </summary>
        /// <value>Two numeric arguments named <c>value</c> and <c>precision</c>.</value>
        public override IEnumerable<FunctionArgument> ExpectedArguments
        {
            get
            {
                yield return new FunctionArgument("value", FunctionValue.NUMBER);
                yield return new FunctionArgument("precision", FunctionValue.NUMBER);
            }
        }

        /// <summary>
        /// Executes the <c>round</c> function.
        /// </summary>
        /// <param name="args">
        /// The evaluated arguments supplied to the function. Exactly two numeric arguments are expected.
        /// </param>
        /// <returns>
        /// A numeric <see cref="FunctionValue"/> containing the input value rounded to the requested precision.
        /// </returns>
        public FunctionValue Execute(params FunctionArgument[] args)
        {
            return Execute(args, (_args) =>
            {
                var inputValue = _args[0].Value;
                decimal value = Convert.ToDecimal(inputValue.Value);
                var inputPrecision = _args[1].Value;
                decimal precision = Convert.ToDecimal(inputPrecision.Value);
                precision = Math.Round(precision);
                int precisionInt = Convert.ToInt32(precision);

                return new FunctionValue(FunctionValueType.Numeric, Math.Round(value, precisionInt));
            });
        }
    }
}
