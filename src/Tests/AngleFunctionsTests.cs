using System.Globalization;
using CSMic;
using CSMic.StandardLibrary;

namespace CSMic.Tests;

public class AngleFunctionsTests
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
    public void Degrees_Works_OnCommonValues()
    {
        AssertSuccess(_interp.Interpret("degrees(0)"), 0m, _interp);
        AssertSuccess(_interp.Interpret("degrees(pi)"), 180m, _interp);
        AssertSuccess(_interp.Interpret("degrees(tau)"), 360m, _interp);
    }

    [Test]
    public void Radians_Works_OnCommonValues()
    {
        AssertSuccess(_interp.Interpret("radians(0)"), 0m, _interp);
        var res = _interp.Interpret("radians(180)");
        // Approximately pi
        AssertApprox(res, 3.1415926535897931m, 0.0000000000001m, _interp);
    }

    [Test]
    public void WrapAngle_WrapsIntoRange()
    {
        AssertSuccess(_interp.Interpret("wrapangle(-10, 0, 360)"), 350m, _interp);
        AssertSuccess(_interp.Interpret("wrapangle(370, 0, 360)"), 10m, _interp);
        AssertSuccess(_interp.Interpret("wrapangle(361, -180, 180)"), -179m, _interp);
    }
}

