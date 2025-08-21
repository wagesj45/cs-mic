using CSMic.StandardLibrary.Functions;
using CSMic.StandardLibrary.Functions.Angle;

namespace CSMic.StandardLibrary
{
    public static class Initializer
    {
        public static void InitializeAll(InputInterpreter interpreter)
        {
            InitializeAllFunctions(interpreter);
            InitializeConstants(interpreter);
        }

        public static void InitializeAllFunctions(InputInterpreter inputInterpreter)
        {
            if (inputInterpreter == null)
            {
                throw new ArgumentNullException("inputInterpreter", "Cannot initialize a null InputInterpreter.");
            }

            InitializeBaseFunctions(inputInterpreter);
        }

        public static void InitializeBaseFunctions(InputInterpreter inputInterpreter)
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

        public static void InitializeAngleFunctions(InputInterpreter inputInterpreter)
        {
            if (inputInterpreter == null)
            {
                throw new ArgumentNullException("inputInterpreter", "Cannot initialize a null InputInterpreter.");
            }

            inputInterpreter.RegisterFunction(new Degrees());
            inputInterpreter.RegisterFunction(new Radians());
            inputInterpreter.RegisterFunction(new WrapAngle());
        }

        public static void InitializeNumberTheoryFunctions(InputInterpreter inputInterpreter)
        {
            if (inputInterpreter == null)
            {
                throw new ArgumentNullException("inputInterpreter", "Cannot initialize a null InputInterpreter.");
            }

            // Register functions...
        }

        public static void InitializeConstants(InputInterpreter inputInterpreter)
        {
            if (inputInterpreter == null)
            {
                throw new ArgumentNullException("inputInterpreter", "Cannot initialize a null InputInterpreter.");
            }

            inputInterpreter.Interpret("pi :: 3.1415926535897931");
            inputInterpreter.Interpret("e :: 2.7182818284590451");
            inputInterpreter.Interpret("tau :: 6.2831853071795862");
            inputInterpreter.Interpret("phi :: 1.6180339887498948");
            inputInterpreter.Interpret("goldenratio :: 1.6180339887498948");
            inputInterpreter.Interpret("eurler :: 0.5772156649015329");
            inputInterpreter.Interpret("omega :: 0.5671432904097839");
        }
    }
}

