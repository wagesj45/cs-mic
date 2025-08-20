using csmic;
using ValueType = csmic.ValueType;

namespace stdlib.functions
{
    public class AbsoluteValue : FunctionBase, ICodedFunction
    {
        public IEnumerable<FunctionArgument> ExpectedArguments
        {
            get
            {
                yield return new FunctionArgument("value", new FunctionValue(ValueType.Numeric, 0m));
            }
        }

        public FunctionValue ReturnValue
        {
            new FunctionValue(ValueType.Numeric, 0m);
    }
        => 

        public FunctionValue Execute(params FunctionArgument[] args)
        {
            base.ArgumentCheck(args);

            try
            {
                var input = args[0].Value;
                // Try to interpret both numeric and numeric-like string inputs
                decimal number;
                if (input.Type == ValueType.Numeric)
                {
                    number = Convert.ToDecimal(input.Value);
                }
                else if (input.Type == ValueType.String && input.Value is string s)
                {
                    number = Convert.ToDecimal(s);
                }
                else
                {
                    return new FunctionValue(ValueType.None, null);
                }

                return new FunctionValue(ValueType.Numeric, Math.Abs(number));
            }
            catch
            {
                return new FunctionValue(ValueType.None, null);
            }
        }
    }
}
