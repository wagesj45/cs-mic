using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSMic
{
    /// <summary> An encapsulated variable that names a runtime value. </summary>
    public class Variable
    {
        #region Members

        /// <summary> The type of the variable. </summary>
        private VariableType type;

        /// <summary> The name of the variable. </summary>
        private string name;

        /// <summary> The value assigned to the variable. </summary>
        private object? value;

        #endregion

        #region Properties

        /// <summary> Gets the variable type. </summary>
        /// <value> The type. </value>
        public VariableType Type
        {
            get
            {
                return this.type;
            }
        }

        /// <summary> Gets the variable name. </summary>
        /// <value> The name. </value>
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        /// <summary> Gets the assigned value. </summary>
        /// <value> The assigned value. </value>
        public object? Value
        {
            get
            {
                return this.value;
            }
        }

        #endregion

        #region Constructor

        /// <summary> Default constructor. </summary>
        public Variable()
        {
            this.type = VariableType.None;
            this.name = string.Empty;
            this.value = null;
        }

        /// <summary> Constructor. </summary>
        /// <param name="type"> The type of the variable. </param>
        /// <param name="name"> The name of the variable. </param>
        /// <param name="value"> The value assigned to the variable. </param>
        public Variable(VariableType type, string name, object? value) 
        {
            this.type = type;
            this.name = name;
            this.value = value;
        }



        #endregion
    }
}
