using System;
using System.Collections.Generic;
using System.Text;

namespace CSMic.StandardLibrary.Functions
{
    public class Normalize : FunctionBase, ICodedFunction
    {
        public string Name
        {
            get
            {
                return "normalize";
            }
        }

        public override IEnumerable<FunctionArgument> ExpectedArguments
        {
            get
            {
                yield return new FunctionArgument("value", FunctionValue.NUMBER);
                yield return new FunctionArgument("minimum", FunctionValue.NUMBER);
                yield return new FunctionArgument("maximum", FunctionValue.NUMBER);
            }
        }

        public FunctionValue Execute(params FunctionArgument[] args)
        {
            return base.Execute(args, (_args) =>
            {
                var input = _args[0].Value;
                var input2 = _args[1].Value;
                var input3 = _args[2].Value;

                decimal number = Convert.ToDecimal(input);
                decimal minimum = Convert.ToDecimal(input2);
                decimal maximum = Convert.ToDecimal(input3);

                if (minimum == maximum)
                {
                    return FunctionValue.ZERO;
                }

                var normalization = (number - minimum) / (maximum - minimum);

                return new FunctionValue(FunctionValueType.Numeric, normalization);
            });
        }
    }
}
