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
        public required csmic.ValueType Type { get; set; }
        public object? Value { get; set; }
    }
}
