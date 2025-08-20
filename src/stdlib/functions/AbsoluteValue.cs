using csmic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stdlib.functions
{
    public class AbsoluteValue : ICodedFunction
    {
        public IEnumerable<FunctionArgument> ExpectedArguments
        {
            get
            {
                yield return new FunctionArgument(
                    name: "value",
                    fv: new FunctionValue(ValueType.Numeric, 0m)
                );
            }
        }

        public FunctionValue ReturnValue => new FunctionValue(ValueType.Numeric, 0m);

        public FunctionValue Execute(params FunctionArgument[] args)
        {
            if (args == null || args.Length < 1 || args[0] == null || args[0].Value == null)
            {
                return new FunctionValue(ValueType.None, null);
            }

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
