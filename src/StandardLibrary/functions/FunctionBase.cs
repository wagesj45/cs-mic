using CsMic;
using FunctionValueType = CsMic.FunctionValueType;

namespace CsMic.StandardLibrary.Functions
{
    public abstract class FunctionBase
    {
        public virtual IEnumerable<FunctionArgument> ExpectedArguments { get; }

        public virtual FunctionValue ReturnValue
        {
            get

            {
                return FunctionValue.NUMBER;
            }
        }

        public bool ArgumentCheck(params FunctionArgument[] args)
        {
            // Top level sanity checks.
            if (args == null || args.Length != this.ExpectedArguments.Count())
            {
                return false;
            }

            // Check each argument against what is expected.
            var expectedArgumentsArray = this.ExpectedArguments.ToArray();
            for (int i = 0; i < args.Length; i++)
            {
                var expectedArgument = expectedArgumentsArray[i];
                var argument = args[i];
                if (argument.Value == null || argument.Value.Value == null || argument.Value.Type != expectedArgument.Value.Type)
                {
                    return false;
                }

                if (argument.Value.Type == FunctionValueType.Numeric && argument.Value.Value is not decimal)
                {
                    return false;
                }

                if (argument.Value.Type == FunctionValueType.String && argument.Value.Value is not string)
                {
                    return false;
                }
            }

            // All checks passed.
            return true;
        }

        public FunctionValue Execute(FunctionArgument[] args, Func<FunctionArgument[], FunctionValue> action)
        {
            if (!ArgumentCheck(args))
            {
                return FunctionValue.NONE;
            }

            try
            {
                return action(args);
            }
            catch
            {
                return FunctionValue.NONE;
            }
        }
    }
}
