using CSMic.StandardLibrary.Functions;
using CSMic.StandardLibrary.Functions.Angle;
using CSMic.StandardLibrary.Functions.Rounding;
using CSMic.StandardLibrary.Functions.Trigonometry;
using CSMic.StandardLibrary.Functions.Trigonometry.Hyperbolic;
using CSMic.StandardLibrary.Functions.NumberTheory;
using CSMic.StandardLibrary.Functions.Random;

namespace CSMic.StandardLibrary
{
    /// <summary>
    ///  Offers helper methods to initialize a <see cref="InputInterpreter"/> with collections of
    ///  standard mathematical expressions and constants.
    /// </summary>
    public static class Initializer
    {
        /// <summary> Initializes all standard library functions and constants. </summary>
        /// <exception cref="ArgumentNullException"> Thrown when one or more required arguments are null. </exception>
        /// <param name="inputInterpreter"> The input interpreter. </param>
        /// <seealso cref="InitializeAllFunctions(InputInterpreter)"/>
        /// <seealso cref="InitializeConstants(InputInterpreter)"/>
        ///
        /// ### <param name="interpreter"> The interpreter to hydrate. </param>
        public static void InitializeAll(InputInterpreter inputInterpreter)
        {
            if (inputInterpreter == null)
            {
                throw new ArgumentNullException("inputInterpreter", "Cannot initialize a null InputInterpreter.");
            }

            InitializeAllFunctions(inputInterpreter);
            InitializeConstants(inputInterpreter);
        }

        /// <summary> Initializes all standard library functions. </summary>
        /// <exception cref="ArgumentNullException"> Thrown when one or more required arguments are null. </exception>
        /// <param name="inputInterpreter"> The input interpreter. </param>
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

        /// <summary> Initializes the base standard library functions. </summary>
        /// <exception cref="ArgumentNullException"> Thrown when one or more required arguments are null. </exception>
        /// <param name="inputInterpreter"> The input interpreter. </param>
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
            inputInterpreter.RegisterFunction(new SquareRoot());
            inputInterpreter.RegisterFunction(new Power());
            inputInterpreter.RegisterFunction(new Log());
            inputInterpreter.RegisterFunction(new Natural_Log());
            inputInterpreter.RegisterFunction(new Lerp());
            inputInterpreter.RegisterFunction(new SmoothStep());
            inputInterpreter.RegisterFunction(new Map());
            inputInterpreter.RegisterFunction(new Normalize());
        }

        /// <summary> Initializes the angle-related functions. </summary>
        /// <exception cref="ArgumentNullException"> Thrown when one or more required arguments are null. </exception>
        /// <param name="inputInterpreter"> The input interpreter. </param>
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

        /// <summary> Initializes the number theory functions. </summary>
        /// <exception cref="ArgumentNullException"> Thrown when one or more required arguments are null. </exception>
        /// <param name="inputInterpreter"> The input interpreter. </param>
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
            inputInterpreter.RegisterFunction(new Fibonacci());
            inputInterpreter.RegisterFunction(new IsEven());
            inputInterpreter.RegisterFunction(new IsOdd());
            inputInterpreter.RegisterFunction(new IsInt());
            inputInterpreter.RegisterFunction(new IsPrime());
        }

        /// <summary> Initializes the rounding functions. </summary>
        /// <exception cref="ArgumentNullException"> Thrown when one or more required arguments are null. </exception>
        /// <param name="inputInterpreter"> The input interpreter. </param>
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

        /// <summary> Initializes the trigonometry functions. </summary>
        /// <exception cref="ArgumentNullException"> Thrown when one or more required arguments are null. </exception>
        /// <param name="inputInterpreter"> The input interpreter. </param>
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

        /// <summary> Initializes the psuedorandom generation and statistics functions. </summary>
        /// <exception cref="ArgumentNullException"> Thrown when one or more required arguments are null. </exception>
        /// <param name="inputInterpreter"> The input interpreter. </param>
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

        /// <summary> Initializes the standard library constants. </summary>
        /// <exception cref="ArgumentNullException"> Thrown when one or more required arguments are null. </exception>
        /// <param name="inputInterpreter"> The input interpreter. </param>
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
            inputInterpreter.Interpret("euler :: 0.5772156649015329");
            inputInterpreter.Interpret("omega :: 0.5671432904097839");
        }
    }
}
