using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Artentus
{
    namespace Utils
    {
        namespace Math
        {
            /// <summary>
            /// Stellt Grundfunktionalität für Vektoren beliebiger Dimension bereit.
            /// </summary>
            public abstract class Vector : IEnumerable<double>, ICloneable, IEquatable<Vector>
            {
                protected abstract double[] GetCoordinates();

                protected abstract void SetCoordinate(int index, double value);

                /// <summary>
                /// Gibt die Koordinate mit dem angegebenen Index zurück oder legt diese fest.
                /// </summary>
                /// <param name="index"></param>
                /// <returns></returns>
                public double this[int index]
                {
                    get
                    {
                        return GetCoordinates()[index];
                    }
                    set
                    {
                        if (index < Dimension)
                            SetCoordinate(index, value);
                        else //Index zu hoch
                            throw new ArgumentException("Der angegebene Index war zu hoch für diesen Vektor. Er muss kleiner sein, als die Dimension des Vektors.");
                    }
                }

                /// <summary>
                /// Berechnet die euklidische Länge dieses Vektors.
                /// </summary>
                public double Length
                {
                    get
                    {
                        var coords = GetCoordinates();
                        return System.Math.Sqrt(coords.Sum(val => val * val)); //Phytagoras
                    }
                }

                /// <summary>
                /// Gibt die Dimension dieses Vektors an.
                /// </summary>
                public int Dimension
                {
                    get
                    {
                        return GetCoordinates().Length;
                    }
                }

                /// <summary>
                /// Berechnet die Entfernung von diesm Vektor zu einem anderen.
                /// Wenn die Vektoren verschiedene Dimensionen besitzen, wird die höhere der beiden zur Berechnung verwendet.
                /// </summary>
                /// <param name="v"></param>
                /// <returns></returns>
                public double GetDistanceTo(Vector v)
                {
                    //Dimension zur Berechnug bestimmen und Array in richtiger Größe anlegen
                    var maxDimension = System.Math.Max(Dimension, v.Dimension);
                    var values = new double[maxDimension];

                    //Werte aller Dimensionen berechnen
                    for (int i = 0; i < maxDimension; i++)
                    {
                        var value = 0.0;

                        if (Dimension > i)
                            value = this[i];

                        if (v.Dimension > i)
                            value -= v[i];

                        values[i] = value;
                    }

                    //euklidische Länge des entstandenen Vektors berechnen
                    return System.Math.Sqrt(values.Sum(val => val * val));
                }

                /// <summary>
                /// Stellt diesen Vektor als Matrix dar, wobei die Koordinaten in einer Zeile angeordnet werden.
                /// </summary>
                /// <returns></returns>
                public Matrix ToHorizontalMatrix()
                {
                    var m = new Matrix(Dimension, 1); //Matrix erstellen

                    for (int i = 0; i < Dimension; i++)
                        m[i, 0] = this[i]; //Koordinate der Matrix zuweisen

                    return m;
                }

                /// <summary>
                /// Stellt diesen Vektor als Matrix dar, wobei die Koordinaten in einer Spalte angeordnet werden.
                /// </summary>
                /// <returns></returns>
                public Matrix ToVerticalMatrix()
                {
                    var m = new Matrix(1, Dimension); //Matrix erstellen

                    for (int i = 0; i < Dimension; i++)
                        m[0, i] = this[i]; //Koordinate der Matrix zuweisen

                    return m;
                }

                /// <summary>
                /// Normaliziert diesen Vektor.
                /// </summary>
                public void Normalize()
                {
                    var f = 1.0 / Length; //Skalarfaktor
                    var coords = GetCoordinates();

                    for (int i = 0; i < coords.Length; i++)
                        SetCoordinate(i, coords[i] * f); //Koordinate mit Faktor multiplizieren
                }

                public IEnumerator<double> GetEnumerator()
                {
                    return new VectorEnumerator(this);
                }

                System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
                {
                    return new VectorEnumerator(this);
                }

                internal class VectorEnumerator : IEnumerator<double>
                {
                    private Vector v;
                    private int index;

                    internal VectorEnumerator(Vector v)
                    {
                        this.v = v;
                        index = -1;
                    }

                    public double Current
                    {
                        get
                        {
                            return v[index];
                        }
                    }

                    public void Dispose() { }

                    object System.Collections.IEnumerator.Current
                    {
                        get 
                        {
                            return v[index];
                        }
                    }

                    public bool MoveNext()
                    {
                        index++;
                        return index < v.Dimension;
                    }

                    public void Reset()
                    {
                        index = -1;
                    }
                }

                /// <summary>
                /// Addiert zwei Vektoren miteinander.
                /// </summary>
                /// <param name="left"></param>
                /// <param name="right"></param>
                /// <returns></returns>
                public static Vector Add(Vector left, Vector right)
                {
                    Vector v;

                    //neuen Vektor mit größter Dimension erzeugen
                    if (left.Dimension >= right.Dimension)
                        v = (Vector)left.Clone();
                    else
                        v = (Vector)right.Clone();

                    //alle Dimensionen addieren
                    for (int i = 0; i < v.Dimension; i++)
                    {
                        v[i] = 0.0;

                        if (left.Dimension > i)
                            v[i] += left[i];

                        if (right.Dimension > i)
                            v[i] += right[i];
                    }

                    return v;
                }

                /// <summary>
                /// Subtrahiert zwei Vektoren voneinander.
                /// </summary>
                /// <param name="left"></param>
                /// <param name="right"></param>
                /// <returns></returns>
                public static Vector Subtract(Vector left, Vector right)
                {
                    Vector v;

                    //neuen Vektor mit größter Dimension erzeugen
                    if (left.Dimension >= right.Dimension)
                        v = (Vector)left.Clone();
                    else
                        v = (Vector)right.Clone();

                    //Dimensionen subtrahieren
                    for (int i = 0; i < v.Dimension; i++)
                    {
                        v[i] = 0.0;

                        if (left.Dimension > i)
                            v[i] += left[i];

                        if (right.Dimension > i)
                            v[i] -= right[i];
                    }

                    return v;
                }

                /// <summary>
                /// Multipliziert zwei Vektoren miteinander.
                /// </summary>
                /// <param name="left"></param>
                /// <param name="right"></param>
                /// <returns></returns>
                public static Vector Multiplicate(Vector left, Vector right)
                {
                    Vector v;

                    //neuen Vektor mit größter Dimension erzeugen
                    if (left.Dimension >= right.Dimension)
                        v = (Vector)left.Clone();
                    else
                        v = (Vector)right.Clone();

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
                /// <param name="value"></param>
                /// <param name="skalar"></param>
                /// <returns></returns>
                public static Vector Multiplicate(Vector value, double skalar)
                {
                    var v = (Vector)value.Clone(); //Vektor klonen
                    var coords = value.GetCoordinates();

                    //in allen Dimensionen multiplizieren
                    for (int i = 0; i < value.Dimension; i++)
                        v[i] = coords[i] * skalar;

                    return v;
                }

                /// <summary>
                /// Berechnet das Skalarprodukt aus zwei Vektoren.
                /// </summary>
                /// <param name="left"></param>
                /// <param name="right"></param>
                /// <returns></returns>
                public static double GetSkalarProduct(Vector left, Vector right)
                {
                    var v = left * right; //Vektoren multiplizieren
                    var val = 0.0;

                    //Koordinaten zusammenaddieren
                    for (int i = 0; i < v.Dimension; i++)
                        val += v[i];

                    return val;
                }

                public static Vector operator +(Vector left, Vector right)
                {
                    return Add(left, right);
                }

                public static Vector operator -(Vector left, Vector right)
                {
                    return Subtract(left, right);
                }

                public static Vector operator *(Vector left, Vector right)
                {
                    return Multiplicate(left, right);
                }

                public static Vector operator *(Vector value, double skalar)
                {
                    return Multiplicate(value, skalar);
                }

                public static Vector operator *(double skalar, Vector value)
                {
                    return Multiplicate(value, skalar);
                }

                public abstract object Clone();

                public bool Equals(Vector other)
                {
                    //höchste Dimension ermitteln
                    var maxDimension = System.Math.Max(Dimension, other.Dimension);

                    //alle Dimensionen vergleichen
                    for (int i = 0; i < maxDimension; i++)
                    {
                        var val1 = 0.0;
                        var val2 = 0.0;

                        if (i < Dimension)
                            val1 = this[i];

                        if (i < other.Dimension)
                            val2 = other[i];

                        if (val1 != val2)
                            return false; //Dimensionen unterscheiden sich
                    }

                    //keine Dimension unterschiedlich
                    return true;
                }

                public override string ToString()
                {
                    return string.Concat(new object[] { '{', string.Join(", ", GetCoordinates().Select(val => val.ToString()).ToArray()), '}' });
                }
            }
        }
    }
}
