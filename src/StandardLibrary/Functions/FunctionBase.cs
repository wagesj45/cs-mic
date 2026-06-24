using CSMic;

namespace CSMic.StandardLibrary.Functions
{
    /// <summary>
    ///  Provides shared argument validation and execution handling for standard-library functions.
    /// </summary>
    /// <remarks>
    ///  Derive from this class when implementing an <see cref="ICodedFunction"/> that follows the
    ///  standard-library function contract.
    /// </remarks>
    public abstract class FunctionBase
    {
        /// <summary> Gets the argument signature expected by the function. </summary>
        /// <value> The ordered collection of arguments required by the function. </value>
        public virtual IEnumerable<FunctionArgument> ExpectedArguments { get; }

        /// <summary> Gets the return type produced by the function. </summary>
        /// <value>
        ///  The expected return value type. The default is <see cref="FunctionValue.NUMBER"/>.
        /// </value>
        public virtual FunctionValue ReturnValue
        {
            get
            {
                return FunctionValue.NUMBER;
            }
        }

        /// <summary>
        ///  Determines whether the supplied arguments satisfy the function's expected signature.
        /// </summary>
        /// <param name="args"> The evaluated arguments supplied to the function. </param>
        /// <returns>
        ///  <see langword="true"/> if the supplied arguments match the expected count and value types;
        ///  otherwise, <see langword="false"/>.
        /// </returns>
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

        /// <summary> Validates and executes a standard-library function body. </summary>
        /// <param name="args"> The evaluated arguments supplied to the function. </param>
        /// <param name="action"> The function implementation to execute after argument validation
        ///  succeeds. </param>
        /// <returns>
        ///  The result returned by <paramref name="action"/>, or <see cref="FunctionValue.NONE"/> if
        ///  validation fails or execution throws an exception.
        /// </returns>
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
