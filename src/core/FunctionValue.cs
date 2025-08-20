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

        public static readonly FunctionValue TRUE = new FunctionValue(ValueType.Numeric, 1m);
        public static readonly FunctionValue FALSE = new FunctionValue(ValueType.Numeric, 0m);
        public static readonly FunctionValue NONE = new FunctionValue(ValueType.None, null);
        public static readonly FunctionValue NUMBER = new FunctionValue(ValueType.Numeric, 0m);
        public static readonly FunctionValue STRING = new FunctionValue(ValueType.String, string.Empty);

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
