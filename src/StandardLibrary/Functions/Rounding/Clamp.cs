namespace CSMic.StandardLibrary.Functions.Rounding
{
    public class Clamp : FunctionBase, ICodedFunction
    {

        public string Name
        {
            get
            {
                return "clamp";
            }
        }

        public override IEnumerable<FunctionArgument> ExpectedArguments
        {
            get
            {
                yield return new FunctionArgument("value", FunctionValue.NUMBER);
                yield return new FunctionArgument("low", FunctionValue.NUMBER);
                yield return new FunctionArgument("high", FunctionValue.NUMBER);
            }
        }

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
