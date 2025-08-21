namespace CSMic.StandardLibrary.Functions.Angle
{
    public class Degrees : FunctionBase, ICodedFunction
    {

        public string Name
        {
            get
            {
                return "degrees";
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
                var input = _args[0].Value;
                decimal value = Convert.ToDecimal(input.Value);

                return new FunctionValue(FunctionValueType.Numeric, double.DegreesToRadians((double)value));
            });
        }
    }
}
