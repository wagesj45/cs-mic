using CSMic;

namespace CSMic.StandardLibrary.Functions
{
    /// <summary>
    /// Represents the standard-library <c>abs</c> function.
    /// </summary>
    /// <remarks>
    /// The <c>abs</c> function evaluates a numeric expression and returns its absolute value.
    /// </remarks>
    public class AbsoluteValue : FunctionBase, ICodedFunction
    {
        /// <summary>
        /// Gets the expression-language name used to invoke this function.
        /// </summary>
        /// <value><c>abs</c>.</value>
        public string Name
        {
            get
            {
                return "abs";
            }
        }

        /// <summary>
        /// Gets the argument signature expected by the <c>abs</c> function.
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
        /// Executes the <c>abs</c> function.
        /// </summary>
        /// <param name="args">
        /// The evaluated arguments supplied to the function. Exactly one numeric argument is expected.
        /// </param>
        /// <returns>
        /// A numeric <see cref="FunctionValue"/> containing the absolute value of the input.
        /// </returns>
        public FunctionValue Execute(params FunctionArgument[] args)
        {
            return base.Execute(args, (_args) =>
            {
                var input = _args[0].Value;
                decimal number = Convert.ToDecimal(input.Value);

                return new FunctionValue(FunctionValueType.Numeric, Math.Abs(number));
            });
        }
    }
}
