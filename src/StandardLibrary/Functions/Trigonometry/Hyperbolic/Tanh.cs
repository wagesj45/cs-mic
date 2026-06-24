namespace CSMic.StandardLibrary.Functions.Trigonometry.Hyperbolic
{
    /// <summary>
    /// Represents the standard-library <c>tanh</c> function.
    /// </summary>
    /// <remarks>
    /// The <c>tanh</c> function evaluates a numeric expression and returns its hyperbolic tangent.
    /// </remarks>
    public class Tanh : FunctionBase, ICodedFunction
    {

        /// <summary>
        /// Gets the expression-language name used to invoke this function.
        /// </summary>
        /// <value><c>tanh</c>.</value>
        public string Name
        {
            get
            {
                return "tanh";
            }
        }

        /// <summary>
        /// Gets the argument signature expected by the <c>tanh</c> function.
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
        /// Executes the <c>tanh</c> function.
        /// </summary>
        /// <param name="args">
        /// The evaluated arguments supplied to the function. Exactly one numeric argument is expected.
        /// </param>
        /// <returns>
        /// A numeric <see cref="FunctionValue"/> containing the hyperbolic tangent of the input value.
        /// </returns>
        public FunctionValue Execute(params FunctionArgument[] args)
        {
            return Execute(args, (_args) =>
            {
                var input = _args[0].Value;
                decimal value = Convert.ToDecimal(input.Value);

                return new FunctionValue(FunctionValueType.Numeric, Math.Tanh((double)value));
            });
        }
    }
}
