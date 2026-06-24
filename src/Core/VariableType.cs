using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSMic
{
    /// <summary> Values that represent variable types supported by the parser. </summary>
    public enum VariableType
    {
        /// <summary> An enum constant representing an unknown or unsupported type. </summary>
        None,
        /// <summary> An enum constant representing a numeric type backed by a <see cref="decimal"/>. </summary>
        Numeric,
        /// <summary> An enum constant representing numeric array backed by a <see cref="Array"/> of <see cref="decimal"/>. </summary>
        NumericArray,
        /// <summary> An enum constant representing an expression type backed by a <see cref="string"/> and interpreted by the parser at runtime. </summary>
        Expression,
    }
}
