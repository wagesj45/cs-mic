using System.Globalization;
using CSMic;
using CSMic.StandardLibrary;

namespace CSMic.Tests;

public class TrigonometryFunctionsTests
{
    private InputInterpreter _interp = null!;

    [SetUp]
    public void Setup()
    {
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        _interp = new InputInterpreter();
        Initializer.InitializeAll(_interp);
    }

    private static void AssertSuccess(decimal result, decimal expected, InputInterpreter interp)
    {
        Assert.That(result, Is.EqualTo(expected));
        Assert.That(interp.NumericValue, Is.EqualTo(expected));
        Assert.That(interp.StringValue, Is.EqualTo(string.Empty));
    }

    private static void AssertApprox(decimal result, decimal expected, decimal tol, InputInterpreter interp)
    {
        Assert.That(Math.Abs(result - expected) <= tol, $"Expected ~{expected} ± {tol}, got {result}");
        Assert.That(interp.StringValue, Is.EqualTo(string.Empty));
    }

    [Test]
    public void BasicTrig_ZeroPoints()
    {
        AssertSuccess(_interp.Interpret("sin(0)"), 0m, _interp);
        AssertSuccess(_interp.Interpret("cos(0)"), 1m, _interp);
        AssertSuccess(_interp.Interpret("tan(0)"), 0m, _interp);
    }

    [Test]
    public void BasicTrig_CommonAngles()
    {
        AssertApprox(_interp.Interpret("sin(pi/2)"), 1m, 0.0000000000001m, _interp);
        AssertApprox(_interp.Interpret("cos(pi/2)"), 0m, 0.0000000000001m, _interp);
        AssertApprox(_interp.Interpret("tan(pi/4)"), 1m, 0.0000000000001m, _interp);
    }

    [Test]
    public void InverseTrig_ZeroPoints()
    {
        AssertSuccess(_interp.Interpret("asin(0)"), 0m, _interp);
        AssertSuccess(_interp.Interpret("acos(1)"), 0m, _interp);
        AssertSuccess(_interp.Interpret("atan(0)"), 0m, _interp);
        AssertSuccess(_interp.Interpret("atan2(0, 1)"), 0m, _interp);
    }

    [Test]
    public void InverseTrig_CommonValues()
    {
        // asin(1) ~= pi/2, acos(0) ~= pi/2, atan(1) ~= pi/4, atan2(1,0) ~= pi/2
        AssertApprox(_interp.Interpret("asin(1)"), 1.5707963267948966m, 0.0000000000001m, _interp);
        AssertApprox(_interp.Interpret("acos(0)"), 1.5707963267948966m, 0.0000000000001m, _interp);
        AssertApprox(_interp.Interpret("atan(1)"), 0.7853981633974483m, 0.0000000000001m, _interp);
        AssertApprox(_interp.Interpret("atan2(1, 0)"), 1.5707963267948966m, 0.0000000000001m, _interp);
    }

    [Test]
    public void ImplicitMultiplication_WithFunctions()
    {
        AssertSuccess(_interp.Interpret("2sin(0)"), 0m, _interp);
        AssertSuccess(_interp.Interpret("2 sin(0)"), 0m, _interp);
        AssertApprox(_interp.Interpret("2sin(pi/2)"), 2m, 0.0000000000001m, _interp);
    }
}

public class HyperbolicTrigFunctionsTests
{
    private InputInterpreter _interp = null!;

    [SetUp]
    public void Setup()
    {
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        _interp = new InputInterpreter();
        Initializer.InitializeAll(_interp);
    }

    private static void AssertSuccess(decimal result, decimal expected, InputInterpreter interp)
    {
        Assert.That(result, Is.EqualTo(expected));
        Assert.That(interp.NumericValue, Is.EqualTo(expected));
        Assert.That(interp.StringValue, Is.EqualTo(string.Empty));
    }

    [Test]
    public void Hyperbolic_ZeroPoints()
    {
        AssertSuccess(_interp.Interpret("sinh(0)"), 0m, _interp);
        AssertSuccess(_interp.Interpret("cosh(0)"), 1m, _interp);
        AssertSuccess(_interp.Interpret("tanh(0)"), 0m, _interp);
        AssertSuccess(_interp.Interpret("asinh(0)"), 0m, _interp);
        AssertSuccess(_interp.Interpret("acosh(1)"), 0m, _interp);
        AssertSuccess(_interp.Interpret("atanh(0)"), 0m, _interp);
    }
}
