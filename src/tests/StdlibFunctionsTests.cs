using csmic;
using csmic.stdlib;
using NUnit.Framework;
using stdlib;
using stdlib.functions;
using System.Globalization;
using System.Reflection.Metadata;

namespace tests;

public class StdlibFunctionsTests
{
    private InputInterpreter _interp = null!;

    [SetUp]
    public void Setup()
    {
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        _interp = new InputInterpreter();
        Constants.Initialize(_interp);
        Functions.Initialize(_interp); ;
    }

    private static void AssertSuccess(decimal result, decimal expected, InputInterpreter interp)
    {
        Assert.That(result, Is.EqualTo(expected));
        Assert.That(interp.NumericValue, Is.EqualTo(expected));
        Assert.That(interp.StringValue, Is.EqualTo(string.Empty));
    }

    [TestCase("abs(-1)", 1)]
    [TestCase("abs(0)", 0)]
    [TestCase("abs(2)", 2)]
    [TestCase("abs(-3.5)", 3.5m)]
    [TestCase("2 + abs(-1)", 3)]
    public void AbsoluteValue_Works(string expr, decimal expected)
    {
        var result = _interp.Interpret(expr);
        AssertSuccess(result, expected, _interp);
    }

    [TestCase("sign(5)", 1)]
    [TestCase("sign(0)", 1)]
    [TestCase("sign(-2)", -1)]
    [TestCase("2*sign(-3)", -2)]
    public void Sign_Works(string expr, decimal expected)
    {
        var result = _interp.Interpret(expr);
        AssertSuccess(result, expected, _interp);
    }

    [TestCase("min(1,2)", 1)]
    [TestCase("min(2,1)", 1)]
    [TestCase("min(-3,4)", -3)]
    [TestCase("1 + min(3,4)", 4)]
    [TestCase("min(2.5, 2.6)", 2.5m)]
    public void Min_Works(string expr, decimal expected)
    {
        var result = _interp.Interpret(expr);
        AssertSuccess(result, expected, _interp);
    }

    [TestCase("max(1,2)", 2)]
    [TestCase("max(2,1)", 2)]
    [TestCase("max(-3,4)", 4)]
    [TestCase("1 + max(3,4)", 5)]
    [TestCase("max(2.5, 2.6)", 2.6m)]
    public void Max_Works(string expr, decimal expected)
    {
        var result = _interp.Interpret(expr);
        AssertSuccess(result, expected, _interp);
    }

    [TestCase("pi", 3.1415926535897931m)]
    [TestCase("e", 2.7182818284590451m)]
    [TestCase("tau", 6.2831853071795862m)]
    [TestCase("phi", 1.6180339887498948m)]
    [TestCase("goldenratio", 1.6180339887498948m)]
    [TestCase("eurler", 0.5772156649015329m)]
    [TestCase("omega", 0.5671432904097839m)]
    public void Constants_AreAvailable(string expr, decimal expected)
    {
        var result = _interp.Interpret(expr);
        AssertSuccess(result, expected, _interp);
    }

    [Test]
    public void Constants_CanBeUsedInArithmetic()
    {
        AssertSuccess(_interp.Interpret("2*pi"), 6.2831853071795862m, _interp);
        AssertSuccess(_interp.Interpret("pi + e"), 5.8598744820488382m, _interp);
        AssertSuccess(_interp.Interpret("tau / pi"), 2m, _interp);
    }
}
