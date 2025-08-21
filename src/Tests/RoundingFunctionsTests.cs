using System.Globalization;
using CSMic;
using CSMic.StandardLibrary;

namespace CSMic.Tests;

public class RoundingFunctionsTests
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
    public void Floor_Works()
    {
        AssertSuccess(_interp.Interpret("floor(2.3)"), 2m, _interp);
        AssertSuccess(_interp.Interpret("floor(-2.3)"), -3m, _interp);
    }

    [Test]
    public void Ceiling_Works()
    {
        AssertSuccess(_interp.Interpret("ceiling(2.3)"), 3m, _interp);
        AssertSuccess(_interp.Interpret("ceiling(-2.3)"), -2m, _interp);
    }

    [Test]
    public void Truncate_Works()
    {
        AssertSuccess(_interp.Interpret("truncate(2.9)"), 2m, _interp);
        AssertSuccess(_interp.Interpret("truncate(-2.9)"), -2m, _interp);
    }

    [Test]
    public void Fractional_Works()
    {
        AssertSuccess(_interp.Interpret("frac(2.75)"), 0.75m, _interp);
        AssertSuccess(_interp.Interpret("frac(-2.75)"), -0.75m, _interp);
    }

    [Test]
    public void Round_WithPrecision_Works()
    {
        AssertSuccess(_interp.Interpret("round(2.346, 2)"), 2.35m, _interp);
        AssertSuccess(_interp.Interpret("round(2.344, 2)"), 2.34m, _interp);
    }

    [Test]
    public void Clamp_Works()
    {
        AssertSuccess(_interp.Interpret("clamp(5, 1, 10)"), 5m, _interp);
        AssertSuccess(_interp.Interpret("clamp(0, 1, 10)"), 1m, _interp);
        AssertSuccess(_interp.Interpret("clamp(11, 1, 10)"), 10m, _interp);
    }
}

