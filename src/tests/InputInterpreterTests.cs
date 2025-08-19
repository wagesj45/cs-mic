using System.Globalization;
using csmic;

namespace tests;

public class InputInterpreterTests
{
    private InputInterpreter _interp = null!;

    [SetUp]
    public void Setup()
    {
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        _interp = new InputInterpreter();
    }

    private static void AssertSuccess(decimal result, decimal expected, InputInterpreter interp)
    {
        Assert.That(result, Is.EqualTo(expected));
        Assert.That(interp.NumericValue, Is.EqualTo(expected));
        Assert.That(interp.StringValue, Is.EqualTo(string.Empty));
    }

    private static void AssertSoftError(decimal result, InputInterpreter interp)
    {
        Assert.That(result, Is.EqualTo(0m));
        Assert.That(interp.NumericValue, Is.EqualTo(0m));
        Assert.That(string.IsNullOrEmpty(interp.StringValue), Is.False);
    }

    [TestCase("2", 2)]
    [TestCase("-5", -5)]
    [TestCase("2+3*4", 14)]
    [TestCase("(2+3)*4", 20)]
    [TestCase("8/2", 4)]
    [TestCase("7%4", 3)]
    [TestCase("2^3", 8)]
    [TestCase("1e2", 100)]
    [TestCase("0xFF", 255)]
    [TestCase("1010b", 10)]
    public void Arithmetic_Works(string expr, decimal expected)
    {
        var result = _interp.Interpret(expr);
        AssertSuccess(result, expected, _interp);
    }

    [TestCase("2==2", 1)]
    [TestCase("2<3", 1)]
    [TestCase("3<2", 0)]
    [TestCase("2>=2", 1)]
    [TestCase("2<=1", 0)]
    public void Comparisons_YieldBooleanAsNumeric(string expr, decimal expected)
    {
        var result = _interp.Interpret(expr);
        AssertSuccess(result, expected, _interp);
    }

    [Test]
    public void Variables_AssignAndUse_PersistAcrossCalls()
    {
        AssertSuccess(_interp.Interpret("x :: 2"), 2, _interp);
        AssertSuccess(_interp.Interpret("x+1"), 3, _interp);
        AssertSuccess(_interp.Interpret("x :: 3"), 3, _interp);
        AssertSuccess(_interp.Interpret("x+1"), 4, _interp);
    }

    [Test]
    public void UnknownVariable_ProducesSoftError()
    {
        var result = _interp.Interpret("nope + 1");
        AssertSoftError(result, _interp);
    }

    [Test]
    public void ExpressionBinding_EvaluatesAtUseTime()
    {
        AssertSuccess(_interp.Interpret("num :: 2"), 2, _interp);
        AssertSuccess(_interp.Interpret("exp := 1 + num"), 0, _interp); // assignment returns 0
        AssertSuccess(_interp.Interpret("exp"), 3, _interp);

        AssertSuccess(_interp.Interpret("num :: 3"), 3, _interp);
        AssertSuccess(_interp.Interpret("exp"), 4, _interp);
    }

    [Test]
    public void ExpressionBinding_InvalidExpression_ProducesSoftError()
    {
        _interp.Interpret("exp := bad + 1");
        var result = _interp.Interpret("exp");
        AssertSoftError(result, _interp);
    }

    [Test]
    public void Arrays_AssignAndIndex()
    {
        AssertSuccess(_interp.Interpret("arr -> [1,2,3]"), 0, _interp); // array assignment returns 0
        AssertSuccess(_interp.Interpret("arr[0]"), 1, _interp);
        AssertSuccess(_interp.Interpret("arr[2]"), 3, _interp);
    }

    [Test]
    public void Arrays_OutOfRange_ProducesSoftError()
    {
        _interp.Interpret("arr -> [1,2,3]");
        var result = _interp.Interpret("arr[3]");
        AssertSoftError(result, _interp);
    }

    [Test]
    public void Arrays_IndexExpression_Works()
    {
        _interp.Interpret("arr -> [1,2,3]");
        _interp.Interpret("i :: 1");
        AssertSuccess(_interp.Interpret("arr[i+1]"), 3, _interp);
    }

    [Test]
    public void StringLiteral_Alone_IsError()
    {
        var result = _interp.Interpret("\"hi\"");
        AssertSoftError(result, _interp);
    }

    [Test]
    public void DivideByZero_ProducesSoftError()
    {
        var result = _interp.Interpret("1/0");
        AssertSoftError(result, _interp);
    }

    [Test]
    public void LastExecutionTime_IsMeasured()
    {
        _interp.Interpret("2+2");
        Assert.That(_interp.LastExecutionTime, Is.GreaterThanOrEqualTo(TimeSpan.Zero));
        _interp.Interpret("3+3");
        Assert.That(_interp.LastExecutionTime, Is.GreaterThanOrEqualTo(TimeSpan.Zero));
    }
}
