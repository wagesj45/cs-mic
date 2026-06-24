# CS-MIC

CS-MIC is a small, embeddable expression interpreter for .NET applications. It is designed for places where users need to enter flexible numeric input, while the host application needs a deterministic decimal result and a controlled extension surface.

![Build](https://git.jordanwages.com/wagesj45/cs-mic/actions/workflows/build.yml/badge.svg?branch=main)
![Tests](https://git.jordanwages.com/wagesj45/cs-mic/actions/workflows/tests.yml/badge.svg?branch=main)

The 2.0 release separates the project into two NuGet packages:

- `CSMic`: the core parser, interpreter, variable store, and custom function API.
- `CSMic.StandardLibrary`: optional constants and common math functions built on top of the core package.

CS-MIC targets `netstandard2.1`.

## Installation

Install the core interpreter when you want to parse expressions and provide your own functions:

```sh
dotnet add package CSMic
```

Install the standard library when you also want built-in constants and math helpers:

```sh
dotnet add package CSMic.StandardLibrary
```

`CSMic.StandardLibrary` references `CSMic`, so applications that use the standard library do not need to install both packages explicitly.

## Basic Usage

```csharp
using CSMic;

var interpreter = new InputInterpreter();

decimal result = interpreter.Interpret("2 + 3 * 4");
// result == 14
// interpreter.NumericValue == 14
```

`Interpret` returns the numeric result and also stores the last output on the interpreter. Parse and runtime errors are soft errors: the interpreter returns `0` and writes the error message to `StringValue`.

```csharp
decimal result = interpreter.Interpret("1 / 0");

if (!string.IsNullOrEmpty(interpreter.StringValue))
{
    Console.WriteLine(interpreter.StringValue);
}
```

Create a new interpreter for an isolated evaluation context. Reuse an interpreter when variables, arrays, expression bindings, and registered functions should persist across calls.

## Expressions

CS-MIC evaluates numeric expressions with the usual precedence rules for parentheses, powers, multiplication, division, modulus, addition, and subtraction.

| Input | Result |
| --- | ---: |
| `5 + 5` | `10` |
| `1 + 2 * 3` | `7` |
| `(1 + 2) * 3` | `9` |
| `2 ^ 8` | `256` |
| `7 % 4` | `3` |
| `2(3 + 1)` | `8` |

Comparison operators return numeric booleans: `1` for true and `0` for false.

| Input | Result |
| --- | ---: |
| `2 == 2` | `1` |
| `2 < 3` | `1` |
| `3 < 2` | `0` |
| `2 >= 2` | `1` |
| `2 <= 1` | `0` |

## Literals

Numbers are decimal by default. Hexadecimal values use a `0x` prefix, and binary values use a `b` suffix.

| Input | Result |
| --- | ---: |
| `100` | `100` |
| `0xFF` | `255` |
| `1010b` | `10` |
| `0xFF * 1010b` | `2550` |

String literals are accepted only as function arguments. They are not standalone expression values, variables, or arithmetic operands.

## Variables And Arrays

Use `::` to assign a numeric value. Numeric variables are evaluated immediately and persist on the interpreter.

```csharp
interpreter.Interpret("x :: 4");  // 4
interpreter.Interpret("x + 6");   // 10
```

Use `:=` to assign an expression binding. Expression bindings are evaluated when referenced, so they can reflect later changes to other variables.

```csharp
interpreter.Interpret("x :: 2");
interpreter.Interpret("doubleX := 2 * x");

interpreter.Interpret("doubleX"); // 4

interpreter.Interpret("x :: 5");
interpreter.Interpret("doubleX"); // 10
```

Use `->` to assign a numeric array, then index it with zero-based indexes.

```csharp
interpreter.Interpret("values -> [10, 20, 30]");
interpreter.Interpret("values[1]"); // 20
```

## Standard Library

Add `CSMic.StandardLibrary` and initialize the interpreter to register the standard functions and constants:

```csharp
using CSMic;
using CSMic.StandardLibrary;

var interpreter = new InputInterpreter();
Initializer.InitializeAll(interpreter);

decimal area = interpreter.Interpret("pi * 10^2");
decimal angle = interpreter.Interpret("degrees(pi / 2)");
```

`InitializeAll` registers all functions and constants. You can also opt into smaller groups with `InitializeAllFunctions`, `InitializeConstants`, `InitializeBaseFunctions`, `InitializeAngleFunctions`, `InitializeRoundingFunctions`, `InitializeTrigonometryFunctions`, `InitializeNumberTheoryFunctions`, and `InitializeRandomFunctions`.

The standard library includes:

- Base functions: `abs`, `sign`, `min`, `max`
- Angle helpers: `degrees`, `radians`, `wrapangle`
- Rounding helpers: `floor`, `ceiling`, `truncate`, `frac`, `round`, `clamp`
- Trigonometry: `sin`, `cos`, `tan`, `asin`, `acos`, `atan`, `atan2`
- Hyperbolic trigonometry: `sinh`, `cosh`, `tanh`, `asinh`, `acosh`, `atanh`
- Number theory: `fac`, `ncr`, `npr`, `gcd`, `lcm`
- Random helpers: `flip`, `bern`, `rand`, `rands`, `randn`, `randns`
- Constants: `pi`, `e`, `tau`, `phi`, `goldenratio`, `eurler`, `omega`

## Custom Functions

Register custom functions by implementing `ICodedFunction`.

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

decimal result = interpreter.Interpret("square(12)");
// result == 144
```

Functions can accept numeric or string arguments. String arguments are useful for host-defined keys, modes, or labels while preserving CS-MIC's numeric-first expression model.

## Building From Source

```sh
dotnet restore src/CsMic.sln
dotnet test src/CsMic.sln
dotnet pack src/Core/CSMic.Core.csproj -c Release
dotnet pack src/StandardLibrary/CSMic.StandardLibrary.csproj -c Release
```

The core project uses Coco/R during build to generate parser and scanner code from `src/Core/cocor/Interpreter.atg`.
