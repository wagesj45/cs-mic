using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csmic
{
    public interface ICodedFunction
    {
        #region Properties

        string Name { get; }
        IEnumerable<FunctionArgument> ExpectedArguments { get; }
        FunctionValue ReturnValue { get; }

        #endregion

        #region Methods

        FunctionValue Execute(params FunctionArgument[] args);

        #endregion
    }
}
