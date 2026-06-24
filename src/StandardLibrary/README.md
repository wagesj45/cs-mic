# CSMic.StandardLibrary

`CSMic.StandardLibrary` adds optional functions and constants to the core CS-MIC interpreter. It is intended for applications that want end-user convenience functions without having to implement and register each one manually.

This package references `CSMic`.

```sh
dotnet add package CSMic.StandardLibrary
```

## Basic Usage

```csharp
using CSMic;
using CSMic.StandardLibrary;

var interpreter = new InputInterpreter();
Initializer.InitializeAll(interpreter);

decimal result = interpreter.Interpret("max(10, abs(-12))");
// result == 12
```

`Initializer.InitializeAll` registers every standard function and constant. Use a narrower initializer when you only want part of the library:

- `InitializeAllFunctions`
- `InitializeConstants`
- `InitializeBaseFunctions`
- `InitializeAngleFunctions`
- `InitializeRoundingFunctions`
- `InitializeTrigonometryFunctions`
- `InitializeNumberTheoryFunctions`
- `InitializeRandomFunctions`

## Constants

`InitializeConstants` registers:

- `pi`
- `e`
- `tau`
- `phi`
- `goldenratio`
- `eurler`
- `omega`

Constants are stored as interpreter variables, so they can be used in normal expressions:

```csharp
interpreter.Interpret("2 * pi");
interpreter.Interpret("tau / pi");
```

## Functions

Base functions:

- `abs(value)`
- `sign(value)`
- `min(left, right)`
- `max(left, right)`

Angle functions:

- `degrees(radians)`
- `radians(degrees)`
- `wrapangle(value, minimum, maximum)`

Rounding functions:

- `floor(value)`
- `ceiling(value)`
- `truncate(value)`
- `frac(value)`
- `round(value, precision)`
- `clamp(value, minimum, maximum)`

Trigonometry functions:

- `sin(value)`
- `cos(value)`
- `tan(value)`
- `asin(value)`
- `acos(value)`
- `atan(value)`
- `atan2(y, x)`

Hyperbolic trigonometry functions:

- `sinh(value)`
- `cosh(value)`
- `tanh(value)`
- `asinh(value)`
- `acosh(value)`
- `atanh(value)`

Number theory functions:

- `fac(value)`
- `ncr(n, r)`
- `npr(n, r)`
- `gcd(left, right)`
- `lcm(left, right)`

Random functions:

- `flip()`
- `bern(probability)`
- `rand()`
- `rands(minimum, maximum)`
- `randn()`
- `randns(minimum, maximum)`

## Package Role

`CSMic.StandardLibrary` does not replace the core interpreter. It extends an `InputInterpreter` instance by registering `ICodedFunction` implementations and assigning constants. You can combine these functions with your own custom functions on the same interpreter.
