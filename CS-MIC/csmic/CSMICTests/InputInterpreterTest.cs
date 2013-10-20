using csmic;
using csmic.Extensions;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CSMICTests
{
    
    
    /// <summary>
    ///This is a test class for InputInterpreterTest and is intended
    ///to contain all InputInterpreterTest Unit Tests
    ///</summary>
    [TestClass()]
    public class InputInterpreterTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Interpret
        ///</summary>
        [TestMethod()]
        public void InterpretTest()
        {
            InputInterpreter target = new InputInterpreter();
            target.CodedFunctions.Add(CodedFunctionFactory.Create("addfive", 1, rf => rf[0] + 5));

            //HighLoadTest(target, "+1");
            //HighLoadTest(target, "+sin(1)");
            //HighLoadTest(target, "+precision(0.123456789, 7)");
            //HighLoadTest(target, "+2*(2 + 5)/tan(sin(4))");
            //HighLoadTest(target, "+2*2/3-1.23456");

            ExpressionDecimalTest(target, "2+2", 4);
            ExpressionDecimalTest(target, "1 + 2 + 3 + 4 + 5 + 6 + 7 + 8 + 9 + 10", 55);
            ExpressionDecimalTest(target, "4 + 2 / 1 - 3 * 2", 0);
            ExpressionDecimalTest(target, "16*2 - 2*3", 26);
            ExpressionDecimalTest(target, "7-6+3*2", 7);
            ExpressionDecimalTest(target, "1/-2*-1", 0.5M);
            ExpressionDecimalTest(target, "12 + 26 / -2 + 3", 2);
            ExpressionDecimalTest(target, "0.02395/1000", 0.00002395M);
            ExpressionDecimalTest(target, "0100010101b", 277);
            ExpressionDecimalTest(target, "0xabcdef", 11259375);
            ExpressionDecimalTest(target, "11111111b + 0xFF", 510);
            ExpressionDecimalTest(target, "10011001b * 0x01bAc", 1083852);

            ExpressionDecimalTest(target, "log(sin(((25*200) - 5000)^1943),2)", decimal.MinValue);
            ExpressionDecimalTest(target, "log(cos(((25*200) - 5000)^1943),2)", 0);
            ExpressionDecimalTest(target, "cos(2^3)", -0.145500033808614M);
            ExpressionDecimalTest(target, "addfive(30)", 35);
            ExpressionDecimalTest(target, "precision(1.23456789, 4)", 1.2346M);
            ExpressionDecimalTest(target, "3000 * (log(1 + 3162, 2))", 34881.2335215522M);
            ExpressionDecimalTest(target, "(((12)))", 12);

            ExpressionOutputTest(target, "z := (3*x) - (2*y)", "(3*x)-(2*y)");
            ExpressionOutputTest(target, "x :: 3", "3");
            ExpressionOutputTest(target, "y :: 2", "2");
            ExpressionOutputTest(target, "x", "3");
            ExpressionOutputTest(target, "y", "2");
            ExpressionOutputTest(target, "z", "5");
            ExpressionOutputTest(target, "z + 2", "7");
            ExpressionOutputTest(target, "y :: 3", "3");
            ExpressionOutputTest(target, "z", "3");
            ExpressionOutputTest(target, "arr -> [3,6,9,12]", "3,6,9,12");
            ExpressionOutputTest(target, "arr[1]", "6");
            ExpressionOutputTest(target, "arr[1+1]", "9");
            ExpressionOutputTest(target, "arr[2+44]", "0");

            ExpressionOutputTest(target, "log(sin(((25*200) - 5000)^1943),2)", "log(sin(((25*200) - 5000)^1943),2)".Interpret());
            ExpressionOutputTest(target, "log(cos(((25*200) - 5000)^1943),2)", "log(cos(((25*200) - 5000)^1943),2)".Interpret());

        }

        [TestMethod]
        public void AsyncTest()
        {
            InputInterpreter interpreter = new InputInterpreter();
            interpreter.InterpretAsync("2 + 2", i =>
            {
                Assert.IsTrue(i.Output == "4");
            });
        }

        [TestMethod]
        public void ComputableTest()
        {
            InputInterpreter interpreter = new InputInterpreter();
            
            interpreter.Interpret("1");
            Assert.IsTrue(interpreter.AsComputable(c => c.Add(5)).Resolve().Decimal == 6);
            Assert.IsTrue(interpreter.AsComputable(c => c.Subtract(5)).Resolve().Decimal == 1);
            Assert.IsTrue(interpreter.AsComputable(c => c.Multiply(5)).Resolve().Decimal == 5);
            Assert.IsTrue(interpreter.AsComputable(c => c.Divide(1)).Resolve().Decimal == 5);
            Assert.IsTrue(interpreter.AsComputable(c => c.Mod(1)).Resolve().Decimal == 0);
            Assert.IsTrue(interpreter.AsComputable(c => c.RaiseToPower(5)).Resolve().Decimal == 0);

            interpreter.Interpret("1");
            Assert.IsTrue(interpreter.ToComputable().Add(5).Resolve().Decimal == 6);
            Assert.IsTrue(interpreter.ToComputable().Subtract(5).Resolve().Decimal == 1);
            Assert.IsTrue(interpreter.ToComputable().Multiply(5).Resolve().Decimal == 5);
            Assert.IsTrue(interpreter.ToComputable().Divide(1).Resolve().Decimal == 5);
            Assert.IsTrue(interpreter.ToComputable().Mod(1).Resolve().Decimal == 0);
            Assert.IsTrue(interpreter.ToComputable().RaiseToPower(5).Resolve().Decimal == 0);

            interpreter.Interpret("1");
            Assert.IsTrue(interpreter.AsComputable(c => c.Add(5)).ResolveTo(i => i.Decimal) == 6);
            Assert.IsTrue(interpreter.AsComputable(c => c.Subtract(5)).ResolveTo(i => i.Int) == 1);
            
            interpreter.Interpret("5");
            int[] ia = new int[] { 1,2,3 };
            var test = interpreter.AsComputable(computable => computable.ForAll(ia, sub => sub.RaiseToPower)).Resolve();
            Assert.IsTrue(test.Output == "15625");
        }

        /// <summary>
        /// Tests a value repeated 10,000 times and checks for an execution time less than 200 milliseconds.
        /// </summary>
        /// <param name="target">The input interpreter.</param>
        /// <param name="value">The value to repeat and test.</param>
        public void HighLoadTest(InputInterpreter target, string value)
        {
            StringBuilder builder = new StringBuilder();
            string input = string.Empty;
            System.TimeSpan baseTime = new System.TimeSpan();
            for (int i = 0; i < 1000; i++)
            {
                builder.Append(value);
            }
            input = builder.ToString();
            target.Interpret(input);
            baseTime = target.LastExecutionTime;

            builder = new StringBuilder();
            for (int i = 0; i < 100000; i++)
            {
                builder.Append(value);
            }
            input = builder.ToString();
            target.Interpret(input);
            
            Assert.IsTrue(target.LastExecutionTime <= System.TimeSpan.FromTicks((baseTime.Ticks + 1 * 100) + 100), string.Format("{0} failed the high load test.", value));
        }

        /// <summary>
        /// Tests a given expression for a decimal result.
        /// </summary>
        /// <param name="target">The input interpreter.</param>
        /// <param name="input">The input to execute.</param>
        /// <param name="expected">The expected value.</param>
        public void ExpressionDecimalTest(InputInterpreter target, string input, decimal expected)
        {
            target.Interpret(input);
            Assert.AreEqual(expected, target.Decimal, string.Format("{0} returned {1} but {2} was expected.", input, target.Decimal, expected));
        }

        /// <summary>
        /// Tests a given expression for a string result.
        /// </summary>
        /// <param name="target">The input interpreter.</param>
        /// <param name="input">The input to execute.</param>
        /// <param name="expected">The expected value.</param>
        public void ExpressionOutputTest(InputInterpreter target, string input, string expected)
        {
            target.Interpret(input);
            Assert.AreEqual(expected, target.Output, string.Format("{0} returned {1} but {2} was expected.", input, target.Decimal, expected));
        }
    }
}
