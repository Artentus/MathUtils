using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Text.RegularExpressions;

namespace Artentus
{
    namespace Utils
    {
        namespace Math
        {
            /// <summary>
            /// Stellt erweiterte mathematische Funktionen bereit.
            /// </summary>
            public static class MathHelper
            {
                /// <summary>
                /// Konvertiert einen Wert vom Gradmaß ins Bogenmaß.
                /// </summary>
                /// <param name="deg"></param>
                /// <returns></returns>
                public static double GetRad(double deg)
                {
                    return deg * System.Math.PI / 180.0;
                }

                /// <summary>
                /// Konvertiert einen Wert vom Gradmaß ins Bogenmaß.
                /// </summary>
                /// <param name="deg"></param>
                /// <returns></returns>
                public static float GetRad(float deg)
                {
                    return deg * (float)System.Math.PI / 180.0F;
                }

                /// <summary>
                /// Konvertiert einen Wert vom Bogenmaß ins Gradmaß.
                /// </summary>
                /// <param name="rad"></param>
                /// <returns></returns>
                public static double GetDeg(double rad)
                {
                    return rad * 180.0 / System.Math.PI;
                }

                /// <summary>
                /// Konvertiert einen Wert vom Bogenmaß ins Gradmaß.
                /// </summary>
                /// <param name="rad"></param>
                /// <returns></returns>
                public static float GetDeg(float rad)
                {
                    return rad * 180.0f / (float)System.Math.PI;
                }

                /// <summary>
                /// Berechnet die Steigung einer Geraden, die durch zwei Punkte beschrieben wird.
                /// </summary>
                /// <param name="p1"></param>
                /// <param name="p2"></param>
                /// <returns></returns>
                public static double GetGradient(Point2D p1, Point2D p2)
                {
                    return (p1.Y - p2.Y) / (p1.X - p2.X); //Steigungssatz
                }

                /// <summary>
                /// Berechnet die Steigung einer Geraden, die durch zwei Punkte beschrieben wird.
                /// </summary>
                /// <param name="p1"></param>
                /// <param name="p2"></param>
                /// <returns></returns>
                public static float GetGradient(PointF p1, PointF p2)
                {
                    return (p1.Y - p2.Y) / (p1.X - p2.X); //Steigungssatz
                }

                /// <summary>
                /// Berechnet den Arcustangens aus der Steigung zwischen zwei Punkten.
                /// </summary>
                /// <param name="p1"></param>
                /// <param name="p2"></param>
                /// <returns></returns>
                public static double Atan(Point2D p1, Point2D p2)
                {
                    return System.Math.Atan2(p2.Y - p1.Y, p2.X - p1.X);
                }

                /// <summary>
                /// Berechnet den Arcustangens aus der Steigung zwischen zwei Punkten.
                /// </summary>
                /// <param name="p1"></param>
                /// <param name="p2"></param>
                /// <returns></returns>
                public static float Atan(PointF p1, PointF p2)
                {
                    return (float)System.Math.Atan2(p2.Y - p1.Y, p2.X - p1.X);
                }

                /// <summary>
                /// Berechnet die Position eines Punktes, der auf einem Kreis liegt.
                /// </summary>
                /// <param name="center">Der Mittelpunkt des Kreises.</param>
                /// <param name="angle">Der Winkel, in dem der Punkt zur X-Achse liegt.</param>
                /// <param name="radius">Der Radius des Kreises.</param>
                /// <returns></returns>
                public static Point2D GetPointOnCircle(Point2D center, double angle, double radius)
                {
                    //Winkelfunktionen anwenden
                    var sin = System.Math.Sin(angle);
                    var cos = System.Math.Cos(angle);

                    //Koordinaten berechnen
                    var x = center.X + cos * radius;
                    var y = center.Y + sin * radius;

                    return new Point2D(x, y);
                }

                /// <summary>
                /// Berechnet die Position eines Punktes, der auf einem Kreis liegt.
                /// </summary>
                /// <param name="center">Der Mittelpunkt des Kreises.</param>
                /// <param name="angle">Der Winkel, in dem der Punkt zur X-Achse liegt.</param>
                /// <param name="radius">Der Radius des Kreises.</param>
                /// <returns></returns>
                public static PointF GetPointOnCircle(PointF center, float angle, float radius)
                {
                    return GetPointOnCircle(new Point2D(center.X, center.Y), (double)angle, (double)radius).ToPointF();
                }

                /// <summary>
                /// Schränkt eine Zahl auf einen bestimmten Wertebreich ein.
                /// </summary>
                /// <param name="value"></param>
                /// <param name="minValue"></param>
                /// <param name="maxValue"></param>
                /// <returns></returns>
                public static double Clamp(double value, double minValue, double maxValue)
                {
                    if (value < minValue)
                        return minValue;
                    if (value > maxValue)
                        return maxValue;
                    return value;
                }

                /// <summary>
                /// Schränkt eine Zahl auf einen bestimmten Wertebreich ein.
                /// </summary>
                /// <param name="value"></param>
                /// <param name="minValue"></param>
                /// <param name="maxValue"></param>
                /// <returns></returns>
                public static float Clamp(float value, float minValue, float maxValue)
                {
                    if (value < minValue)
                        return minValue;
                    if (value > maxValue)
                        return maxValue;
                    return value;
                }

                /// <summary>
                /// Schränkt eine Zahl auf einen bestimmten Wertebreich ein.
                /// </summary>
                /// <param name="value"></param>
                /// <param name="minValue"></param>
                /// <param name="maxValue"></param>
                /// <returns></returns>
                public static decimal Clamp(decimal value, decimal minValue, decimal maxValue)
                {
                    if (value < minValue)
                        return minValue;
                    if (value > maxValue)
                        return maxValue;
                    return value;
                }

                /// <summary>
                /// Schränkt eine Zahl auf einen bestimmten Wertebreich ein.
                /// </summary>
                /// <param name="value"></param>
                /// <param name="minValue"></param>
                /// <param name="maxValue"></param>
                /// <returns></returns>
                public static long Clamp(long value, long minValue, long maxValue)
                {
                    if (value < minValue)
                        return minValue;
                    if (value > maxValue)
                        return maxValue;
                    return value;
                }

                /// <summary>
                /// Schränkt eine Zahl auf einen bestimmten Wertebreich ein.
                /// </summary>
                /// <param name="value"></param>
                /// <param name="minValue"></param>
                /// <param name="maxValue"></param>
                /// <returns></returns>
                public static ulong Clamp(ulong value, ulong minValue, ulong maxValue)
                {
                    if (value < minValue)
                        return minValue;
                    if (value > maxValue)
                        return maxValue;
                    return value;
                }

                /// <summary>
                /// Schränkt eine Zahl auf einen bestimmten Wertebreich ein.
                /// </summary>
                /// <param name="value"></param>
                /// <param name="minValue"></param>
                /// <param name="maxValue"></param>
                /// <returns></returns>
                public static int Clamp(int value, int minValue, int maxValue)
                {
                    if (value < minValue)
                        return minValue;
                    if (value > maxValue)
                        return maxValue;
                    return value;
                }

                /// <summary>
                /// Schränkt eine Zahl auf einen bestimmten Wertebreich ein.
                /// </summary>
                /// <param name="value"></param>
                /// <param name="minValue"></param>
                /// <param name="maxValue"></param>
                /// <returns></returns>
                public static uint Clamp(uint value, uint minValue, uint maxValue)
                {
                    if (value < minValue)
                        return minValue;
                    if (value > maxValue)
                        return maxValue;
                    return value;
                }

                /// <summary>
                /// Schränkt eine Zahl auf einen bestimmten Wertebreich ein.
                /// </summary>
                /// <param name="value"></param>
                /// <param name="minValue"></param>
                /// <param name="maxValue"></param>
                /// <returns></returns>
                public static short Clamp(short value, short minValue, short maxValue)
                {
                    if (value < minValue)
                        return minValue;
                    if (value > maxValue)
                        return maxValue;
                    return value;
                }

                /// <summary>
                /// Schränkt eine Zahl auf einen bestimmten Wertebreich ein.
                /// </summary>
                /// <param name="value"></param>
                /// <param name="minValue"></param>
                /// <param name="maxValue"></param>
                /// <returns></returns>
                public static ushort Clamp(ushort value, ushort minValue, ushort maxValue)
                {
                    if (value < minValue)
                        return minValue;
                    if (value > maxValue)
                        return maxValue;
                    return value;
                }

                /// <summary>
                /// Schränkt eine Zahl auf einen bestimmten Wertebreich ein.
                /// </summary>
                /// <param name="value"></param>
                /// <param name="minValue"></param>
                /// <param name="maxValue"></param>
                /// <returns></returns>
                public static byte Clamp(byte value, byte minValue, byte maxValue)
                {
                    if (value < minValue)
                        return minValue;
                    if (value > maxValue)
                        return maxValue;
                    return value;
                }

                /// <summary>
                /// Schränkt eine Zahl auf einen bestimmten Wertebreich ein.
                /// </summary>
                /// <param name="value"></param>
                /// <param name="minValue"></param>
                /// <param name="maxValue"></param>
                /// <returns></returns>
                public static sbyte Clamp(sbyte value, sbyte minValue, sbyte maxValue)
                {
                    if (value < minValue)
                        return minValue;
                    if (value > maxValue)
                        return maxValue;
                    return value;
                }

                /// <summary>
                /// Ermittelt den größten gemeinsamen Teiler zweier Ganzzahlen.
                /// </summary>
                /// <param name="left"></param>
                /// <param name="right"></param>
                /// <returns></returns>
                public static long GetGreatestCommonDivisor(long left, long right)
                {
                    var value = left % right;

                    while (value != 0)
                    {
                        left = right;
                        right = value;
                        value = left % right;
                    }

                    return right;
                }

                /// <summary>
                /// Ermittelt den größten gemeinsamen Teiler zweier Ganzzahlen.
                /// </summary>
                /// <param name="left"></param>
                /// <param name="right"></param>
                /// <returns></returns>
                public static int GetGreatestCommonDivisor(int left, int right)
                {
                    var value = left % right;

                    while (value != 0)
                    {
                        left = right;
                        right = value;
                        value = left % right;
                    }

                    return right;
                }

                /// <summary>
                /// Prüft, ob eine Gleitkommazahl einen ganzzahligen Wert besitzt.
                /// </summary>
                /// <param name="value"></param>
                /// <returns></returns>
                public static bool IsInt(double value)
                {
                    return value == (long)value;
                }

                /// <summary>
                /// Prüft, ob eine Gleitkommazahl einen ganzzahligen Wert besitzt.
                /// </summary>
                /// <param name="value"></param>
                /// <returns></returns>
                public static bool IsInt(float value)
                {
                    return value == (long)value;
                }

                /// <summary>
                /// Prüft, ob eine Gleitkommazahl einen ganzzahligen Wert besitzt.
                /// </summary>
                /// <param name="value"></param>
                /// <returns></returns>
                public static bool IsInt(decimal value)
                {
                    return value == (long)value;
                }

                private static List<string> GetInfixTokens(string term)
                {
                    term = term.Replace(" ", string.Empty).ToLowerInvariant(); //Leerzeichen entfernen und in Kleinbuchstaben konvertieren

                    var tokens = new List<string>();

                    //mit RegEx alle Zahlen aussortieren
                    var r = new Regex(@"(((?<=(\(|^))(?<sign>[+\-]{0,1}))|(?<=.))(?<number>([0-9]+)(\.[0-9]+){0,1})");
                    var numbers = r.Matches(term);
                    term = r.Replace(term, "1");

                    //Term in Tokens teilen
                    var possibleTokens = new string[] { "+", "-", "*", "/", "^", "sqrt", "sin", "cos", "tan", "asin", "acos", "atan", "ln", "log", "(", ")" };
                    var numberIndex = 0;
                    while (term.Length > 0)
                    {
                        //Operatoren, Klammern und Funktionen prüfen
                        foreach (var token in possibleTokens)
                            if (term.StartsWith(token))
                            {
                                tokens.Add(token);

                                if (term.Length > token.Length)
                                    term = term.Substring(token.Length);
                                else
                                    term = string.Empty;
                            }

                        //Zahlen prüfen
                        if (term.StartsWith("1"))
                        {
                            if (numbers[numberIndex].Groups["sign"].Value == "-")
                                tokens.Add("!");
                            tokens.Add(numbers[numberIndex].Groups["number"].Value);

                            numberIndex++;

                            if (term.Length > 1)
                                term = term.Substring(1);
                            else
                                term = string.Empty;
                        }
                    }

                    return tokens;
                }

                private static List<string> GetPostfixTokens(List<string> infixTokens)
                {
                    //Operatoren und Prioritäten definieren
                    var operators = new string[] { "+", "-", "*", "/", "^", "sqrt", "sin", "cos", "tan", "asin", "acos", "atan", "ln", "log" };
                    var priority = new int[] { 1, 1, 2, 2, 3, 4, 4, 4, 4, 4, 4, 4, 4, 4 };
                    var leftAssociative = new bool[] {true, true, true, true, false, true, true, true, true, true, true};

                    var opStack = new Stack<string>();
                    var result = new List<string>();

                    //alle Tokens abarbeiten
                    foreach (var token in infixTokens)
                    {
                        //bei Zahl
                        var val = 0.0;
                        if (double.TryParse(token, out val))
                        {
                            result.Add(token);
                        }

                        //bei öffnender Klammer
                        if (token == "(")
                            opStack.Push(token);

                        //bei schließender Klammer
                        if (token == ")")
                        {
                            while (opStack.Peek() != "(")
                                result.Add(opStack.Pop());

                            opStack.Pop();
                        }

                        //bei Operator
                        if (operators.Contains(token))
                        {
                            var tokenPriority = priority[Array.IndexOf(operators, token)];
                            var tokenAssocitivity = leftAssociative[Array.IndexOf(operators, token)];
                            while (opStack.Count > 0)
                            {
                                var stackPriority = 0;
                                if (opStack.Peek() == "(" || opStack.Peek() == ")")
                                    stackPriority = 0;
                                else
                                    stackPriority = priority[Array.IndexOf(operators, opStack.Peek())];

                                if ((tokenPriority < stackPriority) || ((tokenPriority == stackPriority) && tokenAssocitivity))
                                    result.Add(opStack.Pop());
                                else
                                    break;
                            }

                            opStack.Push(token);
                        }
                    }

                    //Rest des Stacks zur Augabe schieben
                    for (int i = 0; i < opStack.Count; i++)
                        result.Add(opStack.Pop());

                    return result;
                }

                /// <summary>
                /// Wertet einen Term aus.
                /// </summary>
                /// <param name="term"></param>
                /// <returns></returns>
                public static double Eval(string term)
                {
                    var tokens = GetPostfixTokens(GetInfixTokens(term));
                    var result = new Stack<double>();

                    //Operatoren implementieren
                    var operators = new Dictionary<string, Func<Stack<double>, double>>();
                    operators.Add("+", new Func<Stack<double>, double>(s => s.Pop() + s.Pop()));
                    operators.Add("-", new Func<Stack<double>, double>(s => -s.Pop() + s.Pop()));
                    operators.Add("!", new Func<Stack<double>, double>(s => -s.Pop()));
                    operators.Add("*", new Func<Stack<double>, double>(s => s.Pop() * s.Pop()));
                    operators.Add("/", new Func<Stack<double>, double>(s => (1 / s.Pop()) * s.Pop()));
                    operators.Add("^", new Func<Stack<double>, double>(s => { var temp = s.Pop(); return System.Math.Pow(s.Pop(), temp); }));
                    operators.Add("sqrt", new Func<Stack<double>, double>(s => System.Math.Sqrt(s.Pop())));
                    operators.Add("sin", new Func<Stack<double>, double>(s => System.Math.Sin(s.Pop())));
                    operators.Add("cos", new Func<Stack<double>, double>(s => System.Math.Cos(s.Pop())));
                    operators.Add("tan", new Func<Stack<double>, double>(s => System.Math.Tan(s.Pop())));
                    operators.Add("asin", new Func<Stack<double>, double>(s => System.Math.Asin(s.Pop())));
                    operators.Add("acos", new Func<Stack<double>, double>(s => System.Math.Acos(s.Pop())));
                    operators.Add("atan", new Func<Stack<double>, double>(s => System.Math.Atan(s.Pop())));
                    operators.Add("ln", new Func<Stack<double>, double>(s => System.Math.Log(s.Pop())));
                    operators.Add("log", new Func<Stack<double>, double>(s => System.Math.Log10(s.Pop())));

                    //Ausrechnen
                    foreach (var token in tokens)
                        if (operators.ContainsKey(token))
                            result.Push(operators[token](result));
                        else
                            result.Push(double.Parse(token));

                    return result.Pop();
                }
            }
        }
    }
}
