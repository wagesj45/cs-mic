using csmic;
using ValueType = csmic.ValueType;

namespace stdlib.functions
{
    public abstract class FunctionBase
    {
        public virtual IEnumerable<FunctionArgument> ExpectedArguments { get; }

        internal bool ArgumentCheck(params FunctionArgument[] args)
        {
            if (args == null || args.Length != this.ExpectedArguments.Count())
            {
                return false;
            }

            var expectedArgumentsArray = this.ExpectedArguments.ToArray();
            for (int i = 0; i < args.Length; i++)
            {
                var expectedArgument = expectedArgumentsArray[i];
                var argument = args[i];
                if(argument.Value == null || argument.Value.Type != expectedArgument.Value.Type)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
