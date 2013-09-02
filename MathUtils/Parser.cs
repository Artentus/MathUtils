using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Artentus
{
    namespace Utils
    {
        namespace Math
        {
            /// <summary>
            /// Kann Terme berechnen.
            /// </summary>
            public static class Parser
            {
                static Regex parserRegEx;
                static string[] possibleTokens;
                static char[] operators;
                static string[] functions;

                static Parser()
                {
                    parserRegEx = new Regex(@"(?<number>[0-9]+(\" + CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator + @"[0-9]+){0,1}(e[+\-]{0,1}[0-9]+){0,1})", RegexOptions.Compiled);
                    possibleTokens = new string[] { "+", "-", "*", "/", "^", "%", "sqrt", "root", "sin", "cos", "tan", "asin", "acos", "atan", "sinh", "cosh", "tanh", "ln", "log", "abs", "int", "(", ")", ";", "pi", "e" };
                    operators = new char[] { '+', '-', '*', '/', '^', '%', '!' };
                    functions = new string[] { "sqrt", "root", "sin", "cos", "tan", "asin", "acos", "atan", "sinh", "cosh", "tanh", "ln", "log", "abs", "int" };
                }

                //stellt ein Token dar
                internal abstract class Token
                {
                    internal abstract string GetName();
                    internal abstract void Eval(Stack<double> s);
                    internal abstract void ParseFromString(string s);
                }

                //ein Token, das eine Zahl darstellt
                internal class NumberToken : Token
                {
                    double val;

                    internal override void Eval(Stack<double> s)
                    {
                        s.Push(val);
                    }

                    internal override void ParseFromString(string s)
                    {
                        val = double.Parse(s);
                    }

                    internal override string GetName()
                    {
                        return val.ToString();
                    }
                }

                //ein Token, das einen Operator darstellt
                internal class OperatorToken : Token
                {
                    static Dictionary<char, int> priority;
                    static Dictionary<char, bool> leftAssociative;
                    static Dictionary<char, Action<Stack<double>>> delegates;

                    static OperatorToken()
                    {
                        priority = new Dictionary<char, int>();
                        priority.Add('+', 1);
                        priority.Add('-', 1);
                        priority.Add('*', 2);
                        priority.Add('/', 2);
                        priority.Add('%', 3);
                        priority.Add('!', 4);
                        priority.Add('^', 5);
                        leftAssociative = new Dictionary<char, bool>();
                        leftAssociative.Add('+', true);
                        leftAssociative.Add('-', true);
                        leftAssociative.Add('*', true);
                        leftAssociative.Add('/', true);
                        leftAssociative.Add('%', true);
                        leftAssociative.Add('!', true);
                        leftAssociative.Add('^', false);
                        delegates = new Dictionary<char, Action<Stack<double>>>();
                        delegates.Add('+', new Action<Stack<double>>(s => s.Push(s.Pop() + s.Pop())));
                        delegates.Add('-', new Action<Stack<double>>(s => s.Push(-s.Pop() + s.Pop())));
                        delegates.Add('!', new Action<Stack<double>>(s => s.Push(-s.Pop())));
                        delegates.Add('*', new Action<Stack<double>>(s => s.Push(s.Pop() * s.Pop())));
                        delegates.Add('/', new Action<Stack<double>>(s => s.Push((1 / s.Pop()) * s.Pop())));
                        delegates.Add('^', new Action<Stack<double>>(s => { var temp = s.Pop(); s.Push(System.Math.Pow(s.Pop(), temp)); }));
                        delegates.Add('%', new Action<Stack<double>>(s => { var temp = s.Pop(); s.Push(s.Pop() % temp); }));
                    }

                    char name;

                    internal int Priority { get; private set; }
                    internal bool IsLeftAssociative { get; private set; }

                    internal override void Eval(Stack<double> s)
                    {
                        delegates[name](s);
                    }

                    internal override void ParseFromString(string s)
                    {
                        name = s[0];
                        Priority = priority[name];
                        IsLeftAssociative = leftAssociative[name];
                    }

                    internal override string GetName()
                    {
                        return name.ToString();
                    }
                }

                //ein Token, das eine Funktion darstellt
                internal class FunctionToken : Token
                {
                    static Dictionary<string, Action<Stack<double>>> delegates;

                    static FunctionToken()
                    {
                        delegates = new Dictionary<string, Action<Stack<double>>>();
                        delegates.Add("sqrt", new Action<Stack<double>>(s => s.Push(System.Math.Sqrt(s.Pop()))));
                        delegates.Add("root", new Action<Stack<double>>(s => { var temp = s.Pop(); s.Push(MathHelper.NthRoot(s.Pop(), temp)); }));
                        delegates.Add("sin", new Action<Stack<double>>(s => s.Push(System.Math.Sin(s.Pop()))));
                        delegates.Add("cos", new Action<Stack<double>>(s => s.Push(System.Math.Cos(s.Pop()))));
                        delegates.Add("tan", new Action<Stack<double>>(s => s.Push(System.Math.Tan(s.Pop()))));
                        delegates.Add("asin", new Action<Stack<double>>(s => s.Push(System.Math.Asin(s.Pop()))));
                        delegates.Add("acos", new Action<Stack<double>>(s => s.Push(System.Math.Acos(s.Pop()))));
                        delegates.Add("atan", new Action<Stack<double>>(s => s.Push(System.Math.Atan(s.Pop()))));
                        delegates.Add("sinh", new Action<Stack<double>>(s => s.Push(System.Math.Sinh(s.Pop()))));
                        delegates.Add("cosh", new Action<Stack<double>>(s => s.Push(System.Math.Cosh(s.Pop()))));
                        delegates.Add("tanh", new Action<Stack<double>>(s => s.Push(System.Math.Tanh(s.Pop()))));
                        delegates.Add("ln", new Action<Stack<double>>(s => s.Push(System.Math.Log(s.Pop()))));
                        delegates.Add("log", new Action<Stack<double>>(s => { var temp = s.Pop(); s.Push(System.Math.Log(temp, s.Pop())); }));
                        delegates.Add("abs", new Action<Stack<double>>(s => s.Push(System.Math.Abs(s.Pop()))));
                        delegates.Add("int", new Action<Stack<double>>(s => s.Push((long)s.Pop())));
                    }

                    string name;

                    internal override void Eval(Stack<double> s)
                    {
                        delegates[name](s);
                    }

                    internal override void ParseFromString(string s)
                    {
                        name = s;
                    }

                    internal override string GetName()
                    {
                        return name;
                    }
                }

                //ein Token, das eine besondere Funktion besitzt
                internal class SpecialToken : Token
                {
                    internal string Name { get; private set; }

                    internal override void Eval(Stack<double> s) { }

                    internal override void ParseFromString(string s)
                    {
                        Name = s;
                    }

                    internal override string GetName()
                    {
                        return Name;
                    }
                }

                private static bool StartsWith(this StringBuilder sb, string value)
                {
                    if (sb.Length < value.Length)
                        return false;

                    for (int i = 0; i < value.Length; i++)
                        if (sb[i] != value[i])
                            return false;
                    
                    return true;
                }

                private static bool StartsWith(this StringBuilder sb, char value)
                {
                    if (sb.Length < 1)
                        return false;

                    return value == sb[0];
                }

                private static Token[] GetInfixTokens(string term)
                {
                    //Leerzeichen entfernen und in Kleinbuchstaben konvertieren
                    term = term.Replace(" ", string.Empty).ToLowerInvariant();

                    var tokens = new List<Token>();

                    //mit RegEx alle Zahlen aussortieren
                    var numbers = parserRegEx.Matches(term);
                    term = parserRegEx.Replace(term, "1");

                    var sb = new StringBuilder(term);

                    //Term in Tokens teilen
                    var numberIndex = 0;
                    while (sb.Length > 0)
                    {
                        var validToken = false;

                        //Zahlen prüfen
                        if (sb.StartsWith("1"))
                        {
                            var t = new NumberToken();
                            t.ParseFromString(numbers[numberIndex].Groups["number"].Value);
                            tokens.Add(t);

                            numberIndex++;

                            //term = ReduceString(term, 1);
                            sb.Remove(0, 1);

                            validToken = true;
                        }

                        //Operatoren prüfen
                        if (!validToken)
                            for (int i = 0; i < operators.Length; i++)
                            {
                                var token = operators[i];
                                if (sb.StartsWith(token))
                                {
                                    var t = new OperatorToken();

                                    if ((token == '+' || token == '-') && (tokens.Count == 0 || tokens.Last().GetName() == "(")) //Vorzeichen
                                    {
                                        if (token == '-')
                                            t.ParseFromString("!");
                                    }
                                    else
                                        t.ParseFromString(token.ToString());

                                    tokens.Add(t);

                                    //term = ReduceString(term, 1);
                                    sb.Remove(0, 1);

                                    validToken = true;
                                    break;
                                }
                            }

                        //Funktionen prüfen
                        if (!validToken)
                            for (int i = 0; i < functions.Length; i++)
                            {
                                var token = functions[i];
                                if (sb.StartsWith(token))
                                {
                                    var t = new FunctionToken();
                                    t.ParseFromString(token);
                                    tokens.Add(t);

                                    //term = ReduceString(term, token.Length);
                                    sb.Remove(0, token.Length);

                                    validToken = true;
                                    break;
                                }
                            }

                        //Rest prüfen
                        if (!validToken)
                        {
                            if (sb.StartsWith("pi")) //Pi
                            {
                                var t = new NumberToken();
                                t.ParseFromString(System.Math.PI.ToString());
                                tokens.Add(t);

                                //term = ReduceString(term, 2);
                                sb.Remove(0, 2);
                            }

                            else if (sb.StartsWith("e")) //e
                            {
                                var t = new NumberToken();
                                t.ParseFromString(System.Math.E.ToString());
                                tokens.Add(t);

                                //term = ReduceString(term, 1);
                                sb.Remove(0, 1);
                            }

                            else if (sb.StartsWith("(")) //öffnende Klammer
                            {
                                var t = new SpecialToken();
                                t.ParseFromString("(");
                                tokens.Add(t);

                                //term = ReduceString(term, 1);
                                sb.Remove(0, 1);
                            }

                            else if (sb.StartsWith(")")) //schließende Klammer
                            {
                                var t = new SpecialToken();
                                t.ParseFromString(")");
                                tokens.Add(t);

                                //term = ReduceString(term, 1);
                                sb.Remove(0, 1);
                            }

                            else if (sb.StartsWith(";")) //Argumenttrennzeichen
                            {
                                var t = new SpecialToken();
                                t.ParseFromString(";");
                                tokens.Add(t);

                                //term = ReduceString(term, 1);
                                sb.Remove(0, 1);
                            }

                            else //Token nicht bekannt
                                throw new ArgumentException("Dieser Term enthält einen ungültigen Token.");
                        }  
                    }   

                    return tokens.ToArray();
                }

                private static Token[] GetPostfixTokens(Token[] infixTokens)
                {
                    var opStack = new Stack<Token>();
                    var result = new List<Token>(infixTokens.Length);

                    //alle Tokens abarbeiten
                    for (int i = 0; i < infixTokens.Length; i++ )
                    {
                        var token = infixTokens[i];

                        //bei Zahl
                        if (token is NumberToken)
                            result.Add(token);

                        //bei Funktion
                        else if (token is FunctionToken)
                            opStack.Push(token);

                        //bei Argumenttrennzeichen
                        else if (token.GetName() == ";")
                            while (opStack.Peek().GetName() != "(")
                                result.Add(opStack.Pop());

                        //bei Operator
                        else if (token is OperatorToken)
                        {
                            while (opStack.Count > 0 && (opStack.Peek() is OperatorToken)
                                && (((token as OperatorToken).IsLeftAssociative && (token as OperatorToken).Priority == (opStack.Peek() as OperatorToken).Priority)
                                || (token as OperatorToken).Priority < (opStack.Peek() as OperatorToken).Priority))
                                result.Add(opStack.Pop());

                            opStack.Push(token);
                        }

                        //bei öffnender Klammer
                        else if (token.GetName() == "(")
                            opStack.Push(token);

                        //bei schließender Klammer
                        else if (token.GetName() == ")")
                        {
                            while (opStack.Peek().GetName() != "(")
                                result.Add(opStack.Pop());
                            opStack.Pop();

                            if (opStack.Count > 0 && (opStack.Peek() is FunctionToken))
                                result.Add(opStack.Pop());
                        }
                    }

                    //Rest des Stacks zur Ausgabe schieben
                    while (opStack.Count > 0)
                        result.Add(opStack.Pop());

                    return result.ToArray();
                }

                /// <summary>
                /// Wertet einen Term aus.
                /// </summary>
                /// <param name="term"></param>
                /// <returns></returns>
                public static double Eval(string term)
                {
                    if (string.IsNullOrEmpty(term)) //bei leerem String Fehler vermeiden
                        return 0;

                    var tokens = GetPostfixTokens(GetInfixTokens(term));
                    var result = new Stack<double>();
                
                    //Ausrechnen
                    for (int i = 0; i < tokens.Length; i++)
                        tokens[i].Eval(result);

                    return result.Pop();
                }
            }
        }
    }
}
