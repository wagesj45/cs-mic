using CSMic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSMic.StandardLibrary.Functions.NumberTheory
{
    /// <summary>
    /// Represents the standard-library <c>fac</c> function.
    /// </summary>
    /// <remarks>
    /// The <c>fac</c> function evaluates a numeric expression and returns its factorial when the value is an integer,
    /// or its gamma-based extension when the value is non-integer.
    /// </remarks>
    public class Factorial : FunctionBase, ICodedFunction
    {
        private static readonly double[] LANCZOS_APPROXIMATION = 
            {
                0.99999999999980993,
                676.5203681218851,
                -1259.1392167224028,
                771.32342877765313,
                -176.61502916214059,
                12.507343278686905,
                -0.13857109526572012,
                9.9843695780195716e-6,
                1.5056327351493116e-7
            };

        /// <summary>
        /// Gets the lookup table used for exact factorial values from <c>0</c> through <c>20</c>.
        /// </summary>
        public static readonly decimal[] INTEGER_FACTORIAL_LOOKUP = 
            {
                /* 0 */ 1,
                /* 1 */ 1,
                /* 2 */ 2,
                /* 3 */ 6,
                /* 4 */ 24,
                /* 5 */ 120,
                /* 6 */ 720,
                /* 7 */ 5040,
                /* 8 */ 40320,
                /* 9 */ 362880,
                /* 10 */ 3628800,
                /* 11 */ 39916800,
                /* 12 */ 479001600,
                /* 13 */ 6227020800,
                /* 14 */ 87178291200,
                /* 15 */ 1307674368000,
                /* 16 */ 20922789888000,
                /* 17 */ 355687428096000,
                /* 18 */ 6402373705728000,
                /* 19 */ 121645100408832000,
                /* 20 */ 2432902008176640000
            };

        /// <summary>
        /// Gets the expression-language name used to invoke this function.
        /// </summary>
        /// <value><c>fac</c>.</value>
        public string Name
        {
            get
            {
                return "fac";
            }
        }

        /// <summary>
        /// Gets the argument signature expected by the <c>fac</c> function.
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
        /// Executes the <c>fac</c> function.
        /// </summary>
        /// <param name="args">
        /// The evaluated arguments supplied to the function. Exactly one numeric argument is expected.
        /// </param>
        /// <returns>
        /// A numeric <see cref="FunctionValue"/> containing the factorial result or a gamma-based approximation.
        /// </returns>
        public FunctionValue Execute(params FunctionArgument[] args)
        {
            return base.Execute(args, (_args) =>
            {
                var inputValue = _args[0].Value;
                decimal value = Convert.ToDecimal(inputValue.Value);

                if (value < 0)
                {
                    return FunctionValue.ZERO;
                }
                if (value > 20)
                {
                    return FunctionValue.ZERO;
                }

                if (Math.Floor(value) == value)
                {
                    // Input is an integer. We'll just use the lookup table.
                    var index = Convert.ToInt32(value);
                    return new FunctionValue(FunctionValueType.Numeric, INTEGER_FACTORIAL_LOOKUP[index]);
                }
                
                double gammaFactorial = Gamma(Convert.ToDouble(value) + 1);

                return new FunctionValue(FunctionValueType.Numeric, Convert.ToDecimal(gammaFactorial));
            });
        }

        /// <summary>
        /// Evaluates the gamma function used to extend factorial values to non-integer inputs.
        /// </summary>
        /// <param name="shiftedGama">The shifted input value.</param>
        /// <returns>The gamma of the supplied value.</returns>
        private static double Gamma(double shiftedGama)
        {
            if (shiftedGama < 0.5)
                return Math.PI / (Math.Sin(Math.PI * shiftedGama) * Gamma(1 - shiftedGama));

            shiftedGama -= 1;
            double accumulator = LANCZOS_APPROXIMATION[0];
            for (int i = 1; i < LANCZOS_APPROXIMATION.Length; i++)
                accumulator += LANCZOS_APPROXIMATION[i] / (shiftedGama + i);

            double shiftedVariable = shiftedGama + 7.5;
            return Math.Sqrt(2 * Math.PI) * Math.Pow(shiftedVariable, shiftedGama + 0.5) * Math.Exp(-shiftedVariable) * accumulator;
        }

    }
}
