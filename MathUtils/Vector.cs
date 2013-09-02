using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace Artentus
{
    namespace Utils
    {
        namespace Math
        {
            /// <summary>
            /// Stellt Funktionen zum Arbeiten mit Vektoren bereit.
            /// </summary>
            public static class Vector
            {
                /// <summary>
                /// Berechnet die euklidische Länge dieses Vektors.
                /// </summary>
                public static double Length(this IVector source)
                {             
                    return System.Math.Sqrt(source.Sum(val => val * val));
                }

                /// <summary>
                /// Berechnet die Entfernung von diesem Vektor zu einem anderen.
                /// </summary>
                public static double DistanceTo<T>(this T first, T second) where T : IVector, new()
                {
                    return Vector.Subtract(first, second).Length();
                }

                /// <summary>
                /// Stellt diesen Vektor als Matrix dar, wobei die Koordinaten in einer Zeile angeordnet werden.
                /// </summary>
                public static Matrix ToHorizontalMatrix(this IVector source)
                {
                    var m = new Matrix(source.Dimension, 1);
                    for (int i = 0; i < source.Dimension; i++)
                        m[i, 0] = source[i];
                    return m;
                }

                /// <summary>
                /// Stellt diesen Vektor als Matrix dar, wobei die Koordinaten in einer Spalte angeordnet werden.
                /// </summary>
                public static Matrix ToVerticalMatrix(this IVector source)
                {
                    var m = new Matrix(1, source.Dimension);
                    for (int i = 0; i < source.Dimension; i++)
                        m[0, i] = source[i];
                    return m;
                }

                /// <summary>
                /// Normalisiert diesen Vektor.
                /// </summary>
                public static T Normalize<T>(this T source) where T : IVector
                {
                    return source.Multiply(1 / source.Length());
                }

                /// <summary>
                /// Multipliziert zwei Vektoren miteinander.
                /// </summary>
                public static T Multiply<T>(T left, T right) where T : IVector, new()
                {
                    var result = new T();
                    for (int i = 0; i < left.Dimension; i++)
                        result[i] = left[i] * right[i];
                    return result;
                }

                /// <summary>
                /// Multipliziert einen Vektor mit einem Skalarfaktor.
                /// </summary>
                public static T Multiply<T>(this T value, double skalar) where T : IVector
                {
                    for (int i = 0; i < value.Dimension; i++)
                        value[i] *= skalar;
                    return value;
                }

                /// <summary>
                /// Addiert zwei Vektoren miteinander.
                /// </summary>
                public static T Add<T>(T left, T right) where T : IVector, new()
                {
                    var result = new T();
                    for (int i = 0; i < left.Dimension; i++)
                        result[i] = left[i] + right[i];
                    return result;
                }

                /// <summary>
                /// Subtrahiert zwei Vektoren voneinander.
                /// </summary>
                public static T Subtract<T>(T left, T right) where T : IVector, new()
                {
                    var result = new T();
                    for (int i = 0; i < left.Dimension; i++)
                        result[i] = left[i] - right[i];
                    return result;
                }

                /// <summary>
                /// Berechnet das Skalarprodukt aus zwei Vektoren.
                /// </summary>
                public static double GetScalarProduct<T>(T left, T right) where T : IVector, new()
                {
                    return Vector.Multiply(left, right).Sum();
                }

                /// <summary>
                /// Prüft, ob zwei Vektoren gleich sind.
                /// </summary>
                public static bool CheckForEquality(IVector left, IVector right)
                {
                    var maxDimension = System.Math.Max(left.Dimension, right.Dimension);
                    for (int i = 0; i < maxDimension; i++)
                    {
                        if ((i < left.Dimension ? left[i] : 0) != (i < right.Dimension ? right[i] : 0))
                            return false;
                    }
                    return true;
                }

                /// <summary>
                /// Konvertiert diesen Vektor in einen anderen.
                /// </summary>
                public static T As<T>(this IVector value) where T : IVector, new()
                {
                    var result = new T();
                    for (int i = 0; i < value.Dimension; i++)
                    {
                        if (i >= result.Dimension)
                            break;
                        result[i] = value[i];
                    }
                    return result;
                }
            }
        }
    }
}
