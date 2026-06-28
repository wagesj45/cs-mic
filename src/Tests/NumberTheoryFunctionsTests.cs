using System.Globalization;
using CSMic;
using CSMic.StandardLibrary;

namespace CSMic.Tests;

public class NumberTheoryFunctionsTests
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
    public void Gcd_Works_And_Validates()
    {
        AssertSuccess(_interp.Interpret("gcd(54, 24)"), 6m, _interp);
        AssertSuccess(_interp.Interpret("gcd(7, 3)"), 1m, _interp);
        AssertSuccess(_interp.Interpret("gcd(0, 5)"), 5m, _interp);
        AssertSuccess(_interp.Interpret("gcd(5.5, 2)"), 0m, _interp);
    }

    [Test]
    public void Lcm_Works_And_Validates()
    {
        AssertSuccess(_interp.Interpret("lcm(4, 6)"), 12m, _interp);
        AssertSuccess(_interp.Interpret("lcm(21, 6)"), 42m, _interp);
        AssertSuccess(_interp.Interpret("lcm(-2, 6)"), 0m, _interp);
        AssertSuccess(_interp.Interpret("lcm(2.5, 6)"), 0m, _interp);
    }

    [Test]
    public void BinomialCoefficient_Works_And_Validates()
    {
        AssertSuccess(_interp.Interpret("ncr(5, 2)"), 10m, _interp);
        AssertSuccess(_interp.Interpret("ncr(20, 0)"), 1m, _interp);
        AssertSuccess(_interp.Interpret("ncr(5, -1)"), 0m, _interp);
        AssertSuccess(_interp.Interpret("ncr(5, 6)"), 0m, _interp);
        AssertSuccess(_interp.Interpret("ncr(5.2, 2)"), 0m, _interp);
    }

    [Test]
    public void Permutations_Works_And_Validates()
    {
        AssertSuccess(_interp.Interpret("npr(5, 2)"), 20m, _interp);
        AssertSuccess(_interp.Interpret("npr(6, 6)"), 720m, _interp);
        AssertSuccess(_interp.Interpret("npr(5, 6)"), 0m, _interp);
        AssertSuccess(_interp.Interpret("npr(5.2, 2)"), 0m, _interp);
    }

    [Test]
    public void Factorial_Works_And_Validates()
    {
        AssertSuccess(_interp.Interpret("fac(0)"), 1m, _interp);
        AssertSuccess(_interp.Interpret("fac(1)"), 1m, _interp);
        AssertSuccess(_interp.Interpret("fac(5)"), 120m, _interp);
        AssertSuccess(_interp.Interpret("fac(20)"), 2432902008176640000m, _interp);
        AssertSuccess(_interp.Interpret("fac(-1)"), 0m, _interp);
        AssertSuccess(_interp.Interpret("fac(21)"), 0m, _interp);

        var res = _interp.Interpret("fac(0.5)");
        // Gamma(1.5) ~= sqrt(pi)/2 ~= 0.8862269254527579
        AssertApprox(res, 0.8862269254527579m, 0.0000000000001m, _interp);
    }
}

