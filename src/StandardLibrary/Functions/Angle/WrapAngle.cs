namespace CSMic.StandardLibrary.Functions.Angle
{
    public class WrapAngle : FunctionBase, ICodedFunction
    {
        public string Name
        {
            get
            {
                return "wrapangle";
            }
        }

        public override IEnumerable<FunctionArgument> ExpectedArguments
        {
            get
            {
                yield return new FunctionArgument("value", FunctionValue.NUMBER);
                yield return new FunctionArgument("periodStart", FunctionValue.NUMBER);
                yield return new FunctionArgument("periodEnd", FunctionValue.NUMBER);
            }
        }

        public FunctionValue Execute(params FunctionArgument[] args)
        {
            return Execute(args, (_args) =>
            {
                var inputValue = _args[0].Value;
                decimal value = Convert.ToDecimal(inputValue.Value);
                var inputPeriodStart = _args[1].Value;
                decimal periodStart = Convert.ToDecimal(inputPeriodStart.Value);
                var inputPeriodEnd = _args[2].Value;
                decimal periodEnd = Convert.ToDecimal(inputPeriodEnd.Value);

                // Perform modulo and shift into [periodStart, periodEnd)
                decimal width = periodEnd - periodStart;
                decimal modulus = value % width;
                modulus = modulus < 0 ? modulus + width : modulus;

                return new FunctionValue(FunctionValueType.Numeric, periodStart + modulus);
            });
        }
    }
}
