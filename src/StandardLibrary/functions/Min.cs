using CSMic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSMic.StandardLibrary.Functions
{
    /// <summary>
    /// Represents the standard-library <c>min</c> function.
    /// </summary>
    /// <remarks>
    /// The <c>min</c> function evaluates two numeric expressions and returns the smaller value.
    /// </remarks>
    public class Min : FunctionBase, ICodedFunction
    {
        /// <summary>
        /// Gets the expression-language name used to invoke this function.
        /// </summary>
        /// <value><c>min</c>.</value>
        public string Name
        {
            get
            {
                return "min";
            }
        }

        /// <summary>
        /// Gets the argument signature expected by the <c>min</c> function.
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
        /// Executes the <c>min</c> function.
        /// </summary>
        /// <param name="args">
        /// The evaluated arguments supplied to the function. Exactly two numeric arguments are expected.
        /// </param>
        /// <returns>
        /// A numeric <see cref="FunctionValue"/> containing the smaller of the two input values.
        /// </returns>
        public FunctionValue Execute(params FunctionArgument[] args)
        {
            return base.Execute(args, (_args) =>
            {
                var inputFirst = _args[0].Value;
                var inputSecond = _args[1].Value;
                decimal first = Convert.ToDecimal(inputFirst.Value);
                decimal second = Convert.ToDecimal(inputSecond.Value);


                return new FunctionValue(FunctionValueType.Numeric, Math.Min(first, second));
            });
        }
    }
}
