# cs-mic
CS-MIC is a .NET library written in C# designed to give developers easy access to expression parsing. For many developers, there is no need to involve complicated settings and formats. With few exposed methods and objects, CS-MIC makes basic expression solving as simple as possible with as little logic required from developers as possible. In fact, one can pull a value with only one line of code.

***

## Installation

For your convience, CS-MIC is packaged and distributed via nuget. See [our instructions on how to get started](http://cs-mic.com/adding-cs-mic-to-your-project/).

## Usage

CS-MIC follows a few basic rules when interpreting user input, but should prove to be fairly straightforward for most developers. While CS-MIC tries to ensure that the order of operations as far as it knows them (parentheses – powers, multiplication, division – addition, subtraction), there may be instances where formatting fails. If this should be noticed, please [report it](https://github.com/wagesj45/cs-mic/issues).

 
### Expression Parsing

#### Operands

`+` - Addition

`–` - Subtraction

`*` - Multiplication

`/` - Division

`^` - Raise to a power

`%` - Modulus

#### Tokens

`::` - Assign a numeric value to a variable.

`:=` - Assign an equation to a variable.

`->` - Assigns a comma delimited array of numbers to an array variable.

#### Comparison

`==` - Returns ‘True’ if two values are equal to each other.

`>` - Returns ‘True’ if a value is greater than another value.

`<` - Returns ‘True’ if a value is less than another value.

`>=` - Returns ‘True’ if a value is greater than or equal to another value.

`<=` - Returns ‘True’ if a value is greater than or equal to another value.


#### Base Conversion

* Decimal
  * All numbers are interpreted as decimal by default.
* Binary
  * Binary numbers are succeeded by a B, non case sensative.
* Hexadecimal
  * Hexadecimal numbers are preceded by 0x, non case sensative.

#### Example Input and Output

**Input**|**Output**
:-----:|:-----:
`5 + 5` | `10`
`5 * 2` | `10`
`1 + 2 * 3` | `7`
`(1 + 2) * 3` | `9`
`5 / 2` | `2.5`
`5 / 0` | `Attempted to divide by zero.`
`2 ^ 32` | `4294967296`
`x :: 4` | `4`
`x :: 6 + 4` | `10`
`y := 2^x` | `(2^x)`

### Scripting

#### Rules

* Any valid expression can be computed.
* All computation counts towards the script’s history. This means that once a variable is set inside a script, the variable will remain set until execution of the script is complete.
* Only one command can be executed per line. No token is required to end the command.
* Every code block must be opened and closed with a bracket ( { } ). The only exception is the main code block.

#### Loops

`while` - Executes a code block as long as a given condition evaluates to true.

```
while(bool expression)
{
    CODE BLOCK
}
```

`for` - After executing an initial condition, a code block is executed while a condition is met. A final statement is executed at the end of each iteration.

```
for(* expression, bool condition, * expression)
{
    CODE BLOCK
}
```

#### Conditionals

`if` - Executes a code block if a given condition evaluates to true.  An optional else block can follow the if block for execution if the statement evaluates as false.

```
if(bool expression)
{
    CODE BLOCK
}
else
{
    CODE BLOCK
}
```

#### Functions

`echo` - Displays the output from the proceding expression.

```
echo: expression
```

`say` - Displays a string.

```
say: “string”
```
    
`display` - Combines strings and expressions to a single line in the output stack.

```
display: “string”, 12 * 2, “string”, sin(12)
```

`function` - Creates a new function with a given name, and any number of expected arguments.

```
function(newFunction, anyArgumentName)
{
    sin(anyArgumentName) + cos(anyArgumentName)
}
```

#### Comments

* Block Tokens
  * Starting token: /*
  * Ending token: */
    *Any line falling between block comment tokens will be ignored by the macro builder at execution time.
* Line Tokens
  * //
    * Any line starting with the line token will be ignored by the macro builder at execution time.

 
**Example Script**

```
say: “Fibonacci Sequence”
temp :: 1
y :: 1
echo: y
for(x :: 1, x < 11, x :: temp + y)
{
    echo: x
    temp :: y
    y :: x
}
x :: 1
say: “While Loop”
while(x < 10)
{
    if(x == 5)
    {
        display: “The condition ‘x == 5’ has been met. x = “, x, “.”
    }
    else
    {
        echo: sin(x)
    }
    x :: x + 1
}
```

**Example Script Output**
```
Fibonacci Sequence
1
1
2
3
5
8
13
21
34
55
89
While Loop
0.841470984807897
0.909297426825682
0.141120008059867
-0.756802495307928
The condition ‘x == 5’ has been met. x = 5.
-0.279415498198926
0.656986598718789
0.989358246623382
0.412118485241757
```

## Built In Functions

The following is a list of the internally recognized functions in CS-MIC:

`sin(double expression)`

Returns the sine value of a given `expression`.
    
`cos(double expression)`

Returns the cosine value of a given `expression`.

`tan(double expression)`

Returns the tangent value of a given `expression`.

`round(double expression)`

Rounds an `expression` to the nearest whole number.

`sqrt(double expression)`

Returns the square root of an `expression`.

`abs(double expression)`

Returns the absolute value of a given `expression`.

`exp(double expression)`

Returns the constant e raised to a given power.

`log(double expression1, double expression2)`

Returns the log of `expression1` to the base of `expression2`

`precision(double expression1, int expression2)`

Returns the value of `expression1` to a given precision. For example, `precision(12.3456789, 4)` will return `12.3456`.


## Donations

[![Donate with Trans Pay](https://support.jordanwages.com/static/donate-with-transpay-en.png)](https://support.jordanwages.com?project=2)

If you would like to donate to the development of **cs-mic**, please direct you donation to my [patron page](https://support.jordanwages.com) (powered by Stripe) or directly via my [PayPal.Me](https://www.paypal.me/wagesj45) page. You can also donate Ethereum [to my wallet](https://etherscan.io/address/0x917f3d67e2a7ec8884d241118ee829af57cc4afd).
