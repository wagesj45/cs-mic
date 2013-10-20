using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csmic
{
    /// <summary>
    /// Represents the data types supported in a variable.
    /// </summary>
    public enum VariableType
    {
        /// <summary>
        /// Decimal
        /// </summary>
        Decimal,
        /// <summary>
        /// Equation
        /// </summary>
        Equation,
        /// <summary>
        /// Array
        /// </summary>
        Array,
        /// <summary>
        /// Unknown
        /// </summary>
        Unknown,
        /// <summary>
        /// No type associated
        /// </summary>
        None
    }

    /// <summary>
    /// An object that contains information about a variable.
    /// </summary>
    public class Variable
    {
        #region Members

        /// <summary>
        /// The type of variable.
        /// </summary>
        private VariableType type;
        /// <summary>
        /// The value of the variable.
        /// </summary>
        private object value;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates an empty variable.
        /// </summary>
        public Variable()
        {
            this.type = VariableType.None;
            this.value = null;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets an object representing the variable's value.
        /// </summary>
        public object Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
            }
        }

        /// <summary>
        /// Gets or sets the type of the variable.
        /// </summary>
        public VariableType Type
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value;
            }
        }

        #endregion
    }
}
