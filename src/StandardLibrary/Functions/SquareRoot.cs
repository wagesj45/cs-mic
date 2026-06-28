using System;
using System.Collections.Generic;
using System.Text;

namespace CSMic.StandardLibrary.Functions
{
    public class SquareRoot: FunctionBase, ICodedFunction
    {
        public string Name
        {
            get
            {
                return "sqrt";
            }
        }

        public override IEnumerable<FunctionArgument> ExpectedArguments
        {
            get
            {
                yield return new FunctionArgument("value", FunctionValue.NUMBER);
            }
        }

        public FunctionValue Execute(params FunctionArgument[] args)
        {
            return base.Execute(args, (_args) =>
            {
                var input = _args[0].Value;
                double number = Convert.ToDouble(input.Value);

                return new FunctionValue(FunctionValueType.Numeric, Math.Sqrt(number));
            });
        }
    }
}
