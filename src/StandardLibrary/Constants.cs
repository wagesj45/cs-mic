using CSMic;

namespace CSMic.StandardLibrary
{
    public static class Constants
    {
        public static void Initialize(InputInterpreter inputInterpreter)
        {
            if(inputInterpreter == null)
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
