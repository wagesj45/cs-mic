using CSMic.StandardLibrary.Functions;
using CSMic.StandardLibrary.Functions.Angle;
using CSMic.StandardLibrary.Functions.Rounding;
using CSMic.StandardLibrary.Functions.Trigonometry;
using CSMic.StandardLibrary.Functions.Trigonometry.Hyperbolic;
using CSMic.StandardLibrary.Functions.NumberTheory;
using CSMic.StandardLibrary.Functions.Random;

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
            InitializeAngleFunctions(inputInterpreter);
            InitializeRoundingFunctions(inputInterpreter);
            InitializeTrigonometryFunctions(inputInterpreter);
            InitializeNumberTheoryFunctions(inputInterpreter);
            InitializeRandomFunctions(inputInterpreter);
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

            inputInterpreter.RegisterFunction(new Factorial());
            inputInterpreter.RegisterFunction(new BinomialCoefficient());
            inputInterpreter.RegisterFunction(new Permutations());
            inputInterpreter.RegisterFunction(new GreatestCommonDivisor());
            inputInterpreter.RegisterFunction(new LeastCommonMultiple());
        }

        public static void InitializeRoundingFunctions(InputInterpreter inputInterpreter)
        {
            if (inputInterpreter == null)
            {
                throw new ArgumentNullException("inputInterpreter", "Cannot initialize a null InputInterpreter.");
            }

            inputInterpreter.RegisterFunction(new Floor());
            inputInterpreter.RegisterFunction(new Ceiling());
            inputInterpreter.RegisterFunction(new Fractional());
            inputInterpreter.RegisterFunction(new Truncate());
            inputInterpreter.RegisterFunction(new Round());
            inputInterpreter.RegisterFunction(new Clamp());
        }

        public static void InitializeTrigonometryFunctions(InputInterpreter inputInterpreter)
        {
            if (inputInterpreter == null)
            {
                throw new ArgumentNullException("inputInterpreter", "Cannot initialize a null InputInterpreter.");
            }

            // Basic trig
            inputInterpreter.RegisterFunction(new Sin());
            inputInterpreter.RegisterFunction(new Cos());
            inputInterpreter.RegisterFunction(new Tan());
            inputInterpreter.RegisterFunction(new Asin());
            inputInterpreter.RegisterFunction(new Acos());
            inputInterpreter.RegisterFunction(new Atan());
            inputInterpreter.RegisterFunction(new Atan2());

            // Hyperbolic trig
            inputInterpreter.RegisterFunction(new Sinh());
            inputInterpreter.RegisterFunction(new Cosh());
            inputInterpreter.RegisterFunction(new Tanh());
            inputInterpreter.RegisterFunction(new Asinh());
            inputInterpreter.RegisterFunction(new Acosh());
            inputInterpreter.RegisterFunction(new Atanh());
        }

        public static void InitializeRandomFunctions(InputInterpreter inputInterpreter)
        {
            if (inputInterpreter == null)
            {
                throw new ArgumentNullException("inputInterpreter", "Cannot initialize a null InputInterpreter.");
            }

            inputInterpreter.RegisterFunction(new FairFlip());
            inputInterpreter.RegisterFunction(new Bernoulli());
            inputInterpreter.RegisterFunction(new RandomUniform());
            inputInterpreter.RegisterFunction(new RandomUniformSpread());
            inputInterpreter.RegisterFunction(new RandomNormal());
            inputInterpreter.RegisterFunction(new RandomNormalSpread());
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
