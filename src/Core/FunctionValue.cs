using CSMic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSMic
{
    public class FunctionValue
    {
        public FunctionValueType Type { get; set; }
        public object? Value { get; set; }

        public static readonly FunctionValue TRUE = new FunctionValue(FunctionValueType.Numeric, 1m);
        public static readonly FunctionValue FALSE = new FunctionValue(FunctionValueType.Numeric, 0m);
        public static readonly FunctionValue NONE = new FunctionValue(FunctionValueType.None, null);
        public static readonly FunctionValue NUMBER = new FunctionValue(FunctionValueType.Numeric, 0m);
        public static readonly FunctionValue STRING = new FunctionValue(FunctionValueType.String, string.Empty);
        public static readonly FunctionValue ZERO = new FunctionValue(FunctionValueType.Numeric, 0m);

        public FunctionValue()
        {
            this.Type = FunctionValueType.None;
            this.Value = null;
        }

        public FunctionValue(FunctionValueType type, object? value)
        {
            this.Type = type;
            this.Value = value;
        }
    }
}
