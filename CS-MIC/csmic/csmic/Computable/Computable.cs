using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csmic.ComputableEngine
{
    /// <summary> Computable class. </summary>
    public sealed class Computable
    {
        #region Members

        /// <summary> The expression to be built.</summary>
        private string expression;
        /// <summary> The interpreter to act as a base.</summary>
        private InputInterpreter interpreter;

        #region Constants

        /// <summary> The add symbol.</summary>
        private const string ADD = "+";
        /// <summary> The substract symbol.</summary>
        private const string SUBSTRACT = "-";
        /// <summary> The divide symbol.</summary>
        private const string DIVIDE = "/";
        /// <summary> The multiply symbol.</summary>
        private const string MULTIPLY = "*";
        /// <summary> The modifier symbol.</summary>
        private const string MOD = "%";
        /// <summary> The power symbol.</summary>
        private const string POWER = "^"; 

        #endregion

        #endregion

        #region Properties

        /// <summary> Gets the expression. </summary>
        /// <value> The expression. </value>
        internal string Expression
        {
            get
            {
                return this.expression;
            }
        }

        #endregion

        #region Constructor

        /// <summary> Creates a Computable instance. </summary>
        /// <param name="expression">  The expression. </param>
        /// <param name="interpreter"> The interpreter. </param>
        internal Computable(string expression, InputInterpreter interpreter)
        {
            this.expression = expression;
            this.interpreter = interpreter;
        }

        #endregion

        #region Methods

        /// <summary> Resolves the computable as an input interpreter having calculated the input. </summary>
        /// <returns> The computable as an input interpreter. </returns>
        public InputInterpreter Resolve()
        {
            this.interpreter.Interpret(this.Expression);
            return this.interpreter;
        }

        /// <summary> Resolve the computer to a type <typeparamref name="T"/>. </summary>
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="selector"> The selector function. </param>
        /// <returns> . </returns>
        public T ResolveTo<T>(Func<InputInterpreter, T> selector)
        {
            return selector(this.Resolve());
        }

        /// <summary> Form the operation given the operation constant and an argument. </summary>
        /// <param name="operation"> The operation constant. </param>
        /// <param name="argument">  The argument. </param>
        /// <returns> A string with the given operation appended. </returns>
        private string FormOperation(string operation, object argument)
        {
            return string.Format("({0} {1} {2})", this.Expression, operation, argument);
        }

        /// <summary> Form the function <paramref name="name"/> with the current expression as the first argument, followed by <paramref name="arguments"/>. </summary>
        /// <param name="name">      The name of the function. </param>
        /// <param name="arguments"> The arguments. </param>
        /// <returns> . </returns>
        private string FormFunction(string name, object[] arguments)
        {
            return string.Format("{0}({1})", name, string.Join(",", this.Expression, arguments));
        }

        /// <summary> Adds addend. </summary>
        /// <param name="addend"> The decimal to add. </param>
        /// <returns> A computable class after the addition. </returns>
        public Computable Add(decimal addend)
        {
            this.expression = FormOperation(ADD, addend);
            return this;
        }

        /// <summary> Subtracts the subtrahend. </summary>
        /// <param name="subtrahend"> The subtrahend. </param>
        /// <returns> A computable class after the subtraction. </returns>
        public Computable Subtract(decimal subtrahend)
        {
            this.expression = FormOperation(SUBSTRACT, subtrahend);
            return this;
        }

        /// <summary> Multiplies the multiplicand. </summary>
        /// <param name="multiplicand"> The multiplicand. </param>
        /// <returns> A computable class after the mulitplication. </returns>
        public Computable Multiply(decimal multiplicand)
        {
            this.expression = FormOperation(MULTIPLY, multiplicand);
            return this;
        }

        /// <summary> Divides the divisor. </summary>
        /// <param name="divisor"> The divisor. </param>
        /// <returns> A computable class after the divison. </returns>
        public Computable Divide(decimal divisor)
        {
            this.expression = FormOperation(DIVIDE, divisor);
            return this;
        }

        /// <summary> Mods using a given divisor. </summary>
        /// <param name="divisor"> The divisor. </param>
        /// <returns> A computable class after the mod. </returns>
        public Computable Mod(decimal divisor)
        {
            this.expression = FormOperation(MOD, divisor);
            return this;
        }

        /// <summary> Raises to power of the given integer value. </summary>
        /// <param name="power"> The power. </param>
        /// <returns> A computable class after the power operation. </returns>
        public Computable RaiseToPower(int power)
        {
            this.expression = FormOperation(POWER, power);
            return this;
        }

        /// <summary> Applies the function <paramref name="name"/> with the current expression as the first argument, followed by <paramref name="arguments"/>. </summary>
        /// <param name="name">      The name of the function. </param>
        /// <param name="arguments"> The arguments. </param>
        /// <returns> A computable class after the function is applied. </returns>
        public Computable  ApplyFunction(string name, object[] arguments)
        {
            this.expression = FormFunction(name, arguments);
            return this;
        }

        /// <summary> Executes a command for all items in a collection. </summary>
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="items">  The items of type <typeparamref name="T"/>. </param>
        /// <param name="action"> The action belonging to type <see cref="Computable"/>. </param>
        /// <returns> . </returns>
        public Computable ForAll<T>(ICollection<T> items, Func<Computable, Func<T, Computable>> action)
        {
            foreach (T item in items)
            {
                action(this)(item);
            }

            return this;
        }

        #endregion
    }
}
