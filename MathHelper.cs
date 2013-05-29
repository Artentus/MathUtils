using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

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
                public static double GetGradient(PointD p1, PointD p2)
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
                /// Berechnet den Arcustangens.
                /// Hierbei werden im Vergleich zu Math.Atan keine negativen Winkel ausgegeben.
                /// </summary>
                /// <param name="gradient"></param>
                /// <returns></returns>
                public static double Atan(double gradient)
                {
                    //Winkel über Arcustangens berechnen
                    var at = System.Math.Atan(gradient);

                    //negative Winkel von Atan bei gradient < 0 umrechnen
                    if (gradient < 0)
                        at = System.Math.PI + at;

                    return at;
                }

                /// <summary>
                /// Berechnet den Arcustangens aus der Steigung zwischen zwei Punkten.
                /// Hierbei werden im Vergleich zu Math.Atan keine negativen Winkel ausgegeben.
                /// </summary>
                /// <param name="p1"></param>
                /// <param name="p2"></param>
                /// <returns></returns>
                public static double Atan(PointD p1, PointD p2)
                {
                    var gradient = GetGradient(p1, p2);
                    return Atan(gradient);
                }

                /// <summary>
                /// Berechnet den Arcustangens.
                /// Hierbei werden im Vergleich zu Math.Atan keine negativen Winkel ausgegeben.
                /// </summary>
                /// <param name="gradient"></param>
                /// <returns></returns>
                public static float Atan(float gradient)
                {
                    return (float)Atan((double)gradient);
                }

                /// <summary>
                /// Berechnet den Arcustangens aus der Steigung zwischen zwei Punkten.
                /// Hierbei werden im Vergleich zu Math.Atan keine negativen Winkel ausgegeben.
                /// </summary>
                /// <param name="p1"></param>
                /// <param name="p2"></param>
                /// <returns></returns>
                public static float Atan(PointF p1, PointF p2)
                {
                    var gradient = GetGradient(p1, p2);
                    return Atan(gradient);
                }

                /// <summary>
                /// Berechnet der Winkel einer Geraden, die durch zwei Punkte beschrieben wird, zur X-Achse.
                /// Der Wertebereich liegt hierbei zwischen 0 und 2Pi (bei Atan nur zwischen 0 ind Pi).
                /// </summary>
                /// <param name="p1"></param>
                /// <param name="p2"></param>
                /// <returns></returns>
                public static double GetAngle(PointD p1, PointD p2)
                {
                    //"echten" Winkel berechnen
                    var at = Atan(p1, p2);

                    //im 3. und 4. Quadranten Pi aufaddieren
                    if ((p1.Y > p2.Y) || (p1.Y == p2.Y && p1.X > p2.X))
                        at += System.Math.PI;

                    return at;
                }

                /// <summary>
                /// Berechnet der Winkel einer Geraden, die durch zwei Punkte beschrieben wird, zur X-Achse.
                /// Der Wertebereich liegt hierbei zwischen 0 und 2Pi (bei Atan nur zwischen 0 ind Pi).
                /// </summary>
                /// <param name="p1"></param>
                /// <param name="p2"></param>
                /// <returns></returns>
                public static float GetAngle(PointF p1, PointF p2)
                {
                    return (float)GetAngle(p1.ToPointD(), p2.ToPointD());
                }

                /// <summary>
                /// Berechnet die Position eines Punktes, der auf einem Kreis liegt.
                /// </summary>
                /// <param name="center">Der Mittelpunkt des Kreises.</param>
                /// <param name="angle">Der Winkel, in dem der Punkt zur X-Achse liegt.</param>
                /// <param name="radius">Der Radius des Kreises.</param>
                /// <returns></returns>
                public static PointD GetPointOnCircle(PointD center, double angle, double radius)
                {
                    //Winkelfunktionen anwenden
                    var sin = System.Math.Sin(angle);
                    var cos = System.Math.Cos(angle);

                    //Koordinaten berechnen
                    var x = center.X + cos * radius;
                    var y = center.X + sin * radius;

                    return new PointD(x, y);
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
                    return GetPointOnCircle(center, (double)angle, (double)radius).ToPointF();
                }

                /// <summary>
                /// Projeziert einen dreidimensionalen Punkt in die zweite Dimension.
                /// </summary>
                /// <param name="point"></param>
                /// <param name="viewPosition"></param>
                /// <returns></returns>
                public static Vector3 GetPerspectiveProjection(Vector4 point, Vector3 viewPosition)
                {
                    var transformationMatrix = Matrix4x4.GetIdentity();
                    transformationMatrix[3, 3] = 0;
                    transformationMatrix[2, 0] = -(viewPosition.X / viewPosition.Z);
                    transformationMatrix[2, 1] = -(viewPosition.Y / viewPosition.Z);
                    transformationMatrix[2, 3] = 1.0 / viewPosition.Z;
                    var v = point * transformationMatrix;
                    return new Vector3(v.X / v.W, v.Y / v.W, v.Z);
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
            }
        }
    }
}
