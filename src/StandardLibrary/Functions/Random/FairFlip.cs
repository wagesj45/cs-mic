using CSMic;
using CSMic.StandardLibrary.Functions.Random;

namespace CSMic.StandardLibrary.Functions.Random
{
    public class FairFlip : RandomBase, ICodedFunction
    {
        public string Name
        {
            get
            {
                return "flip";
            }
        }

        public override IEnumerable<FunctionArgument> ExpectedArguments
        {
            get
            {
                yield break;
            }
        }

        public FunctionValue Execute(params FunctionArgument[] args)
        {
            return base.Execute(args, (_args) =>
            {
                if(NextDecimal() < 0.5m)
                {
                    return FunctionValue.TRUE;
                }

                return FunctionValue.FALSE;
            });
        }
    }
}
