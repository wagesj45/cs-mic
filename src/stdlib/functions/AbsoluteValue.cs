using csmic;
using FunctionValueType = csmic.FunctionValueType;

namespace stdlib.functions
{
    public class AbsoluteValue : FunctionBase, ICodedFunction
    {
        public override IEnumerable<FunctionArgument> ExpectedArguments
        {
            get
            {
                yield return new FunctionArgument("value", FunctionValue.NUMBER);
            }
        }

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
