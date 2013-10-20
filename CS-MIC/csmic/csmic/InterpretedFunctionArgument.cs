using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csmic
{
    /// <summary>
    /// Represents an argument made in an interpreted function.
    /// </summary>
    public class InterpretedFunctionArgument
    {
        #region Members

        /// <summary>
        /// The name of the argument.
        /// </summary>
        private string name;
        /// <summary>
        /// The value of the argument.
        /// </summary>
        private decimal value;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new interpreted function argument.
        /// </summary>
        public InterpretedFunctionArgument()
        {
            this.name = string.Empty;
            this.value = 0;
        }

        /// <summary>
        /// Creates a new interpreted function argument.
        /// </summary>
        /// <param name="name">The name of the argument in the interpreted function.</param>
        /// <param name="value">The value of the argument to use in the interpreted function.</param>
        public InterpretedFunctionArgument(string name, decimal value)
        {
            this.name = name;
            this.value = value;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name of the argument.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        /// <summary>
        /// Gets or sets the value of the argument.
        /// </summary>
        public decimal Value
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

        #endregion
    }
}
