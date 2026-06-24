using CSMic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSMic.StandardLibrary.Functions.NumberTheory
{
    /// <summary>
    /// Represents the standard-library <c>gcd</c> function.
    /// </summary>
    /// <remarks>
    /// The <c>gcd</c> function evaluates two positive integers and returns their greatest common divisor.
    /// </remarks>
    public class GreatestCommonDivisor : FunctionBase, ICodedFunction
    {
        /// <summary>
        /// Gets the expression-language name used to invoke this function.
        /// </summary>
        /// <value><c>gcd</c>.</value>
        public string Name
        {
            get
            {
                return "gcd";
            }
        }

        /// <summary>
        /// Gets the argument signature expected by the <c>gcd</c> function.
        /// </summary>
        /// <value>Two numeric arguments named <c>first</c> and <c>second</c>.</value>
        public override IEnumerable<FunctionArgument> ExpectedArguments
        {
            get
            {
                yield return new FunctionArgument("first", FunctionValue.NUMBER);
                yield return new FunctionArgument("second", FunctionValue.NUMBER);
            }
        }

        /// <summary>
        /// Executes the <c>gcd</c> function.
        /// </summary>
        /// <param name="args">
        /// The evaluated arguments supplied to the function. Exactly two numeric arguments are expected.
        /// </param>
        /// <returns>
        /// A numeric <see cref="FunctionValue"/> containing the greatest common divisor of the two input values.
        /// </returns>
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

                decimal gcd = EuclideanAlgorithm(first, second);

                return new FunctionValue(FunctionValueType.Numeric, gcd);
            });
        }

        /// <summary>
        /// Computes the greatest common divisor using the Euclidean algorithm.
        /// </summary>
        /// <param name="first">The first value.</param>
        /// <param name="second">The second value.</param>
        /// <returns>The greatest common divisor of the supplied values.</returns>
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
