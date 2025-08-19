Design Philosophy

CS‑MIC is a small, embeddable expression interpreter designed primarily for UI scenarios where a user types flexible input (e.g., 2+2+someVar) but the host application needs a validated, deterministic numeric value. The library focuses on predictable evaluation, strong validation and extensibility through developer‑supplied variables and coded functions.

Goals

- Numeric‑first: Expressions evaluate to a numeric result the host can trust.
- Predictable semantics: No implicit coercions or surprising operator behavior.
- Embeddable: Tiny surface area, easy to host inside C# applications.
- Extensible: Developers inject variables and ICodedFunction implementations.
- Clear errors: Friendly, actionable diagnostics for invalid input.

Core Principles

- Determinism: The same input with the same variables/functions yields the same numeric output.
- Minimalism: Keep the grammar and runtime small; add features only when they reinforce the numeric‑first mission.
- Type clarity: Values carry explicit types; operators enforce type rules rather than auto‑converting.
- Safe composition: Functions are pure from the interpreter’s perspective; side effects are the host’s responsibility.

Strings: Arguments‑Only, Numeric‑First

CS‑MIC’s v2 scope treats strings as helpers for functions, not as first‑class expression values. This preserves the “numeric guarantee” while enabling rich, domain‑specific function usage.

- Where strings are allowed: As literals in function argument lists (e.g., myFunc("key", 42)).
- Where strings are not allowed: As standalone primaries, in arithmetic (e.g., "a" + 1), or in comparisons; string variables are out of scope for v2.
- Function contract: ICodedFunction implementations may accept string arguments and should return a numeric result when the function’s value is used in an expression.
- Grammar posture: The grammar recognizes quoted string tokens; the parser accepts them only in function argument positions.
- Operator semantics: No string operators or concatenation; no implicit conversions from string to number.
- Errors: If a string is used outside an argument position or produced where a number is required, the interpreter emits a clear type error (e.g., "strings are only valid as function arguments").

Rationale

- Preserves the primary mission: turn flexible user input into a validated numeric value.
- Keeps complexity low by avoiding general string semantics (concatenation, ordering, variables, etc.).
- Maximizes developer power: functions can receive text payloads (formats, keys, expressions) and return numbers, leveraging CS‑MIC for parsing, validation and invocation.

Developer Guidance

- Implementing ICodedFunction: Inspect FunctionArgument.Value.Type to branch on expected input. If a string is required, validate and produce a numeric FunctionValue.
- Argument metadata: Optionally use ExpectedArguments to document names and intended types for better diagnostics.
- Error messaging: Prefer precise, actionable messages (e.g., "arg 'pattern' must be a string").

Example

// Pseudocode / sketch
// myFunc("HEX", 0xFF) → 255
// myFunc("BIN", 1010b) → 10
// sumWithLabel("groupA", 1, 2, 3) → 6 (label used for logging/selection)

Future Directions (Non‑Goals for v2)

- First‑class strings: Allow strings as values, variables, or return types in general expressions (would require defining operators and comparisons).
- Verbatim/opaque arguments: Special argument modes for embedding mini‑DSLs (higher parser complexity).
- Value tagging: Optional metadata (e.g., OriginalLiteral) attached to FunctionValue for advanced scenarios.
