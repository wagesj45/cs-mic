using CSMic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSMic
{
    /// <summary> An encapsulated function value. </summary>
    public class FunctionValue
    {
        #region Properties
        
        /// <summary> Gets or sets the value type of a function. </summary>
        /// <value> The type. </value>
        public FunctionValueType Type { get; set; }

        /// <summary> Gets or sets the value. </summary>
        /// <value> The value. </value>
        public object? Value { get; set; }

        #endregion

        #region Constants

        /// <summary>
        ///  (Immutable) A defined <see cref="FunctionValue"/> that represents a <c>true</c> <see cref="bool"/>
        ///  whose value is <c>1</c>.
        /// </summary>
        public static readonly FunctionValue TRUE = new FunctionValue(FunctionValueType.Numeric, 1m);

        /// <summary>
        ///  (Immutable) A defined <see cref="FunctionValue"/> that represents a <c>false</c> <see cref="bool"/>
        ///  whose value is <c>0</c>.
        /// </summary>
        public static readonly FunctionValue FALSE = new FunctionValue(FunctionValueType.Numeric, 0m);

        /// <summary>
        ///  (Immutable) A defined <see cref="FunctionValue"/> that represents a undefined value.
        /// </summary>
        public static readonly FunctionValue NONE = new FunctionValue(FunctionValueType.None, null);

        /// <summary>
        ///  (Immutable) A defined <see cref="FunctionValue"/> that represents a numeric value. The
        ///  default value is <c>0</c>.
        /// </summary>
        public static readonly FunctionValue NUMBER = new FunctionValue(FunctionValueType.Numeric, 0m);

        /// <summary>
        ///  (Immutable) A defined <see cref="FunctionValue"/> that represents a string value. The default
        ///  value is <see cref="string.Empty"/>.
        /// </summary>
        public static readonly FunctionValue STRING = new FunctionValue(FunctionValueType.String, string.Empty);

        /// <summary>
        ///  (Immutable) A defined <see cref="FunctionValue"/> that represents the number zero.
        /// </summary>
        public static readonly FunctionValue ZERO = new FunctionValue(FunctionValueType.Numeric, 0m);

        #endregion

        #region Constructors
        
        /// <summary> Default constructor. </summary>
        public FunctionValue()
        {
            this.Type = FunctionValueType.None;
            this.Value = null;
        }

        /// <summary> Constructor. </summary>
        /// <param name="type"> The type. </param>
        /// <param name="value"> The value. </param>
        public FunctionValue(FunctionValueType type, object? value)
        {
            this.Type = type;
            this.Value = value;
        } 

        #endregion
    }
}
