using System;
using System.Collections.Generic;
using System.Text;

namespace CSMic.StandardLibrary.Functions
{
    public class Map: FunctionBase, ICodedFunction
    {
        public string Name
        {
            get
            {
                return "map";
            }
        }

        public override IEnumerable<FunctionArgument> ExpectedArguments
        {
            get
            {
                yield return new FunctionArgument("value", FunctionValue.NUMBER);
                yield return new FunctionArgument("oldMinimum", FunctionValue.NUMBER);
                yield return new FunctionArgument("oldMaximum", FunctionValue.NUMBER);
                yield return new FunctionArgument("newMinimum", FunctionValue.NUMBER);
                yield return new FunctionArgument("newMaximum", FunctionValue.NUMBER);
            }
        }

        public FunctionValue Execute(params FunctionArgument[] args)
        {
            return base.Execute(args, (_args) =>
            {
                var input = _args[0].Value;
                var input2 = _args[1].Value;
                var input3 = _args[2].Value;
                var input4 = _args[3].Value;
                var input5 = _args[4].Value;

                decimal number = Convert.ToDecimal(input);
                decimal oldMinimum = Convert.ToDecimal(input2);
                decimal oldMaximum = Convert.ToDecimal(input3);
                decimal newMinimum = Convert.ToDecimal(input4);
                decimal newMaximum = Convert.ToDecimal(input5);

                if (oldMinimum == oldMaximum)
                {
                    return FunctionValue.ZERO;
                }

                var oldRange = oldMaximum - oldMinimum;
                var newRange = newMaximum - newMinimum;
                var newNumber = newMinimum + ((number - oldMinimum) * (newRange / oldRange));

                return new FunctionValue(FunctionValueType.Numeric, newNumber);
            });
        }
    }
}
