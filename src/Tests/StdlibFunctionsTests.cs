using CSMic;
using CSMic.StandardLibrary;
using NUnit.Framework;
using CSMic.StandardLibrary.Functions;
using System.Globalization;

namespace CSMic.Tests;

public class StdlibFunctionsTests
{
    private InputInterpreter _interp = null!;

    [SetUp]
    public void Setup()
    {
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        _interp = new InputInterpreter();
        // Initialize full standard library (functions + constants)
        Initializer.InitializeAll(_interp);
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
    [TestCase("2 + abs(-1)", 3)]
    public void AbsoluteValue_Works(string expr, decimal expected)
    {
        var result = _interp.Interpret(expr);
        AssertSuccess(result, expected, _interp);
    }

    [TestCase("abs(-3.5)", "3.5")]
    public void AbsoluteValue_Works_Decimal(string expr, string expectedStr)
    {
        var result = _interp.Interpret(expr);
        var expected = decimal.Parse(expectedStr, CultureInfo.InvariantCulture);
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
    public void Min_Works(string expr, decimal expected)
    {
        var result = _interp.Interpret(expr);
        AssertSuccess(result, expected, _interp);
    }

    [TestCase("min(2.5, 2.6)", "2.5")]
    public void Min_Works_Decimals(string expr, string expectedStr)
    {
        var result = _interp.Interpret(expr);
        var expected = decimal.Parse(expectedStr, CultureInfo.InvariantCulture);
        AssertSuccess(result, expected, _interp);
    }

    [TestCase("max(1,2)", 2)]
    [TestCase("max(2,1)", 2)]
    [TestCase("max(-3,4)", 4)]
    [TestCase("1 + max(3,4)", 5)]
    public void Max_Works(string expr, decimal expected)
    {
        var result = _interp.Interpret(expr);
        AssertSuccess(result, expected, _interp);
    }

    [TestCase("max(2.5, 2.6)", "2.6")]
    public void Max_Works_Decimals(string expr, string expectedStr)
    {
        var result = _interp.Interpret(expr);
        var expected = decimal.Parse(expectedStr, CultureInfo.InvariantCulture);
        AssertSuccess(result, expected, _interp);
    }

    [TestCase("sqrt(25)", 5)]
    [TestCase("sqrt(1)", 1)]
    public void Sqrt_Works(string expr, decimal expected)
    {
        var result = _interp.Interpret(expr);
        AssertSuccess(result, expected, _interp);
    }

    [TestCase("pi", "3.1415926535897931")]
    [TestCase("e", "2.7182818284590451")]
    [TestCase("tau", "6.2831853071795862")]
    [TestCase("phi", "1.6180339887498948")]
    [TestCase("goldenratio", "1.6180339887498948")]
    [TestCase("eurler", "0.5772156649015329")]
    [TestCase("omega", "0.5671432904097839")]
    public void Constants_AreAvailable(string expr, string expectedStr)
    {
        var result = _interp.Interpret(expr);
        var expected = decimal.Parse(expectedStr, CultureInfo.InvariantCulture);
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
