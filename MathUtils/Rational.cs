using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Artentus
{
    namespace Utils
    {
        namespace Math
        {
            /// <summary>
            /// Stellt eine rationale Zahl von beliebiger Genauigkeit in Form eines Bruches dar.
            /// </summary>
            /// <remarks>
            /// Es werden implizite Konvertierungen aus <see cref="System.Decimal"/>, <see cref="System.Double"/>, <see cref="System.Single"/>, <see cref="System.Int64"/>, <see cref="System.UInt64"/>, <see cref="System.Int32"/>, <see cref="System.UInt32"/>, <see cref="System.Int16"/>, <see cref="System.UInt16"/>, <see cref="System.Byte"/>, <see cref="System.SByte"/> und <see cref="System.Numerics.BigInteger"/> sowie explizite Konvertierungen in diese Typen unterstützt.
            /// </remarks>
            [Serializable]
            public struct Rational : IComparable, IComparable<Rational>, IEquatable<Rational>, ISerializable
            {
                const string errorNaN = "NaN ist kein zulässiger Wert bei einer Rechenoperation.";

                BigInteger numerator;
                BigInteger denominator;

                /// <summary>
                /// Der Zähler des Bruches.
                /// </summary>
                public BigInteger Numerator
                {
                    get
                    {
                        return numerator;
                    }
                    private set
                    {
                        numerator = value;
                    }
                }
                /// <summary>
                /// Der Nenner des Bruches.
                /// </summary>
                public BigInteger Denominator
                {
                    get
                    {
                        return denominator + 1;
                    }
                    private set
                    {
                        denominator = value - 1;
                    }
                }
                /// <summary>
                /// Das Vorzeichen (1, 0 oder -1) dieses Rationals.
                /// </summary>
                public int Sign
                {
                    get
                    {
                        return Numerator.Sign;
                    }
                }
                /// <summary>
                /// Bestimmt, ob diese Rational-Instanz den ungültigen Zustand (not a number) darstellt.
                /// </summary>
                public bool IsNaN
                {
                    get
                    {
                        return Denominator == 0;
                    }
                }

                /// <summary>
                /// Stellt den ungültigen Zustand (not a number) dar.
                /// </summary>
                public static Rational NaN
                {
                    get
                    {
                        return new Rational(0, 0);
                    }
                }
                /// <summary>
                /// Stellt die Zahl 0 dar.
                /// </summary>
                public static Rational Zero
                {
                    get
                    {
                        return new Rational(0, 1);
                    }
                }
                /// <summary>
                /// Stellt die Zahl 1 dar.
                /// </summary>
                public static Rational One
                {
                    get
                    {
                        return new Rational(1, 1);
                    }
                }
                /// <summary>
                /// Stellt die Zahl -1 dar.
                /// </summary>
                public static Rational MinusOne
                {
                    get
                    {
                        return new Rational(-1, 1);
                    }
                }

                /// <summary>
                /// Versucht, den angegebenen String unter Verwendung des angegebenen <paramref name="style"/> und <paramref name="provider"/> in einen Rational zu konvertieren.
                /// </summary>
                /// <param name="s">Der zu konvertierende <see cref="System.String"/>.</param>
                /// <param name="style">Der für die Konvertierung verwendete <see cref="System.Globalization.NumberStyles"/>.</param>
                /// <param name="provider">Der für die Konvertierung verwendete <see cref="System.IFormatProvider"/>.</param>
                /// <param name="result">Das Ergebnis der Konvertierung. Wenn die Konvertierung fehlgeschlagen ist, ist dieser Parameter 0.</param>
                /// <returns>
                /// Gibt True zurück, wenn die Konvertierung erfolgreich war, andernfalls False.
                /// </returns>
                /// <remarks>
                /// Die Funktion kann Strings in der Exponentialschreibweise (0.0E0) und der Bruchschreibweise (0/0) verarbeiten.
                /// Es sind die NumberStyles AllowLeadingWhite, AllowTrailingWhite, AllowLeadingSign, AllowDecimalPoint, AllowThousands und AllowExponent erlaubt.
                /// </remarks>
                /// <seealso cref="System.Globalization.NumberStyles"/>
                /// <seealso cref="System.IFormatProvider"/>
                public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out Rational result)
                {
                    result = default(Rational);
                    if (string.IsNullOrEmpty(s)) return false;
                    string[] parts = s.Split('/');
                    NumberStyles intStyle = (style & (NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowThousands));
                    if (parts.Length == 2)
                    {
                        BigInteger num;
                        BigInteger denom;
                        if (!(BigInteger.TryParse(parts[0], intStyle, provider, out num) && BigInteger.TryParse(parts[1], intStyle, provider, out denom))) return false;
                        result = new Rational(num, denom);
                        return true;
                    }
                    else if (parts.Length == 1)
                    {
                        BigInteger num = 0;
                        BigInteger denom = 0;
                        BigInteger decimalPlacesToShift = 0;
                        int exponentIndex = s.IndexOf("e", StringComparison.OrdinalIgnoreCase);
                        if (exponentIndex > -1)
                        {
                            if ((style & NumberStyles.AllowExponent) == NumberStyles.None) return false;
                            BigInteger exponent;
                            if (!BigInteger.TryParse(s.Substring(exponentIndex + 1), intStyle, provider, out exponent)) return false;
                            decimalPlacesToShift = exponent;
                            s = s.Substring(0, exponentIndex);
                        }
                        NumberFormatInfo info = NumberFormatInfo.GetInstance(provider);
                        int decimalSeparatorIndex = s.IndexOf(info.NumberDecimalSeparator);
                        if (decimalSeparatorIndex > -1)
                        {
                            if ((style & NumberStyles.AllowDecimalPoint) == NumberStyles.None) return false;
                            if (s.LastIndexOf(info.NumberDecimalSeparator) != decimalSeparatorIndex) return false;
                            for (int i = decimalSeparatorIndex + info.NumberDecimalSeparator.Length; i < s.Length; i++)
                            {
                                if (char.IsDigit(s[i]))
                                    decimalPlacesToShift--;
                            }
                            s = s.Replace(info.NumberDecimalSeparator, string.Empty);
                        }
                        if (!BigInteger.TryParse(s, intStyle, provider, out num)) return false;
                        if (decimalPlacesToShift < 0)
                            denom = MathHelper.BigIntPow(10, BigInteger.Abs(decimalPlacesToShift));
                        else
                            num *= MathHelper.BigIntPow(10, BigInteger.Abs(decimalPlacesToShift));
                            denom = 1;
                        result = new Rational(num, denom);
                        return true;
                    }
                    else
                        return false;
                }

                /// <summary>
                /// Versucht, den angegebenen String in einen Rational zu konvertieren.
                /// </summary>
                /// <param name="s">Der zu konvertierende <see cref="System.String"/>.</param>
                /// <param name="result">Das Ergebnis der Konvertierung. Wenn die Konvertierung fehlgeschlagen ist, ist dieser Parameter 0.</param>
                /// <returns>
                /// Gibt True zurück, wenn die Konvertierung erfolgreich war, andernfalls False.
                /// </returns>
                /// <remarks>
                /// Die Funktion kann Strings in der Exponentialschreibweise (0.0E0) und der Bruchschreibweise (0/0) verarbeiten.
                /// Die Konvertierung findet unter Verwendung der aktuellen Systemkultur statt.
                /// </remarks>
                public static bool TryParse(string s, out Rational result)
                {
                    return Rational.TryParse(s, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.CurrentCulture.NumberFormat, out result);
                }

                /// <summary>
                /// Konvertiert den angegebenen String unter Verwendung des angegebenen <paramref name="style"/> und <paramref name="provider"/> in einen Rational
                /// </summary>
                /// <param name="s">Der zu konvertierende <see cref="System.String"/>.</param>
                /// <param name="style">Der für die Konvertierung verwendete <see cref="System.Globalization.NumberStyles"/>.</param>
                /// <param name="provider">Der für die Konvertierung verwendete <see cref="System.IFormatProvider"/>.</param>
                /// <returns>
                /// Gibt das Ergebnis der Konvertierung zurück, oder 0, wenn ein Fehler aufgetreten ist.
                /// </returns>
                /// <remarks>
                /// Die Funktion kann Strings in der Exponentialschreibweise (0.0E0) und der Bruchschreibweise (0/0) verarbeiten.
                /// Es sind die NumberStyles AllowLeadingWhite, AllowTrailingWhite, AllowLeadingSign, AllowDecimalPoint, AllowThousands und AllowExponent erlaubt.
                /// </remarks>
                /// <seealso cref="System.Globalization.NumberStyles"/>
                /// <seealso cref="System.IFormatProvider"/>
                /// <exception cref="System.ArgumentNullException">Wird ausgelöst, wenn <paramref name="s"/> Null (Nothing) ist.</exception>
                /// <exception cref="System.ArgumentException">Wird ausgelöst, wenn <paramref name="s"/> leer ist oder ein ungültiger Wert für <paramref name="style"/> übergeben wurde.</exception>
                /// <exception cref="System.FormatException">Tritt auf, wenn die Eingabezeichenfolge <paramref name="s"/> nicht in einen Rational konvertiert werden konnte.</exception>
                public static Rational Parse(string s, NumberStyles style, IFormatProvider provider)
                {
                    if (s == null)
                        throw new ArgumentNullException("s");
                    if (string.IsNullOrEmpty(s))
                        throw new ArgumentException("Der übergebene String ist leer.", "s");
                    if ((style & ~(NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent | NumberStyles.AllowHexSpecifier)) != NumberStyles.None)
                        throw new ArgumentException("Der angegebene NumberStyle wird nicht unterstützt.", "style");
                    if ((style & NumberStyles.AllowHexSpecifier) != NumberStyles.None)
                        throw new ArgumentException("NumberStyles.AllowHexSpecifier ist für eine Gleitkommazahl ungültig.");

                    Rational result;
                    if (Rational.TryParse(s, style, provider, out result))
                        return result;
                    else
                        throw new FormatException();
                }

                /// <summary>
                /// Konvertiert den angegebenen String unter Verwendung des angegebenen <paramref name="style"/> in einen Rational
                /// </summary>
                /// <param name="s">Der zu konvertierende <see cref="System.String"/>.</param>
                /// <param name="style">Der für die Konvertierung verwendete <see cref="System.Globalization.NumberStyles"/>.</param>
                /// <returns>
                /// Gibt das Ergebnis der Konvertierung zurück, oder 0, wenn ein Fehler aufgetreten ist.
                /// </returns>
                /// <remarks>
                /// Die Funktion kann Strings in der Exponentialschreibweise (0.0E0) und der Bruchschreibweise (0/0) verarbeiten.
                /// Es sind die NumberStyles AllowLeadingWhite, AllowTrailingWhite, AllowLeadingSign, AllowDecimalPoint, AllowThousands und AllowExponent erlaubt.
                /// Die Konvertierung findet unter Verwendung der aktuellen Systemkultur statt.
                /// </remarks>
                /// <seealso cref="System.Globalization.NumberStyles"/>
                /// <exception cref="System.ArgumentNullException">Wird ausgelöst, wenn <paramref name="s"/> Null (Nothing) ist.</exception>
                /// <exception cref="System.ArgumentException">Wird ausgelöst, wenn <paramref name="s"/> leer ist oder ein ungültiger Wert für <paramref name="style"/> übergeben wurde.</exception>
                /// <exception cref="System.FormatException">Tritt auf, wenn die Eingabezeichenfolge <paramref name="s"/> nicht in einen Rational konvertiert werden konnte.</exception>
                public static Rational Parse(string s, NumberStyles style)
                {
                    return Rational.Parse(s, style, CultureInfo.CurrentCulture.NumberFormat);
                }
                /// <summary>
                /// Konvertiert den angegebenen String unter Verwendung des angegebenen <paramref name="provider"/> in einen Rational
                /// </summary>
                /// <param name="s">Der zu konvertierende <see cref="System.String"/>.</param>
                /// <param name="provider">Der für die Konvertierung verwendete <see cref="System.IFormatProvider"/>.</param>
                /// <returns>
                /// Gibt das Ergebnis der Konvertierung zurück, oder 0, wenn ein Fehler aufgetreten ist.
                /// </returns>
                /// <remarks>
                /// Die Funktion kann Strings in der Exponentialschreibweise (0.0E0) und der Bruchschreibweise (0/0) verarbeiten.
                /// </remarks>
                /// <seealso cref="System.IFormatProvider"/>
                /// <exception cref="System.ArgumentNullException">Wird ausgelöst, wenn <paramref name="s"/> Null (Nothing) ist.</exception>
                /// <exception cref="System.ArgumentException">Wird ausgelöst, wenn <paramref name="s"/> leer ist.</exception>
                /// <exception cref="System.FormatException">Tritt auf, wenn die Eingabezeichenfolge <paramref name="s"/> nicht in einen Rational konvertiert werden konnte.</exception>
                public static Rational Parse(string s, IFormatProvider provider)
                {
                    return Rational.Parse(s, NumberStyles.Float | NumberStyles.AllowThousands, provider);
                }
                /// <summary>
                /// Konvertiert den angegebenen String in einen Rational
                /// </summary>
                /// <param name="s">Der zu konvertierende <see cref="System.String"/>.</param>
                /// <returns>
                /// Gibt das Ergebnis der Konvertierung zurück, oder 0, wenn ein Fehler aufgetreten ist.
                /// </returns>
                /// <remarks>
                /// Die Funktion kann Strings in der Exponentialschreibweise (0.0E0) und der Bruchschreibweise (0/0) verarbeiten.
                /// Die Konvertierung findet unter Verwendung der aktuellen Systemkultur statt.
                /// </remarks>
                /// <exception cref="System.ArgumentNullException">Wird ausgelöst, wenn <paramref name="s"/> Null (Nothing) ist.</exception>
                /// <exception cref="System.ArgumentException">Wird ausgelöst, wenn <paramref name="s"/> leer ist.</exception>
                /// <exception cref="System.FormatException">Tritt auf, wenn die Eingabezeichenfolge <paramref name="s"/> nicht in einen Rational konvertiert werden konnte.</exception>
                public static Rational Parse(string s)
                {
                    return Rational.Parse(s, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.CurrentCulture.NumberFormat);
                }

                /// <summary>
                /// Addiert zwei Rational-Instanzen miteinander.
                /// </summary>
                /// <returns>
                /// Gibt das Ergebnis der Addition zurück.
                /// </returns>
                /// <exception cref="System.ArgumentException">Wird ausgelöst, wenn <paramref name="left"/> oder <paramref name="right"/> NaN ist.</exception>
                public static Rational Add(Rational left, Rational right)
                {
                    if (left.IsNaN) throw new ArgumentException(errorNaN, "left");
                    if (right.IsNaN) throw new ArgumentException(errorNaN, "right");
                    if (left == Rational.Zero) return right;
                    if (right == Rational.Zero) return left;
                    return new Rational(left.Numerator * right.Denominator + right.Numerator * left.Denominator, left.Denominator * right.Denominator);
                }

                /// <summary>
                /// Subtrahiert einen Rational von einem anderen.
                /// </summary>
                /// <returns>
                /// Gibt das Ergebnis der Subtraktion zurück.
                /// </returns>
                /// <exception cref="System.ArgumentException">Wird ausgelöst, wenn <paramref name="left"/> oder <paramref name="right"/> NaN ist.</exception>
                public static Rational Subtract(Rational left, Rational right)
                {
                    if (left.IsNaN) throw new ArgumentException(errorNaN, "left");
                    if (right.IsNaN) throw new ArgumentException(errorNaN, "right");
                    if (left == Rational.Zero) return -right;
                    if (right == Rational.Zero) return left;
                    return new Rational(left.Numerator * right.Denominator - right.Numerator * left.Denominator, left.Denominator * right.Denominator);
                }

                /// <summary>
                /// Multipliziert zwei Rational-Instanzen miteinander.
                /// </summary>
                /// <returns>
                /// Gibt das Ergebnis der Multiplikation zurück.
                /// </returns>
                /// <exception cref="System.ArgumentException">Wird ausgelöst, wenn <paramref name="left"/> oder <paramref name="right"/> NaN ist.</exception>
                public static Rational Multiply(Rational left, Rational right)
                {
                    if (left.IsNaN) throw new ArgumentException(errorNaN, "left");
                    if (right.IsNaN) throw new ArgumentException(errorNaN, "right");
                    if (left == Rational.Zero || right == Rational.Zero) return Rational.Zero;
                    return new Rational(left.Numerator * right.Numerator, left.Denominator * right.Denominator);
                }

                /// <summary>
                /// Dividiert einen Rational durch einen anderen.
                /// </summary>
                /// <returns>
                /// Gibt das Ergebnis der Division zurück.
                /// </returns>
                /// <exception cref="System.ArgumentException">Wird ausgelöst, wenn <paramref name="dividend"/> oder <paramref name="divisor"/> NaN ist.</exception>
                /// <exception cref="System.DivideByZeroException">Wird ausgelöst, wenn versucht wurde durch 0 zu teilen.</exception>
                public static Rational Divide(Rational dividend, Rational divisor)
                {
                    if (dividend.IsNaN) throw new ArgumentException(errorNaN, "dividend");
                    if (divisor.IsNaN) throw new ArgumentException(errorNaN, "divisor");
                    if (divisor == Rational.Zero) throw new DivideByZeroException();
                    if (dividend == Rational.Zero) return Rational.Zero;
                    return new Rational(dividend.Numerator * divisor.Denominator, dividend.Denominator * divisor.Numerator);
                }

                /// <summary>
                /// Führt eine Ganzzahldivision durch und gibt den Rest zurück.
                /// </summary>
                /// <returns>
                /// Gibt den Rest der Ganzzahldivision zurück.
                /// </returns>
                /// <exception cref="System.ArgumentException">Wird ausgelöst, wenn <paramref name="dividend"/> oder <paramref name="divisor"/> NaN ist.</exception>
                /// <exception cref="System.DivideByZeroException">Wird ausgelöst, wenn versucht wurde durch 0 zu teilen.</exception>
                public static Rational Remainder(Rational dividend, Rational divisor)
                {
                    if (dividend.IsNaN) throw new ArgumentException(errorNaN, "dividend");
                    if (divisor.IsNaN) throw new ArgumentException(errorNaN, "divisor");
                    if (divisor == Rational.Zero) throw new DivideByZeroException();
                    if (dividend == Rational.Zero) return Rational.Zero;
                    return new Rational(dividend.Numerator * divisor.Denominator % divisor.Numerator * dividend.Denominator, dividend.Denominator * divisor.Denominator);
                }

                /// <summary>
                /// Führt eine Ganzzahldivision durch und gibt den Rest als Parameter zurück.
                /// </summary>
                /// <returns>
                /// Gibt das Ergebnis der Ganzzahldivision zurück.
                /// </returns>
                /// <exception cref="System.ArgumentException">Wird ausgelöst, wenn <paramref name="dividend"/> oder <paramref name="divisor"/> NaN ist.</exception>
                /// <exception cref="System.DivideByZeroException">Wird ausgelöst, wenn versucht wurde durch 0 zu teilen.</exception>
                public static Rational DivRem(Rational dividend, Rational divisor, out Rational remainder)
                {
                    remainder = default(Rational);
                    if (dividend.IsNaN) throw new ArgumentException(errorNaN, "dividend");
                    if (divisor.IsNaN) throw new ArgumentException(errorNaN, "divisor");
                    if (divisor == Rational.Zero) throw new DivideByZeroException();
                    if (dividend == Rational.Zero) return BigInteger.Zero;
                    BigInteger ad = dividend.Numerator * divisor.Denominator;
                    BigInteger cb = divisor.Numerator * dividend.Denominator;
                    BigInteger bd = dividend.Denominator + divisor.Denominator;
                    remainder = new Rational(ad % cb, bd);
                    return ad / cb;
                }

                /// <summary>
                /// Potenziert einen Rational mit einem BigInteger.
                /// </summary>
                /// <returns>
                /// Gibt das Ergebnis der Potenz zurück.
                /// </returns>
                /// <exception cref="System.ArgumentException">Wird ausgelöst, wenn <paramref name="base"/> NaN ist.</exception>
                public static Rational Pow(Rational @base, BigInteger exponent)
                {
                    if (@base.IsNaN) throw new ArgumentException(errorNaN, "base");
                    if (exponent == BigInteger.Zero) return Rational.One;
                    if (@base == Rational.Zero) return Rational.Zero;
                    if (exponent < 0)
                        return new Rational(MathHelper.BigIntPow(@base.Denominator, exponent), MathHelper.BigIntPow(@base.Numerator, exponent));
                    else
                        return new Rational(MathHelper.BigIntPow(@base.Numerator, exponent), MathHelper.BigIntPow(@base.Denominator, exponent));
                }

                ///// <summary>
                ///// Potenziert einen Rational.
                ///// </summary>
                ///// <returns>
                ///// Gibt das Ergebnis der Potenz zurück.
                ///// </returns>
                ///// <exception cref="System.ArgumentException">Wird ausgelöst, wenn <paramref name="base"/>, <paramref name="exponent"/> oder <paramref name="epsilon"/> NaN ist, oder wenn <paramref name="base"/> negativ ist.</exception>
                //public static Rational Pow(Rational @base, Rational exponent, Rational epsilon)
                //{
                //    if (@base.IsNaN) throw new ArgumentException(errorNaN, "base");
                //    if (exponent.IsNaN) throw new ArgumentException(errorNaN, "exponent");
                //    if (epsilon.IsNaN) throw new ArgumentException(errorNaN, "epsilon");
                //    if (@base.Sign == -1) throw new ArgumentException("Die Basis darf bei einem gebrochenen Exponenten nicht negativ sein.", "base");
                //    if (exponent == Rational.Zero) return Rational.One;
                //    if (@base == Rational.Zero) return Rational.Zero;
                //    Rational invertedDenom = Rational.Invert(exponent.Denominator);
                //    Rational roundingVal = epsilon < 1 ? epsilon * epsilon : Rational.Sqrt(epsilon);
                //    Rational result = @base;
                //    Rational delta = 0;
                //    do
                //    {
                //        delta = invertedDenom * (@base / Rational.Pow(result, exponent.Denominator - 1) - result);
                //        result -= delta;
                //        result -= result % roundingVal;
                //    } while (Rational.Abs(delta) > epsilon);
                //    return Rational.Pow(result, exponent.Numerator);
                //}

                ///// <summary>
                ///// Potenziert einen Rational.
                ///// </summary>
                ///// <returns>
                ///// Gibt das Ergebnis der Potenz zurück.
                ///// </returns>
                ///// <exception cref="System.ArgumentException">Wird ausgelöst, wenn <paramref name="base"/> oder <paramref name="exponent"/> NaN ist.</exception>
                //public static Rational Pow(Rational @base, Rational exponent)
                //{
                //    if (@base.IsNaN) throw new ArgumentException(errorNaN, "base");
                //    if (exponent.IsNaN) throw new ArgumentException(errorNaN, "exponent");
                //    Rational epsilon = @base * 1E-10m;
                //    return Rational.Pow(@base, exponent, epsilon);
                //}

                /// <summary>
                /// Negiert eine Rational-Instanz.
                /// </summary>
                /// <returns>
                /// Gibt das Ergebnis der Negation zurück.
                /// </returns>
                /// <exception cref="System.ArgumentException">Wird ausgelöst, wenn <paramref name="value"/> NaN ist.</exception>
                public static Rational Negate(Rational value)
                {
                    if (value.IsNaN) throw new ArgumentException(errorNaN, "value");
                    if (value == Rational.Zero) return Rational.Zero;
                    return new Rational(-value.Numerator, value.Denominator);
                }

                /// <summary>
                /// Invertiert einen Rational.
                /// </summary>
                /// <returns>
                /// Gibt das Ergebnis der Invertierung zurück.
                /// </returns>
                /// <exception cref="System.ArgumentException">Wird ausgelöst, wenn <paramref name="value"/> NaN ist.</exception>
                public static Rational Invert(Rational value)
                {
                    if (value.IsNaN) throw new ArgumentException(errorNaN, "value");
                    if (value == Rational.Zero) return Rational.Zero;
                    return new Rational(value.Denominator, value.Numerator);
                }

                /// <summary>
                /// Bestimmt den Absolutbetrag eines Rational.
                /// </summary>
                /// <returns>
                /// Gibt den Absolutbetrag von <paramref name="value"/> zurück.
                /// </returns>
                /// <exception cref="System.ArgumentException">Wird ausgelöst, wenn <paramref name="value"/> NaN ist.</exception>
                public static Rational Abs(Rational value)
                {
                    if (value.IsNaN) throw new ArgumentException(errorNaN, "value");
                    return new Rational(BigInteger.Abs(value.Numerator), value.Denominator);
                }

                /// <summary>
                /// Gibt die größte Ganzzahl zurück, die kleiner oder gleich der angegebenen Zahl ist.
                /// </summary>
                /// <exception cref="System.ArgumentException">Wird ausgelöst, wenn <paramref name="value"/> NaN ist.</exception>
                public static BigInteger Floor(Rational value)
                {
                    if (value.IsNaN) throw new ArgumentException(errorNaN, "value");
                    return value.Numerator / value.Denominator;
                }

                /// <summary>
                /// Gibt die kleinste Ganzzahl zurück, die größer oder gleich der angegebenen Zahl ist.
                /// </summary>
                /// <exception cref="System.ArgumentException">Wird ausgelöst, wenn <paramref name="value"/> NaN ist.</exception>
                public static BigInteger Ceiling(Rational value)
                {
                    if (value.IsNaN) throw new ArgumentException(errorNaN, "value");
                    return (value.Numerator + value.Denominator - 1) / value.Denominator * value.Denominator;
                }

                /// <summary>
                /// Rundet die angegebene zahl auf die angegebene Anzahl an Nachkommastellen.
                /// </summary>
                /// <exception cref="System.ArgumentException">Wird ausgelöst, wenn <paramref name="value"/> NaN ist.</exception>
                /// <exception cref="System.ArgumentOutOfRangeException">Wird ausgelöst, wenn <paramref name="decimalPlaces"/> kleiner als 0 ist.</exception>
                public static Rational Round(Rational value, BigInteger decimalPlaces)
                {
                    if (value.IsNaN) throw new ArgumentException(errorNaN, "value");
                    if (decimalPlaces < 0) throw new ArgumentOutOfRangeException("Die Anzahl an Dezimalstellen muss positiv sein.", "decimalPlaces");
                    BigInteger factor = MathHelper.BigIntPow(10, decimalPlaces);
                    Rational result = value * factor;
                    result = Rational.Floor(result + 0.5m);
                    result /= factor;
                    return result;
                }

                /// <summary>
                /// Bestimmt die größere von zwei Rational-Instanzen.
                /// </summary>
                /// <returns>
                /// Gibt die größere der beiden Rational-Instanzen zurück.
                /// </returns>
                /// <exception cref="System.ArgumentException">Wird ausgelöst, wenn <paramref name="left"/> oder <paramref name="right"/> NaN ist.</exception>
                public static Rational Max(Rational left, Rational right)
                {
                    if (left.IsNaN) throw new ArgumentException(errorNaN, "left");
                    if (right.IsNaN) throw new ArgumentException(errorNaN, "right");
                    if (left > right) return left;
                    return right;
                }

                /// <summary>
                /// Bestimmt die kleinere von zwei Rational-Instanzen.
                /// </summary>
                /// <returns>
                /// Gibt die kleinere der beiden Rational-Instanzen zurück.
                /// </returns>
                /// <exception cref="System.ArgumentException">Wird ausgelöst, wenn <paramref name="left"/> oder <paramref name="right"/> NaN ist.</exception>
                public static Rational Min(Rational left, Rational right)
                {
                    if (left.IsNaN) throw new ArgumentException(errorNaN, "left");
                    if (right.IsNaN) throw new ArgumentException(errorNaN, "right");
                    if (left < right) return left;
                    return right;
                }

                ///// <summary>
                ///// Zieht die Quadratwurzel zum angegebenen Rational unter berücksichtigeng des angegebenen Epsilon.
                ///// </summary>
                ///// <returns>
                ///// Gibt die Quadratwurzel des angegebenen Rational zurück.
                ///// </returns>
                ///// <exception cref="System.ArgumentException">Wird ausgelöst, wenn <paramref name="value"/> oder <paramref name="epsilon"/> NaN ist.</exception>
                //public static Rational Sqrt(Rational value, Rational epsilon)
                //{
                //    if (value.IsNaN) throw new ArgumentException(errorNaN, "value");
                //    if (epsilon.IsNaN) throw new ArgumentException(errorNaN, "epsilon");
                //    if (value.Sign == -1) throw new ArgumentException("Negative Werte besitzen keine Quadratwurzel.");
                //    if (value == Rational.Zero) return Rational.Zero;
                //    Rational result = (value + 1) / 2;
                //    while (Rational.Abs(value - result * result) > epsilon)
                //        result = (result + value / result) / 2;
                //    return result;
                //}

                ///// <summary>
                ///// Zieht die Quadratwurzel zum angegebenen Rational.
                ///// </summary>
                ///// <returns>
                ///// Gibt die Quadratwurzel des angegebenen Rational zurück.
                ///// </returns>
                ///// <exception cref="System.ArgumentException">Wird ausgelöst, wenn <paramref name="value"/> NaN ist.</exception>
                //public static Rational Sqrt(Rational value)
                //{
                //    if (value.IsNaN) throw new ArgumentException(errorNaN, "value");
                //    Rational epsilon = value * 1E-10m;
                //    return Rational.Sqrt(value, epsilon);
                //}

                /// <summary>
                /// Addiert zwei Rational-Instanzen miteinander.
                /// </summary>
                public static Rational operator +(Rational left, Rational right)
                {
                    return Rational.Add(left, right);
                }
                /// <summary>
                /// Subtrahiert einen Rational von einem anderen.
                /// </summary>
                public static Rational operator -(Rational left, Rational right)
                {
                    return Rational.Subtract(left, right);
                }
                /// <summary>
                /// Multipliziert zwei Rational-Instanzen miteinander.
                /// </summary>
                public static Rational operator *(Rational left, Rational right)
                {
                    return Rational.Multiply(left, right);
                }
                /// <summary>
                /// Dividiert einen Rational durch einen anderen.
                /// </summary>
                public static Rational operator /(Rational left, Rational right)
                {
                    return Rational.Divide(left, right);
                }
                /// <summary>
                /// Führt eine Ganzzahldivision durch und gibt den Rest zurück.
                /// </summary>
                public static Rational operator %(Rational left, Rational right)
                {
                    return Rational.Remainder(left, right);
                }
                /// <summary>
                /// Gibt <paramref name="value"/> zurück.
                /// </summary>
                public static Rational operator +(Rational value)
                {
                    return value;
                }
                /// <summary>
                /// Negiert eine Rational-Instanz.
                /// </summary>
                public static Rational operator -(Rational value)
                {
                    return Rational.Negate(value);
                }

                /// <summary>
                /// Prüft zwei Rational-Instanzen auf Gleichheit.
                /// </summary>
                public static bool operator ==(Rational left, Rational right)
                {
                    if (left.IsNaN || right.IsNaN) return false;
                    if (left.Numerator == 0 && right.Numerator == 0) return true;
                    return (left.Numerator == right.Numerator && left.Denominator == right.Denominator);
                }

                /// <summary>
                /// Prüft zwei Rational-Instanzen auf Ungleichheit.
                /// </summary>
                public static bool operator !=(Rational left, Rational right)
                {
                    if (left.IsNaN || right.IsNaN) return true;
                    if (left.Numerator == 0 && right.Numerator == 0) return false;
                    return (left.Numerator != right.Numerator || left.Denominator != right.Denominator);
                }

                /// <summary>
                /// Prüft, ob eine Rational-Instanz kleiner als eine andere ist.
                /// </summary>
                public static bool operator <(Rational left, Rational right)
                {
                    if (left.IsNaN || right.IsNaN) return false;
                    if (left.Numerator == 0) return right.Sign == 1;
                    if (right.Numerator == 0) return left.Sign == -1;
                    return left.Numerator * right.Denominator < right.Numerator * left.Denominator;
                }

                /// <summary>
                /// Prüft, ob eine Rational-Instanz größer als eine andere ist.
                /// </summary>
                public static bool operator >(Rational left, Rational right)
                {
                    if (left.IsNaN || right.IsNaN) return false;
                    if (left.Numerator == 0) return right.Sign == -1;
                    if (right.Numerator == 0) return left.Sign == 1;
                    return left.Numerator * right.Denominator > right.Numerator * left.Denominator;
                }

                /// <summary>
                /// Prüft, ob eine Rational-Instanz kleiner oder gleich einer anderen ist.
                /// </summary>
                public static bool operator <=(Rational left, Rational right)
                {
                    if (left.IsNaN || right.IsNaN) return false;
                    if (left.Numerator == 0) return right.Sign != -1;
                    if (right.Numerator == 0) return left.Sign != 1;
                    return left.Numerator * right.Denominator <= right.Numerator * left.Denominator;
                }

                /// <summary>
                /// Prüft, ob eine Rational-Instanz größer oder gleich einer anderen ist.
                /// </summary>
                public static bool operator >=(Rational left, Rational right)
                {
                    if (left.IsNaN || right.IsNaN) return false;
                    if (left.Numerator == 0) return right.Sign != 1;
                    if (right.Numerator == 0) return left.Sign != -1;
                    return left.Numerator * right.Denominator >= right.Numerator * left.Denominator;
                }

                /// <summary>
                /// Serialisiert diesen Rational.
                /// </summary>
                public void GetObjectData(SerializationInfo info, StreamingContext context)
                {
                    info.AddValue("Numerator", Numerator, typeof(BigInteger));
                    info.AddValue("Denominator", Denominator, typeof(BigInteger));
                }

                /// <summary>
                /// Deserialisiert die angegebenen Daten in einen Rational.
                /// </summary>
                public Rational(SerializationInfo info, StreamingContext context)
                    : this()
                {
                    Numerator = (BigInteger)info.GetValue("Numerator", typeof(BigInteger));
                    Denominator = (BigInteger)info.GetValue("Denominator", typeof(BigInteger));
                }

                /// <summary>
                /// Erstellt eine neue Rational-Instanz.
                /// </summary>
                /// <param name="numerator">Der Zähler des Rationals.</param>
                /// <param name="denominator">Der nenner des Rationals</param>
                public Rational(BigInteger numerator, BigInteger denominator)
                    : this()
                {
                    if (denominator == 0)
                    {
                        Numerator = 0;
                        Denominator = 0;
                        return;
                    }
                    if (numerator == 0)
                    {
                        Numerator = 0;
                        Denominator = 1;
                        return;
                    }
                    BigInteger num = BigInteger.Abs(numerator);
                    BigInteger denom = BigInteger.Abs(denominator);
                    BigInteger gcd = BigInteger.GreatestCommonDivisor(num, denom);
                    Numerator = num / gcd;
                    Denominator = denom / gcd;
                    if (numerator < 0 ^ denominator < 0)
                        Numerator = -Numerator;
                }

                /// <summary>
                /// Erstellt eine neue Rational-Instanz.
                /// </summary>
                /// <param name="value">Ein <see cref="System.Decimal"/>, der den Wert des Rationals darstellt.</param>
                unsafe public Rational(decimal value)
                    : this()
                {
                    fixed (int* ints = decimal.GetBits(value))
                    {
                        byte* bytes = (byte*)ints;
                        byte[] numeratorBytes = new byte[12];
                        Marshal.Copy((IntPtr)bytes, numeratorBytes, 0, 12);
                        Numerator = new BigInteger(numeratorBytes);
                        Denominator = new BigInteger(System.Math.Pow(10, bytes[14]));
                        BigInteger gcd = BigInteger.GreatestCommonDivisor(Numerator, Denominator);
                        Numerator /= gcd;
                        Denominator /= gcd;
                        if (bytes[15] == 1)
                            Numerator = -Numerator; 
                    }
                }

                /// <summary>
                /// Erstellt eine neue Rational-Instanz.
                /// </summary>
                /// <param name="value">Ein <see cref="System.Double"/>, der den Wert des Rationals darstellt.</param>
                unsafe public Rational(double value)
                    : this()
                {
                    if (double.IsNaN(value) || double.IsInfinity(value))
                    {
                        Denominator = 0;
                        return;
                    }
                    if (value == 0) return;
                    ulong rawValue = *(ulong*)&value;
                    ulong denom = 1ul << 52;
                    ulong mantissa = (rawValue & 0xFFFFFFFFFFFFFul) | denom;
                    int exponent = ((int)(rawValue >> 52) & 0x7FF) - 1023;
                    if (exponent == 0)
                    {
                        while ((mantissa & 1) == 0)
                        {
                            mantissa >>= 1;
                            denom >>= 1;
                        }
                        Numerator = mantissa;
                        Denominator = denom;
                    }
                    else if (exponent > 0)
                    {
                        while (exponent > 0 && denom > 1)
                        {
                            denom >>= 1;
                            exponent--;
                        }
                        Numerator = mantissa;
                        if (exponent > 0) Numerator <<= exponent;
                        Denominator = denom;
                    }
                    else
                    {
                        Denominator = denom;
                        Denominator <<= System.Math.Abs(exponent);
                        while ((mantissa & 1) == 0)
                        {
                            mantissa >>= 1;
                            Denominator >>= 1;
                        }
                        Numerator = mantissa;
                    }
                    BigInteger gcd = BigInteger.GreatestCommonDivisor(Numerator, Denominator);
                    Numerator /= gcd;
                    Denominator /= gcd;
                    if ((rawValue & (1ul << 63)) != 0) Numerator = -Numerator;
                }

                /// <summary>
                /// Erstellt eine neue Rational-Instanz.
                /// </summary>
                /// <param name="value">Ein <see cref="System.Single"/>, der den Wert des Rationals darstellt.</param>
                unsafe public Rational(float value)
                    : this()
                {
                    if (float.IsNaN(value) || float.IsInfinity(value))
                    {
                        Denominator = 0;
                        return;
                    }
                    if (value == 0) return;
                    uint rawValue = *(uint*)&value;
                    uint denom = 1u << 23;
                    uint mantissa = (rawValue & 0x7FFFFFu) | denom;
                    int exponent = ((int)(rawValue >> 23) & 0xFF) - 127;
                    if (exponent == 0)
                    {
                        while ((mantissa & 1) == 0)
                        {
                            mantissa >>= 1;
                            denom >>= 1;
                        }
                        Numerator = mantissa;
                        Denominator = denom;
                    }
                    else if (exponent > 0)
                    {
                        while (exponent > 0 && denom > 1)
                        {
                            denom >>= 1;
                            exponent--;
                        }
                        Numerator = mantissa;
                        if (exponent > 0) Numerator <<= exponent;
                        Denominator = denom;
                    }
                    else
                    {
                        Denominator = denom;
                        Denominator <<= System.Math.Abs(exponent);
                        while ((mantissa & 1) == 0)
                        {
                            mantissa >>= 1;
                            Denominator >>= 1;
                        }
                        Numerator = mantissa;
                    }
                    BigInteger gcd = BigInteger.GreatestCommonDivisor(Numerator, Denominator);
                    Numerator /= gcd;
                    Denominator /= gcd;
                    if ((rawValue & (1u << 31)) != 0) Numerator = -Numerator;
                }

                /// <summary>
                /// Erstellt eine neue Rational-Instanz.
                /// </summary>
                /// <param name="value">Ein <see cref="System.int64"/>, der den Wert des Rationals darstellt.</param>
                public Rational(long value)
                    : this()
                {
                    Numerator = value;
                    Denominator = 1;
                }

                /// <summary>
                /// Erstellt eine neue Rational-Instanz.
                /// </summary>
                /// <param name="value">Ein <see cref="System.UInt64"/>, der den Wert des Rationals darstellt.</param>
                public Rational(ulong value)
                    : this()
                {
                    Numerator = value;
                    Denominator = 1;
                }

                /// <summary>
                /// Erstellt eine neue Rational-Instanz.
                /// </summary>
                /// <param name="value">Ein <see cref="System.Int32"/>, der den Wert des Rationals darstellt.</param>
                public Rational(int value)
                    : this()
                {
                    Numerator = value;
                    Denominator = 1;
                }

                /// <summary>
                /// Erstellt eine neue Rational-Instanz.
                /// </summary>
                /// <param name="value">Ein <see cref="System.UInt32"/>, der den Wert des Rationals darstellt.</param>
                public Rational(uint value)
                    : this()
                {
                    Numerator = value;
                    Denominator = 1;
                }

                /// <summary>
                /// Erstellt eine neue Rational-Instanz.
                /// </summary>
                /// <param name="value">Ein <see cref="System.Int16"/>, der den Wert des Rationals darstellt.</param>
                public Rational(short value)
                    : this()
                {
                    Numerator = value;
                    Denominator = 1;
                }

                /// <summary>
                /// Erstellt eine neue Rational-Instanz.
                /// </summary>
                /// <param name="value">Ein <see cref="System.UInt16"/>, der den Wert des Rationals darstellt.</param>
                public Rational(ushort value)
                    : this()
                {
                    Numerator = value;
                    Denominator = 1;
                }

                /// <summary>
                /// Erstellt eine neue Rational-Instanz.
                /// </summary>
                /// <param name="value">Ein <see cref="System.Byte"/>, das den Wert des Rationals darstellt.</param>
                public Rational(byte value)
                    : this()
                {
                    Numerator = value;
                    Denominator = 1;
                }

                /// <summary>
                /// Erstellt eine neue Rational-Instanz.
                /// </summary>
                /// <param name="value">Ein <see cref="System.SByte"/>, das den Wert des Rationals darstellt.</param>
                public Rational(sbyte value)
                    : this()
                {
                    Numerator = value;
                    Denominator = 1;
                }

                /// <summary>
                /// Erstellt eine neue Rational-Instanz.
                /// </summary>
                /// <param name="value">Ein <see cref="System.Numerics.BigInteger"/>, der den Wert des Rationals darstellt.</param>
                public Rational(BigInteger value)
                    : this()
                {
                    Numerator = value;
                    Denominator = 1;
                }

                /// <summary>
                /// Konvertiert einen <see cref="System.Decimal"/> in einen Rational.
                /// </summary>
                public static implicit operator Rational(decimal value)
                {
                    return new Rational(value);
                }
                /// <summary>
                /// Konvertiert einen <see cref="System.Double"/> in einen Rational.
                /// </summary>
                public static implicit operator Rational(double value)
                {
                    return new Rational(value);
                }
                /// <summary>
                /// Konvertiert einen <see cref="System.Single"/> in einen Rational.
                /// </summary>
                public static implicit operator Rational(float value)
                {
                    return new Rational(value);
                }
                /// <summary>
                /// Konvertiert einen <see cref="System.Int64"/> in einen Rational.
                /// </summary>
                public static implicit operator Rational(long value)
                {
                    return new Rational(value);
                }
                /// <summary>
                /// Konvertiert einen <see cref="System.UInt64"/> in einen Rational.
                /// </summary>
                public static implicit operator Rational(ulong value)
                {
                    return new Rational(value);
                }
                /// <summary>
                /// Konvertiert einen <see cref="System.Int32"/> in einen Rational.
                /// </summary>
                public static implicit operator Rational(int value)
                {
                    return new Rational(value);
                }
                /// <summary>
                /// Konvertiert einen <see cref="System.UInt32"/> in einen Rational.
                /// </summary>
                public static implicit operator Rational(uint value)
                {
                    return new Rational(value);
                }
                /// <summary>
                /// Konvertiert einen <see cref="System.Int16"/> in einen Rational.
                /// </summary>
                public static implicit operator Rational(short value)
                {
                    return new Rational(value);
                }
                /// <summary>
                /// Konvertiert einen <see cref="System.UInt16"/> in einen Rational.
                /// </summary>
                public static implicit operator Rational(ushort value)
                {
                    return new Rational(value);
                }
                /// <summary>
                /// Konvertiert ein <see cref="System.Byte"/> in einen Rational.
                /// </summary>
                public static implicit operator Rational(byte value)
                {
                    return new Rational(value);
                }
                /// <summary>
                /// Konvertiert ein <see cref="System.SByte"/> in einen Rational.
                /// </summary>
                public static implicit operator Rational(sbyte value)
                {
                    return new Rational(value);
                }
                /// <summary>
                /// Konvertiert einen <see cref="System.Numerics.BigInteger"/> in einen Rational.
                /// </summary>
                public static implicit operator Rational(BigInteger value)
                {
                    return new Rational(value);
                }

                /// <summary>
                /// Konvertiert einen Rational in einen <see cref="System.Decimal"/>.
                /// </summary>
                unsafe public static explicit operator decimal(Rational value)
                {
                    BigInteger num = BigInteger.Abs(value.Numerator);
                    BigInteger denom = value.Denominator;
                    BigInteger r = num % denom;
                    BigInteger x;
                    byte decimalPlaces = 0;
                    while (r != 0 && decimalPlaces < 28)
                    {
                        r *= 10;
                        x = r / denom;
                        r = r - denom * x;
                        decimalPlaces++;
                    }
                    num *= (long)System.Math.Pow(10, decimalPlaces);
                    num /= denom;
                    if ((num >> 96) > 0) throw new OverflowException("Der Wert war für einen Decimal zu groß oder zu klein.");
                    uint lo = (uint)(num & uint.MaxValue);
                    uint mid = (uint)((num >> 32) & uint.MaxValue);
                    uint hi = (uint)((num >> 64) & uint.MaxValue);
                    return new decimal(*(int*)&lo, *(int*)&mid, *(int*)&hi, value.Sign == -1, decimalPlaces);
                }
                /// <summary>
                /// Konvertiert einen Rational in einen <see cref="System.Double"/>.
                /// </summary>
                unsafe public static explicit operator double(Rational value)
                {
                    try
                    {
                        BigInteger num = BigInteger.Abs(value.Numerator);
                        double numLog = BigInteger.Log(num, 2);
                        BigInteger denom = value.Denominator;
                        double denomLog = BigInteger.Log(denom, 2);
                        BigInteger mantissa = num << 53;
                        mantissa >>= (int)(numLog - denomLog);
                        mantissa /= denom;
                        ulong exponent = (ulong)(numLog - denomLog + 1023) << 52;
                        if ((exponent & 0x7FFul) != 0) throw new Exception();
                        ulong rawData = exponent | ((ulong)mantissa & ~(1ul << 52));
                        if (value.Sign == -1) rawData |= 1ul << 63;
                        return *(double*)&rawData;
                    }
                    catch (Exception ex)
                    {
                        throw new OverflowException("Der Wert war für einen Double zu groß oder zu klein.", ex);
                    }
                }
                /// <summary>
                /// Konvertiert einen Rational in einen <see cref="System.Single"/>.
                /// </summary>
                unsafe public static explicit operator float(Rational value)
                {
                    try
                    {
                        BigInteger num = BigInteger.Abs(value.Numerator);
                        double numLog = BigInteger.Log(num, 2);
                        BigInteger denom = value.Denominator;
                        double denomLog = BigInteger.Log(denom, 2);
                        BigInteger mantissa = num << 24;
                        mantissa >>= (int)(numLog - denomLog);
                        mantissa /= denom;
                        uint exponent = (uint)(numLog - denomLog + 127) << 23;
                        if ((exponent & 0xFFu) != 0) throw new Exception();
                        uint rawData = exponent | ((uint)mantissa & ~(1u << 23));
                        if (value.Sign == -1) rawData |= 1u << 31;
                        return *(float*)&rawData;
                    }
                    catch (Exception ex)
                    {
                        throw new OverflowException("Der Wert war für einen Single zu groß oder zu klein.", ex);
                    }
                }
                /// <summary>
                /// Konvertiert einen Rational in einen <see cref="System.Int64"/>.
                /// </summary>
                public static explicit operator long(Rational value)
                {
                    return (long)(value.Numerator / value.Denominator);
                }
                /// <summary>
                /// Konvertiert einen Rational in einen <see cref="System.UInt64"/>.
                /// </summary>
                public static explicit operator ulong(Rational value)
                {
                    return (ulong)(value.Numerator / value.Denominator);
                }
                /// <summary>
                /// Konvertiert einen Rational in einen <see cref="System.Int32"/>.
                /// </summary>
                public static explicit operator int(Rational value)
                {
                    return (int)(value.Numerator / value.Denominator);
                }
                /// <summary>
                /// Konvertiert einen Rational in einen <see cref="System.UInt32"/>.
                /// </summary>
                public static explicit operator uint(Rational value)
                {
                    return (uint)(value.Numerator / value.Denominator);
                }
                /// <summary>
                /// Konvertiert einen Rational in einen <see cref="System.Int16"/>.
                /// </summary>
                public static explicit operator short(Rational value)
                {
                    return (short)(value.Numerator / value.Denominator);
                }
                /// <summary>
                /// Konvertiert einen Rational in einen <see cref="System.UInt16"/>.
                /// </summary>
                public static explicit operator ushort(Rational value)
                {
                    return (ushort)(value.Numerator / value.Denominator);
                }
                /// <summary>
                /// Konvertiert einen Rational in ein <see cref="System.Byte"/>.
                /// </summary>
                public static explicit operator byte(Rational value)
                {
                    return (byte)(value.Numerator / value.Denominator);
                }
                /// <summary>
                /// Konvertiert einen Rational in ein <see cref="System.SByte"/>.
                /// </summary>
                public static explicit operator sbyte(Rational value)
                {
                    return (sbyte)(value.Numerator / value.Denominator);
                }
                /// <summary>
                /// Konvertiert einen Rational in einen <see cref="System.Numerics.BigInteger"/>.
                /// </summary>
                public static explicit operator BigInteger(Rational value)
                {
                    return value.Numerator / value.Denominator;
                }

                private void GetDigits(int maxDecimalPlaces, out string intDigits, out string decimalDigits)
                {
                    intDigits = (Numerator / Denominator).ToString();
                    if (maxDecimalPlaces < 1)
                    {
                        decimalDigits = string.Empty;
                        return;
                    }
                    StringBuilder sb = new StringBuilder();
                    BigInteger num = BigInteger.Abs(Numerator);
                    BigInteger denom = Denominator;
                    BigInteger r = num % denom;
                    BigInteger x;
                    bool enableCount = false;
                    while (r != 0 && maxDecimalPlaces > 0)
                    {
                        r *= 10;
                        x = r / denom;
                        if (x != 0 && !enableCount) enableCount = true;
                        sb.Append(x.ToString());
                        r = r - denom * x;
                        if (enableCount) maxDecimalPlaces--;
                    }
                    decimalDigits = sb.ToString();
                }

                /// <summary>
                /// Konvertiert den Wert dieses Rationals in seine Zeichenfolgendarstellung unter Angabe der maximal sichtbaren Stellen und eines IFormatProviders.
                /// </summary>
                /// <param name="maxPlaces">Die Anzahl an Stellen, die in der Ausgabe zu sehen sind.</param>
                /// <param name="provider">Der zu verwendende IFormatProvider.</param>
                /// <remarks>
                /// Überschreitet die benötigte Anzahl Stellen die maximale Anzahl Stellen, werden überstehende Nullen als Exponent dargestellt und der Rest von der Ausgabe abgeschnitten.
                /// </remarks>
                /// <seealso cref="System.IFormatProvider"/>
                public string ToString(int maxPlaces, IFormatProvider provider)
                {
                    if (maxPlaces < 1) throw new ArgumentException("Die maximale Anzahl sichtbarer Stellen muss größer als 0 sein.");
                    if (this.IsNaN) return CultureInfo.CurrentCulture.NumberFormat.NaNSymbol;
                    string intDigits;
                    string decimalDigits;
                    GetDigits(maxPlaces, out intDigits, out decimalDigits);
                    NumberFormatInfo info = NumberFormatInfo.GetInstance(provider);
                    if (intDigits.Length + decimalDigits.Length <= maxPlaces)
                    {
                        if (decimalDigits.Length == 0)
                            return intDigits;
                        else
                            return string.Concat(intDigits, info.NumberDecimalSeparator, decimalDigits);
                    }
                    else if (intDigits == "0")
                    {
                        int leadingZeros = 0;
                        for (int i = 0; i < decimalDigits.Length; i++)
                        {
                            if (decimalDigits[i] == '0')
                                leadingZeros++;
                            else
                                break;
                        }
                        string visibleDigitsExponential = decimalDigits.Substring(leadingZeros, System.Math.Min(maxPlaces, decimalDigits.Length - leadingZeros)).TrimEnd(new[] {'0'});
                        string visibleDigitsFloatingPoint = decimalDigits.Substring(0, System.Math.Min(maxPlaces - 1, decimalDigits.Length)).Trim(new[] {'0'});
                        if (visibleDigitsExponential == visibleDigitsFloatingPoint)
                        {
                            return string.Concat("0", info.NumberDecimalSeparator, decimalDigits.Substring(0, maxPlaces - 1).TrimEnd(new[] { '0' }));
                        }
                        else
                        {
                            
                            StringBuilder sb = new StringBuilder();
                            sb.Append(visibleDigitsExponential);
                            if (sb.Length > 1)
                            {
                                sb.Insert(1, info.NumberDecimalSeparator);
                            }
                            else
                            {
                                sb.Append(info.NumberDecimalSeparator);
                                sb.Append('0');
                            }
                            sb.Append('E');
                            int exponent = -leadingZeros - 1;
                            sb.Append(exponent);
                            return sb.ToString();
                        }
                    }
                    else if (intDigits.Length < maxPlaces)
                    {
                        string visibleDecimalDigits = decimalDigits.Substring(0, maxPlaces - intDigits.Length).TrimEnd(new[] { '0' });
                        if (visibleDecimalDigits.Length == 0)
                            return intDigits;
                        else
                            return string.Concat(intDigits, info.NumberDecimalSeparator, visibleDecimalDigits);
                    }
                    else if (intDigits.Length == maxPlaces)
                    {
                        return intDigits;
                    }
                    else
                    {
                        int exponent = intDigits.Length - 1;
                        StringBuilder sb = new StringBuilder();
                        sb.Append(intDigits[0]);
                        sb.Append(info.NumberDecimalSeparator);
                        sb.Append(intDigits.Substring(1, maxPlaces - 1));
                        sb.Append('E');
                        sb.Append(exponent);
                        return sb.ToString();
                    }
                }

                /// <summary>
                /// Konvertiert den Wert dieses Rationals in seine Zeichenfolgendarstellung unter Angabe der maximal sichtbaren Stellen.
                /// </summary>
                /// <param name="maxPlaces">Die Anzahl an Stellen, die in der Ausgabe zu sehen sind.</param>
                /// <remarks>
                /// Überschreitet die benötigte Anzahl Stellen die maximale Anzahl Stellen, werden überstehende Nullen als Exponent dargestellt und der Rest von der Ausgabe abgeschnitten.
                /// </remarks>
                public string ToString(int maxPlaces)
                {
                    return ToString(maxPlaces, CultureInfo.CurrentCulture.NumberFormat);
                }

                /// <summary>
                /// Konvertiert den Wert dieses Rationals in seine Zeichenfolgendarstellung.
                /// </summary>
                /// <remarks>
                /// Die maximale Anzahl angezeigter Stellen beträgt 20 bei dieser Funktion.
                /// Überschreitet die benötigte Anzahl Stellen die maximale Anzahl Stellen, werden überstehende Nullen als Exponent dargestellt und der Rest von der Ausgabe abgeschnitten.
                /// </remarks>
                public override string ToString()
                {
                    return ToString(10, CultureInfo.CurrentCulture.NumberFormat);
                }
                
                /// <summary>
                /// Vergleicht diesen Rational mit dem angegebenen Object.
                /// </summary>
                /// <remarks>
                /// Die Typen, die mit einem Rational verglichen werden können, sind <see cref="System.Decimal"/>, <see cref="System.Double"/>, <see cref="System.Single"/>, <see cref="System.Int64"/>, <see cref="System.UInt64"/>, <see cref="System.Int32"/>, <see cref="System.UInt32"/>, <see cref="System.Int16"/>, <see cref="System.UInt16"/>, <see cref="System.Byte"/>, <see cref="System.SByte"/> und <see cref="System.Numerics.BigInteger"/>.
                /// </remarks>
                /// <exception cref="System.ArgumentException">Wird ausgelöst, wenn das übergebenen Objekt nicht mit einem Rational verglichen werden kann.</exception>
                public int CompareTo(object obj)
                {
                    if (obj == null) return 1;
                    if (obj is Rational || obj is decimal || obj is double || obj is float || obj is long || obj is ulong || obj is int || obj is uint || obj is short || obj is ushort || obj is byte || obj is sbyte || obj is BigInteger)
                        return this.CompareTo((Rational)obj);
                    else
                        throw new ArgumentException("Der übergebene Typ ist nicht kompatibel.");
                }

                /// <summary>
                /// Vergleicht diesen Rational mit einem anderen.
                /// </summary>
                public int CompareTo(Rational other)
                {
                    if (this < other) return -1;
                    if (this > other) return 1;
                    return 0;
                }

                /// <summary>
                /// Prüft diesen Rational mit dem angegebenen Object auf Gleichheit.
                /// </summary>
                public override bool Equals(object obj)
                {
                    if (obj is Rational || obj is decimal || obj is double || obj is float || obj is long || obj is ulong || obj is int || obj is uint || obj is short || obj is ushort || obj is byte || obj is sbyte)
                        return this.Equals((Rational)obj);
                    else
                        return false;
                }

                /// <summary>
                /// Prüft diesen Rational mit einem anderen auf Gleichheit.
                /// </summary>
                public bool Equals(Rational other)
                {
                    return this == other;
                }

                /// <summary>
                /// Gibt den Hashcode für das aktuelle Rational-Objekt zurück.
                /// </summary>
                public override int GetHashCode()
                {
                    return Numerator.GetHashCode() ^ Denominator.GetHashCode();
                }
            }
        }
    }
}
