using csmic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csmic
{
    public class FunctionValue
    {
        public ValueType Type { get; set; }
        public object? Value { get; set; }

        public static readonly FunctionValue TRUE = new FunctionValue(ValueType.Numeric, 1);
        public static readonly FunctionValue FALSE = new FunctionValue(ValueType.Numeric, 0);

        public FunctionValue()
        {
            this.Type = ValueType.None;
            this.Value = null;
        }

        public FunctionValue(ValueType type, object? value)
        {
            this.Type = type;
            this.Value = value;
        }
    }
}
