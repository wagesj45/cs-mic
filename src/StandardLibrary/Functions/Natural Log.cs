using System;
using System.Collections.Generic;
using System.Text;

namespace CSMic.StandardLibrary.Functions
{
    public class Natural_Log: FunctionBase, ICodedFunction
    {
        public string Name
        {
            get
            {
                return "ln";
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

                return new FunctionValue(FunctionValueType.Numeric, Math.Log(number));
            });
        }
    }
}
