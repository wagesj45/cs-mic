using CSMic;

namespace CSMic.StandardLibrary.Functions
{
    /// <summary> A base class that handles base function handling. </summary>
    public abstract class FunctionBase
    {
        /// <summary> Gets the expected arguments. </summary>
        /// <value> The expected arguments. </value>
        public virtual IEnumerable<FunctionArgument> ExpectedArguments { get; }

        /// <summary> Gets the return value. </summary>
        /// <value> The return value. </value>
        public virtual FunctionValue ReturnValue
        {
            get

            {
                return FunctionValue.NUMBER;
            }
        }

        /// <summary> Checks the provided arguments to ensure the function contract is honored. </summary>
        /// <param name="args"> A variable-length parameters list containing arguments. </param>
        /// <returns> True if it succeeds, false if it fails. </returns>
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

        /// <summary> Executes a standard library function. </summary>
        /// <param name="args"> A variable-length parameters list containing arguments. </param>
        /// <param name="action"> The functions action body. </param>
        /// <returns> A <see cref="FunctionValue"/>. </returns>
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
