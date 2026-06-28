using System;
using System.Collections.Generic;
using System.Text;

namespace CSMic.StandardLibrary.Functions
{
    /// <summary>
    /// Represents the standard-library <c>log</c> function.
    /// </summary>
    /// <remarks>
    /// The <c>log</c> function evaluates a numeric expression and returns its logarithm in the supplied base.
    /// </remarks>
    public class Log: FunctionBase, ICodedFunction
    {
        /// <summary>
        /// Gets the expression-language name used to invoke this function.
        /// </summary>
        /// <value><c>log</c>.</value>
        public string Name
        {
            get
            {
                return "log";
            }
        }

        /// <summary>
        /// Gets the argument signature expected by the <c>log</c> function.
        /// </summary>
        /// <value>Two numeric arguments named <c>value</c> and <c>base</c>.</value>
        public override IEnumerable<FunctionArgument> ExpectedArguments
        {
            get
            {
                yield return new FunctionArgument("value", FunctionValue.NUMBER);
                yield return new FunctionArgument("base", FunctionValue.NUMBER);
            }
        }

        /// <summary>
        /// Executes the <c>log</c> function.
        /// </summary>
        /// <param name="args">
        /// The evaluated arguments supplied to the function. Exactly two numeric arguments are expected.
        /// </param>
        /// <returns>
        /// A numeric <see cref="FunctionValue"/> containing the logarithm of the input value in the supplied base.
        /// </returns>
        public FunctionValue Execute(params FunctionArgument[] args)
        {
            return base.Execute(args, (_args) =>
            {
                var input = _args[0].Value;
                var input2 = _args[1].Value;
                double number = Convert.ToDouble(input.Value);
                double _baseNumber = Convert.ToDouble(input2.Value);

                return new FunctionValue(FunctionValueType.Numeric, Math.Log(number, _baseNumber));
            });
        }
    }
}
