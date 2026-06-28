using System;
using System.Collections.Generic;
using System.Text;

namespace CSMic.StandardLibrary.Functions
{
    public class Lerp: FunctionBase, ICodedFunction
    {
        public string Name
        {
            get
            {
                return "lerp";
            }
        }

        public override IEnumerable<FunctionArgument> ExpectedArguments
        {
            get
            {
                yield return new FunctionArgument("start", FunctionValue.NUMBER);
                yield return new FunctionArgument("end", FunctionValue.NUMBER);
                yield return new FunctionArgument("ammount", FunctionValue.NUMBER);
            }
        }

        public FunctionValue Execute(params FunctionArgument[] args)
        {
            return base.Execute(args, (_args) =>
            {
                var input = _args[0].Value;
                var input2 = _args[1].Value;
                var input3 = _args[2].Value;

                decimal start = Convert.ToDecimal(input);
                decimal end = Convert.ToDecimal(input2);
                decimal ammount = Convert.ToDecimal(input3);

                if (start == end)
                {
                    return new FunctionValue(FunctionValueType.Numeric, start);
                }

                var lerp = start + (ammount * (end - start));

                return new FunctionValue(FunctionValueType.Numeric, lerp);
            });
        }
    }
}
