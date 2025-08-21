using CSMic;
using CSMic.StandardLibrary.Functions.Random;

namespace CSMic.StandardLibrary.Functions.Random
{
    public class RandomUniformSpread : RandomBase, ICodedFunction
    {
        public string Name
        {
            get
            {
                return "rands";
            }
        }

        public override IEnumerable<FunctionArgument> ExpectedArguments
        {
            get
            {
                yield return new FunctionArgument("lower", FunctionValue.NUMBER);
                yield return new FunctionArgument("upper", FunctionValue.NUMBER);
            }
        }

        public FunctionValue Execute(params FunctionArgument[] args)
        {
            return base.Execute(args, (_args) =>
            {
                var inputLower = _args[0].Value;
                var inputUpper = _args[1].Value;
                decimal lower = Convert.ToDecimal(inputLower.Value);
                decimal upper = Convert.ToDecimal(inputUpper.Value);

                if(upper <= lower)
                {
                    return FunctionValue.ZERO;
                }

                return new FunctionValue(FunctionValueType.Numeric, NextDecimal() * (upper - lower));
            });
        }
    }
}
