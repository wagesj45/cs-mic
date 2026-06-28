using System;
using System.Collections.Generic;
using System.Text;

namespace CSMic.StandardLibrary.Functions
{
    public class Log: FunctionBase, ICodedFunction
    {
        public string Name
        {
            get
            {
                return "log";
            }
        }

        public override IEnumerable<FunctionArgument> ExpectedArguments
        {
            get
            {
                yield return new FunctionArgument("value", FunctionValue.NUMBER);
                yield return new FunctionArgument("base", FunctionValue.NUMBER);
            }
        }

        public FunctionValue Execute(params FunctionArgument[] args)
        {
            return base.Execute(args, (_args) =>
            {
                var input = _args[0].Value;
                var input2 = _args[1].Value;
                double number = Convert.ToDouble(input.Value);
                double _baseNumber = Convert.ToDouble(input2.Value);

                return new FunctionValue(FunctionValueType.Numeric, Math.Log(number, _baseNumber));
            });
        }
    }
}
