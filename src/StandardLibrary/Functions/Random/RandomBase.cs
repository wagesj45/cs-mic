using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSMic.StandardLibrary.Functions.Random
{
    public abstract class RandomBase : FunctionBase
    {
        protected static System.Random RandomNumberGenerator
        {
            get
            {
                return System.Random.Shared;
            }
        }

        protected static decimal NextDecimal()
        {
            return Convert.ToDecimal(RandomNumberGenerator.NextDouble());
        }

        protected static decimal NextDecimalNormal()
        {
            double u1 = 1.0 - RandomNumberGenerator.NextDouble();
            double u2 = 1.0 - RandomNumberGenerator.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                                   Math.Sin(2.0 * Math.PI * u2);
            return Convert.ToDecimal(randStdNormal);
        }
    }
}
