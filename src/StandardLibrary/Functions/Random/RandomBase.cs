using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSMic.StandardLibrary.Functions.Random
{
    /// <summary>
    /// Provides shared random-number helpers for the standard-library random functions.
    /// </summary>
    public abstract class RandomBase : FunctionBase
    {
        // Provide a thread-local random to approximate Random.Shared in .NET Standard
        private static readonly System.Threading.ThreadLocal<System.Random> s_threadLocalRandom =
            new System.Threading.ThreadLocal<System.Random>(() => new System.Random());

        /// <summary>
        /// Gets the thread-local random number generator used by the random functions.
        /// </summary>
        protected static System.Random RandomNumberGenerator => s_threadLocalRandom.Value!;

        /// <summary>
        /// Returns a pseudo-random decimal in the half-open interval [0, 1).
        /// </summary>
        protected static decimal NextDecimal()
        {
            return Convert.ToDecimal(RandomNumberGenerator.NextDouble());
        }

        /// <summary>
        /// Returns a pseudo-random decimal sampled from a normal distribution centered at zero.
        /// </summary>
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
