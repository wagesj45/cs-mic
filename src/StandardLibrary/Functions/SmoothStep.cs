using System;
using System.Collections.Generic;
using System.Text;

namespace CSMic.StandardLibrary.Functions
{
    public class SmoothStep : FunctionBase, ICodedFunction
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
                yield return new FunctionArgument("startEdge", FunctionValue.NUMBER);
                yield return new FunctionArgument("endEdge", FunctionValue.NUMBER);
                yield return new FunctionArgument("value", FunctionValue.NUMBER);
            }
        }

        public FunctionValue Execute(params FunctionArgument[] args)
        {
            return base.Execute(args, (_args) =>
            {
                var input = _args[0].Value;
                var input2 = _args[1].Value;
                var input3 = _args[2].Value;

                decimal startEdge = Convert.ToDecimal(input);
                decimal endEdge = Convert.ToDecimal(input2);
                decimal value = Convert.ToDecimal(input3);

                var normalization = Math.Clamp((value - startEdge) / (endEdge - startEdge), 0, 1);
                var polynomialization = normalization * normalization * (3 - (2 * normalization));

                return new FunctionValue(FunctionValueType.Numeric, polynomialization);
            });
        }
    }
}
