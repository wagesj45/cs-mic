using System;
using System.Collections.Generic;
using System.Text;

namespace CSMic.StandardLibrary.Functions
{
    public class Power : FunctionBase, ICodedFunction
    {
        public string Name
        {
            get
            {
                return "pow";
            }
        }

        public override IEnumerable<FunctionArgument> ExpectedArguments
        {
            get
            {
                yield return new FunctionArgument("base", FunctionValue.NUMBER);
                yield return new FunctionArgument("exponent", FunctionValue.NUMBER);
            }
        }

        public FunctionValue Execute(params FunctionArgument[] args)
        {
            return base.Execute(args, (_args) =>
            {
                var input = _args[0].Value;
                var input2 = _args[1].Value;
                double _base = Convert.ToDouble(input.Value);
                double exponent = Convert.ToDouble(input2.Value);

                return new FunctionValue(FunctionValueType.Numeric, Math.Pow(_base, exponent));
            });
        }
    }
}
