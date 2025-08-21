using CSMic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSMic.StandardLibrary.Functions.NumberTheory
{
    public class BinomialCoefficient : FunctionBase, ICodedFunction
    {
        public string Name
        {
            get
            {
                return "ncr";
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

                if (first < 0 || first > 20 || second < 0 || second > 20 || first - second < 0)
                {
                    return FunctionValue.ZERO;
                }

                if(Math.Floor(first) != first || Math.Floor(second) != second)
                {
                    return FunctionValue.ZERO;
                }

                int n = Convert.ToInt32(first);
                int r = Convert.ToInt32(second);
                decimal nFac = Factorial.INTEGER_FACTORIAL_LOOKUP[n];
                decimal rFac = Factorial.INTEGER_FACTORIAL_LOOKUP[r];
                decimal nmrFac = Factorial.INTEGER_FACTORIAL_LOOKUP[n - r];

                try
                {
                    decimal combinations = (nFac) / (rFac * nmrFac);

                    return new FunctionValue(FunctionValueType.Numeric, combinations);
                }
                catch
                {
                    // Last chance emergency fail if the decimal value is exceeded.
                    return FunctionValue.ZERO;
                }
            });
        }
    }
}
