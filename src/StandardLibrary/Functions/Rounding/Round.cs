namespace CSMic.StandardLibrary.Functions.Rounding
{
    public class Round : FunctionBase, ICodedFunction
    {

        public string Name
        {
            get
            {
                return "round";
            }
        }

        public override IEnumerable<FunctionArgument> ExpectedArguments
        {
            get
            {
                yield return new FunctionArgument("value", FunctionValue.NUMBER);
            }
        }

        public FunctionValue Execute(params FunctionArgument[] args)
        {
            return Execute(args, (_args) =>
            {
                var inputValue = _args[0].Value;
                decimal value = Convert.ToDecimal(inputValue.Value);
                var inputPrecision = _args[1].Value;
                decimal precision = Convert.ToDecimal(inputValue.Value);
                precision = Math.Round(precision);
                int precisionInt = Convert.ToInt32(precision);

                return new FunctionValue(FunctionValueType.Numeric, Math.Round(value, precisionInt));
            });
        }
    }
}
