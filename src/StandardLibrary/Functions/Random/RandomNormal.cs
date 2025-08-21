using CSMic;
using CSMic.StandardLibrary.Functions.Random;

namespace CSMic.StandardLibrary.Functions.Random
{
    public class RandomNormal : RandomBase, ICodedFunction
    {
        public string Name
        {
            get
            {
                return "randn";
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
                return new FunctionValue(FunctionValueType.Numeric, NextDecimalNormal());
            });
        }
    }
}
