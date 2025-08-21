using CSMic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSMic.StandardLibrary.Functions.NumberTheory
{
    public class LeastCommonMultiple : FunctionBase, ICodedFunction
    {
        public string Name
        {
            get
            {
                return "gcd";
            }
        }

        public override IEnumerable<FunctionArgument> ExpectedArguments
        {
            get
            {
                yield return new FunctionArgument("first", FunctionValue.NUMBER);
                yield return new FunctionArgument("second", FunctionValue.NUMBER);
            }
        }

        public FunctionValue Execute(params FunctionArgument[] args)
        {
            return base.Execute(args, (_args) =>
            {
                var inputFirst = _args[0].Value;
                var inputSecond = _args[1].Value;
                decimal first = Convert.ToDecimal(inputFirst.Value);
                decimal second = Convert.ToDecimal(inputSecond.Value);

                if (first <= 0 || second <= 0)
                {
                    return FunctionValue.ZERO;
                }

                if (Math.Floor(first) != first || Math.Floor(second) != second)
                {
                    return FunctionValue.ZERO;
                }

                return new FunctionValue(FunctionValueType.Numeric, EuclideanAlgorithm(first, second));
            });
        }

        public decimal EuclideanAlgorithm(decimal first, decimal second) 
        {
            if(second == 0)
            {
                return first;
            }

            return EuclideanAlgorithm(second, first % second);
        }
    }
}
