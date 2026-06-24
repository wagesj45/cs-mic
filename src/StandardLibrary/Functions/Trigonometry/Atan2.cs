namespace CSMic.StandardLibrary.Functions.Trigonometry
{
    /// <summary>
    /// Represents the standard-library <c>atan2</c> function.
    /// </summary>
    /// <remarks>
    /// The <c>atan2</c> function evaluates two numeric expressions and returns the arctangent of their quotient.
    /// </remarks>
    public class Atan2 : FunctionBase, ICodedFunction
    {

        /// <summary>
        /// Gets the expression-language name used to invoke this function.
        /// </summary>
        /// <value><c>atan2</c>.</value>
        public string Name
        {
            get
            {
                return "atan2";
            }
        }

        /// <summary>
        /// Gets the argument signature expected by the <c>atan2</c> function.
        /// </summary>
        /// <value>Two numeric arguments named <c>y</c> and <c>x</c>.</value>
        public override IEnumerable<FunctionArgument> ExpectedArguments
        {
            get
            {
                yield return new FunctionArgument("y", FunctionValue.NUMBER);
                yield return new FunctionArgument("x", FunctionValue.NUMBER);
            }
        }

        /// <summary>
        /// Executes the <c>atan2</c> function.
        /// </summary>
        /// <param name="args">
        /// The evaluated arguments supplied to the function. Exactly two numeric arguments are expected.
        /// </param>
        /// <returns>
        /// A numeric <see cref="FunctionValue"/> containing the arctangent of the supplied coordinates.
        /// </returns>
        public FunctionValue Execute(params FunctionArgument[] args)
        {
            return Execute(args, (_args) =>
            {
                var inputFirst = _args[0].Value;
                decimal valueFirst = Convert.ToDecimal(inputFirst.Value);
                var inputSecond = _args[1].Value;
                decimal valueSecond = Convert.ToDecimal(inputSecond.Value);

                return new FunctionValue(FunctionValueType.Numeric, Math.Atan2((double)valueFirst, (double)valueSecond));
            });
        }
    }
}
