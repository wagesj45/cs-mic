using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSMic
{
    public class FunctionArgument
    {
        public string Name { get; set; }
        public FunctionValue Value { get; set; }

        public FunctionArgument(string name, FunctionValue fv) 
        {
            this.Name = name;
            this.Value = fv;
        }
    }
}
