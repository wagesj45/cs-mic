namespace CSMic.StandardLibrary.Functions.Trigonometry.Hyperbolic
{
    /// <summary>
    /// Represents the standard-library <c>cosh</c> function.
    /// </summary>
    /// <remarks>
    /// The <c>cosh</c> function evaluates a numeric expression and returns its hyperbolic cosine.
    /// </remarks>
    public class Cosh : FunctionBase, ICodedFunction
    {

        /// <summary>
        /// Gets the expression-language name used to invoke this function.
        /// </summary>
        /// <value><c>cosh</c>.</value>
        public string Name
        {
            get
            {
                return "cosh";
            }
        }

        /// <summary>
        /// Gets the argument signature expected by the <c>cosh</c> function.
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
        /// Executes the <c>cosh</c> function.
        /// </summary>
        /// <param name="args">
        /// The evaluated arguments supplied to the function. Exactly one numeric argument is expected.
        /// </param>
        /// <returns>
        /// A numeric <see cref="FunctionValue"/> containing the hyperbolic cosine of the input value.
        /// </returns>
        public FunctionValue Execute(params FunctionArgument[] args)
        {
            return Execute(args, (_args) =>
            {
                var input = _args[0].Value;
                decimal value = Convert.ToDecimal(input.Value);

                return new FunctionValue(FunctionValueType.Numeric, Math.Cosh((double)value));
            });
        }
    }
}
