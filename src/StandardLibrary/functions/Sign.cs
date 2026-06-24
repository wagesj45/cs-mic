using CSMic;


namespace CSMic.StandardLibrary.Functions
{
    /// <summary> A function that returns <c>1</c> if the expression is positive and <c>-1</c> if it is negative. </summary>
    public class Sign : FunctionBase, ICodedFunction
    {
        /// <summary> (Immutable) The return value representing "positive". </summary>
        private const decimal POSITIVE = 1;
        /// <summary> (Immutable) The return value representing "negative". </summary>
        private const decimal NEGATIVE = -1;

        /// <summary> Gets the name of the function. </summary>
        /// <value> sign. </value>
        public string Name
        {
            get
            {
                return "sign";
            }
        }

        /// <summary> Gets the expected arguments. </summary>
        /// <value> The expected arguments. </value>
        public override IEnumerable<FunctionArgument> ExpectedArguments
        {
            get
            {
                yield return new FunctionArgument("value", FunctionValue.NUMBER);
            }
        }

        /// <summary> Executes the function with the given arguments. </summary>
        /// <param name="args"> A variable-length parameters list containing arguments. </param>
        /// <returns> A <see cref="FunctionValue"/> representing the result of the function execution. </returns>
        public FunctionValue Execute(params FunctionArgument[] args)
        {
            return base.Execute(args, (_args) =>
            {
                var input = _args[0].Value;
                decimal number = Convert.ToDecimal(input.Value);

                return new FunctionValue(FunctionValueType.Numeric, number >= 0 ? POSITIVE : NEGATIVE);
            });
        }
    }
}
