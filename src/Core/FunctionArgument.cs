using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSMic
{
    /// <summary> An encapsulated function argument. </summary>
    public class FunctionArgument
    {
        #region Properties
        
        /// <summary> Gets or sets the name. </summary>
        /// <value> The name. </value>
        public string Name { get; set; }

        /// <summary> Gets or sets the value. </summary>
        /// <value> The value. </value>
        public FunctionValue Value { get; set; }

        #endregion

        #region Constructors
        
        /// <summary> Constructor. </summary>
        /// <param name="name"> The name. </param>
        /// <param name="fv"> The fv. </param>
        public FunctionArgument(string name, FunctionValue fv)
        {
            this.Name = name;
            this.Value = fv;
        } 

        #endregion
    }
}
