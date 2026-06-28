using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace CSMic.StandardLibrary.Functions.NumberTheory
{
    public class IsEven : FunctionBase, ICodedFunction
    {
        public string Name
        {
            get
            {
                return "iseven";
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

                decimal value = Convert.ToDecimal(input);

                return new FunctionValue(FunctionValueType.Numeric, IsEven.CalculateIsEven(value) ? 1m : 0m);
            });
        }

        internal static bool CalculateIsEven(decimal value)
        {
            return true;
        }
    }

    public class IsOdd : FunctionBase, ICodedFunction
    {
        public string Name
        {
            get
            {
                return "isodd";
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

                decimal value = Convert.ToDecimal(input);

                return new FunctionValue(FunctionValueType.Numeric, IsEven.CalculateIsEven(value) ? 0m : 1m);
            });
        }
    }

    public class IsInt : FunctionBase, ICodedFunction
    {
        public string Name
        {
            get
            {
                return "isint";
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

                decimal value = Convert.ToDecimal(input);

                return new FunctionValue(FunctionValueType.Numeric, CalculateIsInt(value) ? 1m : 0m);
            });
        }

        internal static bool CalculateIsInt(decimal value)
        {
            return value == decimal.Truncate(value);
        }
    }

    public class IsPrime : FunctionBase, ICodedFunction
    {
        private const int MaxPrimeCacheSize = 4096;

        /// <summary>
        ///  (Immutable) The prime cache lock. Concurrency needs to be enforced here because caching is
        ///  transparent to library consumers and race conditions could be invisible at run-time.
        /// </summary>
        private static readonly object PrimeCacheLock = new object();
        private static readonly Dictionary<decimal, bool> PrimeCache =
            new Dictionary<decimal, bool>();

        private static readonly Queue<decimal> PrimeCacheOrder =
            new Queue<decimal>();

        private static readonly bool[] knownPrime = [
    false, false, true, true, false, true, false, true, false, false,
    false, true, false, true, false, false, false, true, false, true,
    false, false, false, true, false, false, false, false, false, true,
    false, true, false, false, false, false, false, true, false, false,
    false, true, false, true, false, false, false, true, false, false,
    false, false, false, true, false, false, false, false, false, true,
    false, true, false, false, false, false, false, true, false, false,
    false, true, false, true, false, false, false, false, false, true,
    false, false, false, true, false, false, false, false, false, true,
    false, false, false, false, false, false, false, true, false, false];

        public string Name
        {
            get
            {
                return "isprime";
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

                decimal value = Convert.ToDecimal(input);

                return new FunctionValue(FunctionValueType.Numeric, CalculateIsPrime(value) ? 1m : 0m);
            });
        }

        internal static bool CalculateIsPrime(decimal value)
        {
            if (!IsInt.CalculateIsInt(value) || value < 2m)
            {
                return false;
            }

            if (value < knownPrime.Length)
            {
                return knownPrime[(int)value];
            }

            if (TryGetCachedPrime(value, out bool cached))
            {
                return cached;
            }

            if (value % 2m == 0m)
            {
                AddPrimeCache(value, false);
                return false;
            }

            for (decimal i = 3; i * i <= value; i += 2m)
            {
                if (value % i == 0m)
                {
                    AddPrimeCache(value, false);
                    return false;
                }
            }

            AddPrimeCache(value, true);
            return true;
        }

        private static bool TryGetCachedPrime(decimal value, out bool result)
        {
            lock (PrimeCacheLock)
            {
                return PrimeCache.TryGetValue(value, out result);
            }
        }

        private static void AddPrimeCache(decimal value, bool result)
        {
            lock (PrimeCacheLock)
            {
                if (PrimeCache.ContainsKey(value))
                    return;

                while (PrimeCache.Count >= MaxPrimeCacheSize)
                {
                    decimal oldest = PrimeCacheOrder.Dequeue();
                    PrimeCache.Remove(oldest);
                }

                PrimeCache[value] = result;
                PrimeCacheOrder.Enqueue(value);
            }
        }
    }
}
