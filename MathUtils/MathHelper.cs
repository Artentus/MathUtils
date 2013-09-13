using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Numerics;

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
                public static bool IsInt(double value)
                {
                    return value == (long)value;
                }

                /// <summary>
                /// Prüft, ob eine Gleitkommazahl einen ganzzahligen Wert besitzt.
                /// </summary>
                public static bool IsInt(float value)
                {
                    return value == (long)value;
                }

                /// <summary>
                /// Prüft, ob eine Gleitkommazahl einen ganzzahligen Wert besitzt.
                /// </summary>
                public static bool IsInt(decimal value)
                {
                    return value == (long)value;
                }

                /// <summary>
                /// Berechnet die n-te Wurzel einer Zahl.
                /// </summary>
                public static double NthRoot(double radicant, double degree)
                {
                    return System.Math.Pow(radicant, 1 / degree);
                }

                /// <summary>
                /// Potenziert einen BigInteger mit einem anderen.
                /// </summary>
                /// <remarks>
                /// Es werden keine negativen Exponenten zugelassen.
                /// </remarks>
                public static BigInteger BigIntPow(BigInteger @base, BigInteger exponent)
                {
                    if (exponent < 0) throw new ArgumentException("Negative Exponenten sind nicht erlaubt.");
                    BigInteger result = 1;
                    while (exponent > 0)
                    {
                        if ((exponent & 1) != 0)
                            result *= @base;
                        exponent >>= 1;
                        @base *= @base;
                    }
                    return result;
                }
            }
        }
    }
}
