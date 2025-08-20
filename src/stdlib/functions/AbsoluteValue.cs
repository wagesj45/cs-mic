using csmic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stdlib.functions
{
    public class AbsoluteValue : ICodedFunction
    {
        public IEnumerable<FunctionArgument> ExpectedArguments
        {
            get
            {
                yield return new FunctionArgument() { };
            }
        }

        public FunctionValue ReturnValue => throw new NotImplementedException();

        public FunctionValue Execute(params FunctionArgument[] args)
        {
            throw new NotImplementedException();
        }
    }
}
