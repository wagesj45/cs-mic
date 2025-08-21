using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsMic
{
    public class Variable
    {
        #region Members

        private VariableType type;

        private string name;

        private object value;

        #endregion

        #region Constructor

        public Variable()
        {
            this.type = VariableType.None;
            this.value = string.Empty;
        }

        #endregion
    }
}
