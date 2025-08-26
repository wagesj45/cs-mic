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

                // Convert radians to degrees (compatible with .NET Standard)
                return new FunctionValue(FunctionValueType.Numeric, (double)value * (180.0 / Math.PI));
            });
        }
    }
}
