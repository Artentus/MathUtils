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
                /// Listet alle Koordinaten dieses Vektors auf.
                /// </summary>
                /// <returns></returns>
                public static IEnumerable<double> Coordinates(this IVector source)
                {
                    //alle Koordinaten ermittlen
                    for (int i = 0; i < source.Dimension; i++)
                        yield return source[i];
                }

                /// <summary>
                /// Berechnet die euklidische Länge dieses Vektors.
                /// </summary>
                public static double Length(this IVector source)
                {             
                    return System.Math.Sqrt(source.Coordinates().Sum(val => val * val)); //Phytagoras
                }

                /// <summary>
                /// Berechnet die Entfernung von diesm Vektor zu einem anderen.
                /// </summary>
                /// <returns></returns>
                public static double DistanceTo(this IVector first, IVector second)
                {
                    //Dimension zur Berechnug bestimmen und Array in richtiger Größe anlegen
                    var maxDimension = System.Math.Max(first.Dimension, second.Dimension);
                    var values = new double[maxDimension];

                    //Werte aller Dimensionen berechnen
                    for (int i = 0; i < maxDimension; i++)
                    {
                        var value = 0.0;

                        if (first.Dimension > i)
                            value = first[i];

                        if (second.Dimension > i)
                            value -= second[i];

                        values[i] = value;
                    }

                    //euklidische Länge des entstandenen Vektors berechnen
                    return System.Math.Sqrt(values.Sum(val => val * val));
                }

                /// <summary>
                /// Stellt diesen Vektor als Matrix dar, wobei die Koordinaten in einer Zeile angeordnet werden.
                /// </summary>
                /// <returns></returns>
                public static Matrix ToHorizontalMatrix(this IVector source)
                {
                    var m = new Matrix(source.Dimension, 1); //Matrix erstellen

                    for (int i = 0; i < source.Dimension; i++)
                        m[i, 0] = source[i]; //Koordinate der Matrix zuweisen

                    return m;
                }

                /// <summary>
                /// Stellt diesen Vektor als Matrix dar, wobei die Koordinaten in einer Spalte angeordnet werden.
                /// </summary>
                /// <returns></returns>
                public static Matrix ToVerticalMatrix(this IVector source)
                {
                    var m = new Matrix(1, source.Dimension); //Matrix erstellen

                    for (int i = 0; i < source.Dimension; i++)
                        m[0, i] = source[i]; //Koordinate der Matrix zuweisen

                    return m;
                }

                /// <summary>
                /// Normalisiert diesen Vektor.
                /// </summary>
                public static IVector Normalize(this IVector source)
                {
                    var f = 1.0 / source.Length(); //Skalarfaktor

                    for (int i = 0; i < source.Dimension; i++)
                        source[i] *= f; //Koordinate mit Faktor multiplizieren

                    return source;
                }

                /// <summary>
                /// Multipliziert zwei Vektoren miteinander.
                /// </summary>
                /// <returns></returns>
                public static IVector Multiply(IVector left, IVector right)
                {
                    IVector v;

                    //neuen Vektor mit größter Dimension erzeugen
                    if (left.Dimension >= right.Dimension)
                        v = left;
                    else
                        v = right;

                    //Dimensionen multiplizieren
                    for (int i = 0; i < v.Dimension; i++)
                    {
                        if (left.Dimension > i && right.Dimension > i)
                            v[i] = left[i] * right[i];
                        else //mindestens eine Dimension 0
                            v[i] = 0.0;
                    }

                    return v;
                }

                /// <summary>
                /// Multipliziert einen Vektor mit einem Skalarfaktor.
                /// </summary>
                /// <returns></returns>
                public static IVector Multiply(this IVector value, double skalar)
                {
                    //in allen Dimensionen multiplizieren
                    for (int i = 0; i < value.Dimension; i++)
                        value[i] *= skalar;

                    return value;
                }

                /// <summary>
                /// Addiert zwei Vektoren miteinander.
                /// </summary>
                /// <returns></returns>
                public static IVector Add(IVector left, IVector right)
                {
                    IVector v;

                    //neuen Vektor mit größter Dimension erzeugen
                    if (left.Dimension >= right.Dimension)
                        v = left;
                    else
                        v = right;

                    //alle Dimensionen addieren
                    for (int i = 0; i < v.Dimension; i++)
                    {
                        if (left.Dimension > i)
                            v[i] = left[i];
                        else
                            v[i] = 0;

                        if (right.Dimension > i)
                            v[i] += right[i];
                    }

                    return v;
                }

                /// <summary>
                /// Subtrahiert zwei Vektoren voneinander.
                /// </summary>
                /// <returns></returns>
                public static IVector Subtract(IVector left, IVector right)
                {
                    IVector v;

                    //neuen Vektor mit größter Dimension erzeugen
                    if (left.Dimension >= right.Dimension)
                        v = left;
                    else
                        v = right;

                    //Dimensionen subtrahieren
                    for (int i = 0; i < v.Dimension; i++)
                    {
                        if (left.Dimension > i)
                            v[i] = left[i];
                        else
                            v[i] = 0;

                        if (right.Dimension > i)
                            v[i] -= right[i];
                    }

                    return v;
                }

                /// <summary>
                /// Berechnet das Skalarprodukt aus zwei Vektoren.
                /// </summary>
                /// <returns></returns>
                public static double GetScalarProduct(IVector left, IVector right)
                {
                    var v = Vector.Multiply(left, right); //Vektoren multiplizieren
                    var val = 0.0;

                    //Koordinaten zusammenaddieren
                    for (int i = 0; i < v.Dimension; i++)
                        val += v[i];

                    return val;
                }

                /// <summary>
                /// Prüft, ob zwei Vektoren gleich sind.
                /// </summary>
                /// <param name="left"></param>
                /// <param name="right"></param>
                /// <returns></returns>
                public static bool CheckForEquality(IVector left, IVector right)
                {
                    var coords1 = new List<double>(left.Coordinates());
                    var coords2 = new List<double>(right.Coordinates());

                    //Dimensionszahl angleichen
                    var maxDimension = System.Math.Max(coords1.Count, coords2.Count);
                    while (coords1.Count < maxDimension)
                        coords1.Add(0);
                    while (coords2.Count < maxDimension)
                        coords2.Add(0);

                    //alle Koordinaten vergleichen
                    for (int i = 0; i < maxDimension; i++)
                        if (coords1[i] != coords2[i])
                            return false;

                    return true;
                }
            }
        }
    }
}
