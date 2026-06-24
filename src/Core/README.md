# CSMic

`CSMic` is the core CS-MIC package. It provides the expression parser, interpreter runtime, variable storage, array support, expression bindings, soft-error reporting, and the custom function API.

Install it when you want the parser and runtime without the optional standard-library functions.

```sh
dotnet add package CSMic
```

## What It Provides

- `InputInterpreter`, the main API for parsing and evaluating input.
- Numeric expression support for arithmetic, powers, modulus, parentheses, comparisons, implicit multiplication, decimal literals, hexadecimal literals, and binary literals.
- Persistent interpreter state for numeric variables, expression variables, and numeric arrays.
- Soft-error behavior through `StringValue` instead of throwing for normal parse and evaluation failures.
- `ICodedFunction`, `FunctionArgument`, and `FunctionValue` for host-provided functions.

## Basic Usage

```csharp
using CSMic;

var interpreter = new InputInterpreter();

decimal result = interpreter.Interpret("2 + 3 * 4");
// result == 14
```

`Interpret` returns the latest numeric value and updates these properties:

- `NumericValue`: the numeric result of the latest successful expression, or `0` for a soft error.
- `StringValue`: empty for a successful numeric expression, or an error message for a soft error.
- `LastExecutionTime`: elapsed time for the latest interpretation.
- `Variables`: the current numeric, expression, and array variables.

## Variables

Numeric variables use `::`.

```csharp
interpreter.Interpret("x :: 4");
interpreter.Interpret("x + 1"); // 5
```

Expression variables use `:=` and are evaluated when referenced.

```csharp
interpreter.Interpret("x :: 2");
interpreter.Interpret("y := x + 1");
interpreter.Interpret("y"); // 3

interpreter.Interpret("x :: 5");
interpreter.Interpret("y"); // 6
```

Numeric arrays use `->` and zero-based indexes.

```csharp
interpreter.Interpret("values -> [1, 2, 3]");
interpreter.Interpret("values[2]"); // 3
```

## Custom Functions

Implement `ICodedFunction` and register it with the interpreter.

```csharp
using CSMic;

public sealed class Square : ICodedFunction
{
    public string Name => "square";

    public IEnumerable<FunctionArgument> ExpectedArguments =>
        new[] { new FunctionArgument("value", FunctionValue.NUMBER) };

    public FunctionValue ReturnValue => FunctionValue.NUMBER;

    public FunctionValue Execute(params FunctionArgument[] args)
    {
        var value = (decimal)args[0].Value.Value!;
        return new FunctionValue(FunctionValueType.Numeric, value * value);
    }
}

var interpreter = new InputInterpreter();
interpreter.RegisterFunction(new Square());

interpreter.Interpret("square(5)"); // 25
```

Functions can accept numeric and string arguments. String literals are only valid in function argument positions; general expression results remain numeric-first.

## When To Use This Package

Use `CSMic` directly when your application owns the function set, wants a small expression runtime, or needs to keep end-user functions tightly scoped to your domain.

Use `CSMic.StandardLibrary` when you also want ready-made constants and common math functions.
