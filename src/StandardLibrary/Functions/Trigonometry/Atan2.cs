namespace CSMic.StandardLibrary.Functions.Trigonometry
{
    public class Atan2 : FunctionBase, ICodedFunction
    {

        public string Name
        {
            get
            {
                return "atan2";
            }
        }

        public override IEnumerable<FunctionArgument> ExpectedArguments
        {
            get
            {
                yield return new FunctionArgument("y", FunctionValue.NUMBER);
                yield return new FunctionArgument("x", FunctionValue.NUMBER);
            }
        }

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
