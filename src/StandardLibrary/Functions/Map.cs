using System;
using System.Collections.Generic;
using System.Text;

namespace CSMic.StandardLibrary.Functions
{
    /// <summary>
    /// Represents the standard-library <c>map</c> function.
    /// </summary>
    /// <remarks>
    /// The <c>map</c> function evaluates a numeric value from one range and returns the corresponding value in a
    /// second range.
    /// </remarks>
    public class Map: FunctionBase, ICodedFunction
    {
        /// <summary>
        /// Gets the expression-language name used to invoke this function.
        /// </summary>
        /// <value><c>map</c>.</value>
        public string Name
        {
            get
            {
                return "map";
            }
        }

        /// <summary>
        /// Gets the argument signature expected by the <c>map</c> function.
        /// </summary>
        /// <value>
        /// Five numeric arguments named <c>value</c>, <c>oldMinimum</c>, <c>oldMaximum</c>,
        /// <c>newMinimum</c>, and <c>newMaximum</c>.
        /// </value>
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

        /// <summary>
        /// Executes the <c>map</c> function.
        /// </summary>
        /// <param name="args">
        /// The evaluated arguments supplied to the function. Exactly five numeric arguments are expected.
        /// </param>
        /// <returns>
        /// A numeric <see cref="FunctionValue"/> containing the mapped value, or <c>0</c> when the source range has
        /// equal minimum and maximum bounds.
        /// </returns>
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
