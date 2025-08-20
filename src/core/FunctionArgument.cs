using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csmic
{
    public class FunctionArgument
    {
        public required string Name { get; set; }
        public required FunctionValue Value { get; set; }

        public FunctionArgument(string name, FunctionValue fv) 
        {
            this.Name = name;
            this.Value = fv;
        }
    }
}
