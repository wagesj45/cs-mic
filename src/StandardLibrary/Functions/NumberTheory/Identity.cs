using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace CSMic.StandardLibrary.Functions.NumberTheory
{
    /// <summary>
    /// Represents the standard-library <c>iseven</c> function.
    /// </summary>
    /// <remarks>
    /// The <c>iseven</c> function evaluates a numeric expression and returns <c>1</c> when the value is even;
    /// otherwise, it returns <c>0</c>.
    /// </remarks>
    public class IsEven : FunctionBase, ICodedFunction
    {
        /// <summary>
        /// Gets the expression-language name used to invoke this function.
        /// </summary>
        /// <value><c>iseven</c>.</value>
        public string Name
        {
            get
            {
                return "iseven";
            }
        }

        /// <summary>
        /// Gets the argument signature expected by the <c>iseven</c> function.
        /// </summary>
        /// <value>A single numeric argument named <c>value</c>.</value>
        public override IEnumerable<FunctionArgument> ExpectedArguments
        {
            get
            {
                yield return new FunctionArgument("value", FunctionValue.NUMBER);
            }
        }

        /// <summary>
        /// Executes the <c>iseven</c> function.
        /// </summary>
        /// <param name="args">
        /// The evaluated arguments supplied to the function. Exactly one numeric argument is expected.
        /// </param>
        /// <returns>
        /// A numeric <see cref="FunctionValue"/> containing <c>1</c> when the input value is even; otherwise <c>0</c>.
        /// </returns>
        public FunctionValue Execute(params FunctionArgument[] args)
        {
            return base.Execute(args, (_args) =>
            {
                var input = _args[0].Value;

                decimal value = Convert.ToDecimal(input.Value);

                return new FunctionValue(FunctionValueType.Numeric, IsEven.CalculateIsEven(value) ? 1m : 0m);
            });
        }

        internal static bool CalculateIsEven(decimal value)
        {
            return IsInt.CalculateIsInt(value) && value % 2m == 0m;
        }
    }

    /// <summary>
    /// Represents the standard-library <c>isodd</c> function.
    /// </summary>
    /// <remarks>
    /// The <c>isodd</c> function evaluates a numeric expression and returns <c>1</c> when the value is odd;
    /// otherwise, it returns <c>0</c>.
    /// </remarks>
    public class IsOdd : FunctionBase, ICodedFunction
    {
        /// <summary>
        /// Gets the expression-language name used to invoke this function.
        /// </summary>
        /// <value><c>isodd</c>.</value>
        public string Name
        {
            get
            {
                return "isodd";
            }
        }

        /// <summary>
        /// Gets the argument signature expected by the <c>isodd</c> function.
        /// </summary>
        /// <value>A single numeric argument named <c>value</c>.</value>
        public override IEnumerable<FunctionArgument> ExpectedArguments
        {
            get
            {
                yield return new FunctionArgument("value", FunctionValue.NUMBER);
            }
        }

        /// <summary>
        /// Executes the <c>isodd</c> function.
        /// </summary>
        /// <param name="args">
        /// The evaluated arguments supplied to the function. Exactly one numeric argument is expected.
        /// </param>
        /// <returns>
        /// A numeric <see cref="FunctionValue"/> containing <c>1</c> when the input value is odd; otherwise <c>0</c>.
        /// </returns>
        public FunctionValue Execute(params FunctionArgument[] args)
        {
            return base.Execute(args, (_args) =>
            {
                var input = _args[0].Value;

                decimal value = Convert.ToDecimal(input.Value);

                return new FunctionValue(FunctionValueType.Numeric, CalculateIsOdd(value) ? 1m : 0m);
            });
        }

        internal static bool CalculateIsOdd(decimal value)
        {
            return IsInt.CalculateIsInt(value) && value % 2m != 0m;
        }
    }

    /// <summary>
    /// Represents the standard-library <c>isint</c> function.
    /// </summary>
    /// <remarks>
    /// The <c>isint</c> function evaluates a numeric expression and returns <c>1</c> when the value has no fractional
    /// component; otherwise, it returns <c>0</c>.
    /// </remarks>
    public class IsInt : FunctionBase, ICodedFunction
    {
        /// <summary>
        /// Gets the expression-language name used to invoke this function.
        /// </summary>
        /// <value><c>isint</c>.</value>
        public string Name
        {
            get
            {
                return "isint";
            }
        }

        /// <summary>
        /// Gets the argument signature expected by the <c>isint</c> function.
        /// </summary>
        /// <value>A single numeric argument named <c>value</c>.</value>
        public override IEnumerable<FunctionArgument> ExpectedArguments
        {
            get
            {
                yield return new FunctionArgument("value", FunctionValue.NUMBER);
            }
        }

        /// <summary>
        /// Executes the <c>isint</c> function.
        /// </summary>
        /// <param name="args">
        /// The evaluated arguments supplied to the function. Exactly one numeric argument is expected.
        /// </param>
        /// <returns>
        /// A numeric <see cref="FunctionValue"/> containing <c>1</c> when the input value is an integer; otherwise
        /// <c>0</c>.
        /// </returns>
        public FunctionValue Execute(params FunctionArgument[] args)
        {
            return base.Execute(args, (_args) =>
            {
                var input = _args[0].Value;

                decimal value = Convert.ToDecimal(input.Value);

                return new FunctionValue(FunctionValueType.Numeric, CalculateIsInt(value) ? 1m : 0m);
            });
        }

        internal static bool CalculateIsInt(decimal value)
        {
            return value == decimal.Truncate(value);
        }
    }

    /// <summary>
    /// Represents the standard-library <c>isprime</c> function.
    /// </summary>
    /// <remarks>
    /// The <c>isprime</c> function evaluates a numeric expression and returns <c>1</c> when the value is a prime
    /// integer; otherwise, it returns <c>0</c>.
    /// </remarks>
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

        /// <summary>
        /// Gets the expression-language name used to invoke this function.
        /// </summary>
        /// <value><c>isprime</c>.</value>
        public string Name
        {
            get
            {
                return "isprime";
            }
        }

        /// <summary>
        /// Gets the argument signature expected by the <c>isprime</c> function.
        /// </summary>
        /// <value>A single numeric argument named <c>value</c>.</value>
        public override IEnumerable<FunctionArgument> ExpectedArguments
        {
            get
            {
                yield return new FunctionArgument("value", FunctionValue.NUMBER);
            }
        }

        /// <summary>
        /// Executes the <c>isprime</c> function.
        /// </summary>
        /// <param name="args">
        /// The evaluated arguments supplied to the function. Exactly one numeric argument is expected.
        /// </param>
        /// <returns>
        /// A numeric <see cref="FunctionValue"/> containing <c>1</c> when the input value is prime; otherwise
        /// <c>0</c>.
        /// </returns>
        public FunctionValue Execute(params FunctionArgument[] args)
        {
            return base.Execute(args, (_args) =>
            {
                var input = _args[0].Value;

                decimal value = Convert.ToDecimal(input.Value);

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
