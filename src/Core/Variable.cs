using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSMic
{
    public class Variable
    {
        #region Members

        private VariableType type;

        private string name;

        private object? value;

        #endregion

        #region Properties

        public VariableType Type
        {
            get
            {
                return this.type;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public object? Value
        {
            get
            {
                return this.value;
            }
        }

        #endregion

        #region Constructor

        public Variable()
        {
            this.type = VariableType.None;
            this.name = string.Empty;
            this.value = null;
        }

        public Variable(VariableType type, string name, object? value) 
        {
            this.type = type;
            this.name = name;
            this.value = value;
        }



        #endregion
    }
}
