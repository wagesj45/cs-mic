using csmic;
using NUnit.Framework;
using stdlib.functions;

namespace tests;

public class StdlibFunctionsTests
{
    private static FunctionArgument NumArg(decimal d) => new FunctionArgument("value", new FunctionValue(ValueType.Numeric, d));

    [Test]
    public void AbsoluteValue_Positive_ReturnsSame()
    {
        var fn = new AbsoluteValue();
        var result = fn.Execute(NumArg(5m));
        Assert.That(result.Type, Is.EqualTo(ValueType.Numeric));
        Assert.That(result.Value, Is.EqualTo(5m));
    }

    [Test]
    public void AbsoluteValue_Negative_ReturnsPositive()
    {
        var fn = new AbsoluteValue();
        var result = fn.Execute(NumArg(-12.5m));
        Assert.That(result.Type, Is.EqualTo(ValueType.Numeric));
        Assert.That(result.Value, Is.EqualTo(12.5m));
    }

    [Test]
    public void AbsoluteValue_Zero_ReturnsZero()
    {
        var fn = new AbsoluteValue();
        var result = fn.Execute(NumArg(0m));
        Assert.That(result.Type, Is.EqualTo(ValueType.Numeric));
        Assert.That(result.Value, Is.EqualTo(0m));
    }

    [Test]
    public void AbsoluteValue_InvalidArgType_ReturnsNone()
    {
        var fn = new AbsoluteValue();
        var badArg = new FunctionArgument("value", FunctionValue.STRING);
        var result = fn.Execute(badArg);
        Assert.That(result.Type, Is.EqualTo(ValueType.None));
        Assert.That(result.Value, Is.Null);
    }

    [Test]
    public void AbsoluteValue_WrongArgCount_ReturnsNone()
    {
        var fn = new AbsoluteValue();
        var result0 = fn.Execute();
        var result2 = fn.Execute(NumArg(1m), NumArg(2m));
        Assert.That(result0.Type, Is.EqualTo(ValueType.None));
        Assert.That(result2.Type, Is.EqualTo(ValueType.None));
    }

    [Test]
    public void Sign_Negative_ReturnsNegativeOne()
    {
        var fn = new Sign();
        var result = fn.Execute(NumArg(-1m));
        Assert.That(result.Type, Is.EqualTo(ValueType.Numeric));
        Assert.That(result.Value, Is.EqualTo(Sign.NEGATIVE));
    }

    [Test]
    public void Sign_Zero_ReturnsPositiveOne()
    {
        var fn = new Sign();
        var result = fn.Execute(NumArg(0m));
        Assert.That(result.Type, Is.EqualTo(ValueType.Numeric));
        Assert.That(result.Value, Is.EqualTo(Sign.POSITIVE));
    }

    [Test]
    public void Sign_Positive_ReturnsPositiveOne()
    {
        var fn = new Sign();
        var result = fn.Execute(NumArg(99.99m));
        Assert.That(result.Type, Is.EqualTo(ValueType.Numeric));
        Assert.That(result.Value, Is.EqualTo(Sign.POSITIVE));
    }

    [Test]
    public void Sign_InvalidArgType_ReturnsNone()
    {
        var fn = new Sign();
        var badArg = new FunctionArgument("value", FunctionValue.STRING);
        var result = fn.Execute(badArg);
        Assert.That(result.Type, Is.EqualTo(ValueType.None));
        Assert.That(result.Value, Is.Null);
    }

    [Test]
    public void Sign_WrongArgCount_ReturnsNone()
    {
        var fn = new Sign();
        var result0 = fn.Execute();
        var result2 = fn.Execute(NumArg(1m), NumArg(2m));
        Assert.That(result0.Type, Is.EqualTo(ValueType.None));
        Assert.That(result2.Type, Is.EqualTo(ValueType.None));
    }
}

