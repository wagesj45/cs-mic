using CSMic;
using CSMic.StandardLibrary.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSMic.StandardLibrary
{
    public static class Functions
    {
        public static void Initialize(InputInterpreter inputInterpreter)
        {
            if (inputInterpreter == null)
            {
                throw new ArgumentNullException("inputInterpreter", "Cannot initialize a null InputInterpreter.");
            }

            inputInterpreter.RegisterFunction(new AbsoluteValue());
            inputInterpreter.RegisterFunction(new Sign());
            inputInterpreter.RegisterFunction(new Min());
            inputInterpreter.RegisterFunction(new Max());
        }
    }
}
