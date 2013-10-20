using csmic;
using System.Text;
using System.Collections.Generic;



using System;
using System.CodeDom.Compiler;

namespace csmic.Interpreter {



[GeneratedCodeAttribute("Coco/R", "")]
public class Parser {
	public const int _EOF = 0;
	public const int _identifier = 1;
	public const int _sign = 2;
	public const int _binary = 3;
	public const int _hex = 4;
	public const int _number = 5;
	public const int _string = 6;
	public const int _LPAREN = 7;
	public const int _RPAREN = 8;
	public const int _COMPARER = 9;
	public const int maxT = 22;

	const bool T = true;
	const bool x = false;
	const int minErrDist = 2;
	
	public Scanner scanner;
	public Errors  errors;

	public Token t;    // last recognized token
	public Token la;   // lookahead token
	int errDist = minErrDist;

private decimal calcValue = 0;
private string stringValue = string.Empty;

public decimal CalculatedValue
{
	get
	{
		return this.calcValue;
	}
	set
	{
		this.calcValue = value;
	}
}

private InputInterpreter interpreter = null;

public InputInterpreter Interpreter
{
	get
	{
		return this.interpreter;
	}
	set
	{
		this.interpreter = value;
	}
}

bool IsFunctionCall()  
{
  scanner.ResetPeek();
  Token next = scanner.Peek();
  if (next.kind == _LPAREN && la.kind == _identifier)
    return true;
  return false;
}

bool IsCompare()  
{
  scanner.ResetPeek();
  Token next = scanner.Peek();
  if (next.kind == _COMPARER)
    return true;
  return false;
}

bool IsAssignment()  
{
  scanner.ResetPeek();
  Token next = scanner.Peek();
  if (next.val == "::" || next.val == ":=" || next.val == "->")
    return true;
  return false;
}

bool IsArrayCall()
{
	scanner.ResetPeek();
	Token next = scanner.Peek();
	if(next.val == "[")
		return true;
	return false;
}
	


	public Parser(Scanner scanner) {
		this.scanner = scanner;
		errors = new Errors();
	}

	void SynErr (int n) {
		if (errDist >= minErrDist) errors.SynErr(la.line, la.col, n);
		errDist = 0;
	}

	public void SemErr (string msg) {
		if (errDist >= minErrDist) errors.SemErr(t.line, t.col, msg);
		errDist = 0;
	}
	
	void Get () {
		for (;;) {
			t = la;
			la = scanner.Scan();
			if (la.kind <= maxT) { ++errDist; break; }

			la = t;
		}
	}
	
	void Expect (int n) {
		if (la.kind==n) Get(); else { SynErr(n); }
	}
	
	bool StartOf (int s) {
		return set[s, la.kind];
	}
	
	void ExpectWeak (int n, int follow) {
		if (la.kind == n) Get();
		else {
			SynErr(n);
			while (!StartOf(follow)) Get();
		}
	}


	bool WeakSeparator(int n, int syFol, int repFol) {
		int kind = la.kind;
		if (kind == n) {Get(); return true;}
		else if (StartOf(repFol)) {return false;}
		else {
			SynErr(n);
			while (!(set[syFol, kind] || set[repFol, kind] || set[0, kind])) {
				Get();
				kind = la.kind;
			}
			return StartOf(syFol);
		}
	}

	
	void CSMIC() {
		decimal r = 0;
		string s = string.Empty;
		decimal[] a = new decimal[0];
		bool success = true;
		if(this.interpreter == null)
		{
		return;
		}
		
		if (IsCompare()) {
			Comparison(out success);
			this.calcValue = (success == true) ? 1 : 0;  this.interpreter.ProduceOutput(success); 
		} else if (IsAssignment()) {
			Assignment(out r);
			this.calcValue = r; 
		} else if (StartOf(1)) {
			Expression(out r);
			this.calcValue = r; this.interpreter.ProduceOutput(r); 
		} else if (la.kind == 6) {
			String(out s);
			this.stringValue = s; this.interpreter.ProduceOutput(s); 
		} else SynErr(23);
	}

	void Comparison(out bool result) {
		decimal firstValue = 0;
		decimal secondValue = 0;
		string compareType = string.Empty;
		
		Expression(out firstValue);
		Expect(9);
		compareType = t.val; 
		Expression(out secondValue);
		switch(compareType)
		{
		case "==":
			result = (firstValue == secondValue);
			break;
		case ">":
			result = (firstValue > secondValue);
			break;
		case "<":
			result = (firstValue < secondValue);
			break;
		case ">=":
			result = (firstValue >= secondValue);
			break;
		case "<=":
			result = (firstValue <= secondValue);
			break;
		default:
			result = false;
			break;
		}
		
	}

	void Assignment(out decimal r) {
		string identifier = string.Empty;
		string expression = string.Empty;
		decimal[] d = new decimal[0];
		r = 0;
		
		Expect(1);
		identifier = t.val; 
		if (la.kind == 19) {
			Get();
			Expression(out r);
			this.interpreter.Assign(identifier, r); 
			this.interpreter.ProduceOutput(r); 
		} else if (la.kind == 20) {
			Get();
			AnyExpression(out expression);
			this.interpreter.Assign(identifier, expression); 
			this.interpreter.ProduceOutput(expression); 
		} else if (la.kind == 21) {
			Get();
			ArrayL(out d);
			this.interpreter.Assign(identifier, d); r = 0; 
			StringBuilder builder = new StringBuilder();
			foreach(decimal dec in d)
			{
			builder.Append("," + dec.ToString());
			}
			builder.Remove(0,1);
			this.interpreter.ProduceOutput(builder.ToString());
			
		} else SynErr(24);
	}

	void Expression(out decimal r) {
		decimal r1; 
		Term(out r);
		while (la.kind == 10 || la.kind == 11) {
			if (la.kind == 10) {
				Get();
				Term(out r1);
				r += r1; 
			} else {
				Get();
				Term(out r1);
				r -= r1; 
			}
		}
	}

	void String(out string s) {
		Expect(6);
		s = t.val; 
	}

	void Term(out decimal r) {
		decimal r1; 
		Factor(out r);
		while (la.kind == 12 || la.kind == 13 || la.kind == 14) {
			if (la.kind == 12) {
				Get();
				Factor(out r1);
				r *= r1; 
			} else if (la.kind == 13) {
				Get();
				Factor(out r1);
				r /= r1; 
			} else {
				Get();
				Term(out r1);
				r %= r1; 
			}
		}
	}

	void Factor(out decimal r) {
		decimal r1; 
		Value(out r);
		while (la.kind == 15) {
			Get();
			Expression(out r1);
			r = Convert.ToDecimal(Math.Pow(Convert.ToDouble(r), Convert.ToDouble(r1))); 
		}
	}

	void Value(out decimal r) {
		r = 0;
		decimal r1 = 0;
		string fn; 
		int sign = 1;
		if (la.kind == 10 || la.kind == 11) {
			if (la.kind == 10) {
				Get();
			} else {
				Get();
				sign = -1; 
			}
		}
		if (IsFunctionCall()) {
			Function(out r);
			r = sign * r; 
		} else if (IsArrayCall()) {
			ArrayCall(out r);
			r = sign * r; 
		} else if (la.kind == 1) {
			Get();
			if(this.interpreter.variables.ContainsKey(t.val))
			{
			Variable v = this.interpreter.variables[t.val];
			if(v.Type == VariableType.Equation)
			{
			InputInterpreter i = new InputInterpreter(this.interpreter);
			i.Interpret(v.Value.ToString());
			r = i.Decimal;
			}
			else if(v.Type == VariableType.Decimal)
			{
			r = Convert.ToDecimal(v.Value);
			}
			else
			{
			r = 0;
			}
			}
			
		} else if (la.kind == 5) {
			Get();
			r = sign * Convert.ToDecimal (t.val); 
		} else if (la.kind == 4) {
			Get();
			string expression = t.val.Remove(0,2);
			try
			{
			decimal value = Convert.ToDecimal(Convert.ToInt64(expression, 16));
			r = sign * value;
			}
			catch
			{
			r = 0;
			}
			
		} else if (la.kind == 3) {
			Get();
			string expression = t.val.Remove(t.val.Length - 1);
			try
			{
			decimal value = Convert.ToDecimal(Convert.ToInt64(expression, 2));
			r = sign * value;
			}
			catch
			{
			r = 0;
			}
			
		} else if (la.kind == 7) {
			Get();
			Expression(out r);
			Expect(8);
			r = sign * r; 
		} else SynErr(25);
	}

	void Function(out decimal r) {
		string functionName = string.Empty;
		decimal[] d = new decimal[0];
		
		Expect(1);
		functionName = t.val; 
		Expect(7);
		CommaList(out d);
		Expect(8);
		r = this.interpreter.ExecuteFunction(functionName, d); 
	}

	void ArrayCall(out decimal r) {
		string ident = string.Empty; r = 0; decimal pos = 0; 
		Expect(1);
		ident = t.val; 
		Expect(16);
		Expression(out pos);
		int i = 0;
		try
		{
		i = Convert.ToInt32(pos);
		if(this.interpreter.variables.ContainsKey(ident))
		{
		decimal[] values = this.interpreter.variables[ident].Value as decimal[];
		if(values != null)
		{
		r = values[i];
		}
		}
		}
		catch
		{
		}
		
		Expect(17);
	}

	void ArrayL(out decimal[] d) {
		Expect(16);
		CommaList(out d);
		Expect(17);
	}

	void CommaList(out decimal[] d) {
		List<decimal> list = new List<decimal>(); decimal r = 0; 
		Expression(out r);
		list.Add(r); d = list.ToArray(); 
		while (la.kind == 18) {
			Get();
			Expression(out r);
			list.Add(r); d = list.ToArray(); 
		}
	}

	void AnyExpression(out string value) {
		value = string.Empty; StringBuilder builder = new StringBuilder(); 
		Get();
		builder.Append(t.val); 
		while (StartOf(2)) {
			Get();
			builder.Append(t.val); 
		}
		value = builder.ToString(); 
	}



	public void Parse() {
		la = new Token();
		la.val = "";		
		Get();
		CSMIC();
		Expect(0);

    Expect(0);
	}
	
	static readonly bool[,] set = {
		{T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x},
		{x,T,x,T, T,T,x,T, x,x,T,T, x,x,x,x, x,x,x,x, x,x,x,x},
		{x,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,x}

	};
} // end Parser


public class Errors {
	public int count = 0;                                    // number of errors detected
	public StringBuilder builder = new StringBuilder();   // error messages go to this stream
    public string errMsgFormat = "-- position {0}: {1}"; // 0=line, 1=column, 2=text
  
	public void SynErr (int line, int col, int n) {
		string s;
		switch (n) {
			case 0: s = "EOF expected"; break;
			case 1: s = "identifier expected"; break;
			case 2: s = "sign expected"; break;
			case 3: s = "binary expected"; break;
			case 4: s = "hex expected"; break;
			case 5: s = "number expected"; break;
			case 6: s = "string expected"; break;
			case 7: s = "LPAREN expected"; break;
			case 8: s = "RPAREN expected"; break;
			case 9: s = "COMPARER expected"; break;
			case 10: s = "\"+\" expected"; break;
			case 11: s = "\"-\" expected"; break;
			case 12: s = "\"*\" expected"; break;
			case 13: s = "\"/\" expected"; break;
			case 14: s = "\"%\" expected"; break;
			case 15: s = "\"^\" expected"; break;
			case 16: s = "\"[\" expected"; break;
			case 17: s = "\"]\" expected"; break;
			case 18: s = "\",\" expected"; break;
			case 19: s = "\"::\" expected"; break;
			case 20: s = "\":=\" expected"; break;
			case 21: s = "\"->\" expected"; break;
			case 22: s = "??? expected"; break;
			case 23: s = "invalid CSMIC"; break;
			case 24: s = "invalid Assignment"; break;
			case 25: s = "invalid Value"; break;

			default: s = "error " + n; break;
		}
		builder.AppendFormat(errMsgFormat, col, s);
		count++;
	}

	public void SemErr (int line, int col, string s) {
		builder.AppendFormat(errMsgFormat, col, s);
		count++;
	}
	
	public void SemErr (string s) {
		builder.AppendLine(s);
		count++;
	}
	
	public void Warning (int line, int col, string s) {
		builder.AppendFormat(errMsgFormat, col, s);
	}
	
	public void Warning(string s) {
		builder.AppendLine(s);
	}
} // Errors


public class FatalError: Exception {
	public FatalError(string m): base(m) {}
}

}