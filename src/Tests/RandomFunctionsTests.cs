using System.Globalization;
using CSMic;
using CSMic.StandardLibrary;

namespace CSMic.Tests;

public class RandomFunctionsTests
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
    public void Flip_ReturnsBooleanAsNumeric()
    {
        for (int i = 0; i < 5; i++)
        {
            var res = _interp.Interpret("flip()");
            Assert.That(res == 0m || res == 1m, "flip() must return 0 or 1");
            Assert.That(_interp.StringValue, Is.EqualTo(string.Empty));
        }
    }

    [Test]
    public void Rand_IsWithinUnitInterval()
    {
        for (int i = 0; i < 5; i++)
        {
            var res = _interp.Interpret("rand()");
            Assert.That(res, Is.GreaterThanOrEqualTo(0m));
            Assert.That(res, Is.LessThan(1m));
            Assert.That(_interp.StringValue, Is.EqualTo(string.Empty));
        }
    }

    [Test]
    public void RandSpread_IsWithinBounds_And_Validates()
    {
        for (int i = 0; i < 5; i++)
        {
            var res = _interp.Interpret("rands(5, 10)");
            Assert.That(res, Is.GreaterThanOrEqualTo(5m));
            Assert.That(res, Is.LessThan(10m));
            Assert.That(_interp.StringValue, Is.EqualTo(string.Empty));
        }

        AssertSuccess(_interp.Interpret("rands(10, 5)"), 0m, _interp);
    }

    [Test]
    public void RandNormal_ProducesNumeric()
    {
        bool sawNonZero = false;
        for (int i = 0; i < 10; i++)
        {
            var res = _interp.Interpret("randn()");
            if (res != 0m) sawNonZero = true;
            Assert.That(_interp.StringValue, Is.EqualTo(string.Empty));
        }
        Assert.That(sawNonZero, Is.True);
    }

    [Test]
    public void RandNormalSpread_ValidatesBounds()
    {
        AssertSuccess(_interp.Interpret("randns(10, 5)"), 0m, _interp);
        // With valid bounds, it should succeed and produce a numeric (any value)
        var res = _interp.Interpret("randns(5, 10)");
        Assert.That(_interp.StringValue, Is.EqualTo(string.Empty));
    }

    [Test]
    public void Bernoulli_ValidatesP_And_ReturnsBoolean()
    {
        // Invalid p produces soft error (message populated)
        _interp.Interpret("bern(-0.1)");
        Assert.That(string.IsNullOrEmpty(_interp.StringValue), Is.False);
        _interp.Interpret("bern(1.1)");
        Assert.That(string.IsNullOrEmpty(_interp.StringValue), Is.False);

        // Valid p returns 0 or 1 without error
        var res = _interp.Interpret("bern(0.7)");
        Assert.That(res == 0m || res == 1m);
        Assert.That(_interp.StringValue, Is.EqualTo(string.Empty));
    }
}
