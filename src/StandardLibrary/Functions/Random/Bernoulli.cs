using CSMic;
using CSMic.StandardLibrary.Functions.Random;

namespace CSMic.StandardLibrary.Functions.Random
{
    public class Bernoulli : RandomBase, ICodedFunction
    {
        public string Name
        {
            get
            {
                return "bern";
            }
        }

        public override IEnumerable<FunctionArgument> ExpectedArguments
        {
            get
            {
                yield return new FunctionArgument("p", FunctionValue.NUMBER);
            }
        }

        public FunctionValue Execute(params FunctionArgument[] args)
        {
            return base.Execute(args, (_args) =>
            {
                var input = _args[0].Value;
                decimal pValue = Convert.ToDecimal(input.Value);

                if(pValue < 0m || pValue > 1m)
                {
                    throw new ArgumentOutOfRangeException(nameof(pValue), "The p value must be between 0 and 1.");
                }

                if(NextDecimal() < pValue)
                {
                    return FunctionValue.TRUE;
                }

                return FunctionValue.FALSE;
            });
        }
    }
}
