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
            /// Ein dreidimensionaler Vektor.
            /// </summary>
            public struct Vector3 : IVector
            {
                /// <summary>
                /// Die X-Koordinate.
                /// </summary>
                public double X { get; set; }

                /// <summary>
                /// Die Y-Koordinate.
                /// </summary>
                public double Y { get; set; }

                /// <summary>
                /// Die Z-Koordinate.
                /// </summary>
                public double Z { get; set; }

                /// <summary>
                /// Gibt 3 zurück.
                /// </summary>
                public int Dimension { get { return 3; } }

                /// <summary>
                /// Gibt die Koordinate an dem angegebenen Index zurück oder legt diese fest.
                /// </summary>
                public double this[int index]
                {
                    get
                    {
                        switch (index)
                        {
                            case 0:
                                return X;
                            case 1:
                                return Y;
                            case 2:
                                return Z;
                            default:
                                throw new ArgumentException("Der angegebene Index war für einen dreidimensionalen Vektor zu hoch.");
                        }
                    }
                    set
                    {
                        switch (index)
                        {
                            case 0:
                                X = value;
                                break;
                            case 1:
                                Y = value;
                                break;
                            case 2:
                                Z = value;
                                break;
                            default:
                                throw new ArgumentException("Der angegebene Index war für einen dreidimensionalen Vektor zu hoch.");
                        }
                    }
                }

                /// <summary>
                /// Erstellt einen neuen Vector3.
                /// </summary>
                public Vector3(double x, double y, double z)
                    : this()
                {
                    X = x;
                    Y = y;
                    Z = z;
                }

                /// <summary>
                /// Erstellt einen neuen Vector3.
                /// </summary>
                public Vector3(Vector3 v)
                    : this()
                {
                    X = v.X;
                    Y = v.Y;
                    Z = v.Z;
                }

                /// <summary>
                /// Berechnet das Kreuzprodukt (Vektorprodukt im R3) aus zwei Vektoren.
                /// </summary>
                public static Vector3 GetCrossProduct(Vector3 left, Vector3 right)
                {
                    return new Vector3(left.Y * right.Z - left.Z * right.Y, left.Z * right.X - left.X * right.Z, left.X * right.Y - left.Y * right.X);
                }

                public IEnumerator<double> GetEnumerator()
                {
                    return new VectorEnumerator(this);
                }

                System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
                {
                    return new VectorEnumerator(this);
                }

                public bool Equals(IVector other)
                {
                    return Vector.CheckForEquality(this, other);
                }

                public static Vector3 operator +(Vector3 left, Vector3 right)
                {
                    return Vector.Add(left, right);
                }

                public static Vector3 operator -(Vector3 left, Vector3 right)
                {
                    return Vector.Subtract(left, right);
                }

                public static Vector3 operator -(Vector3 value)
                {
                    return Vector.Negate(value);
                }

                public static Vector3 operator *(Vector3 value, double skalar)
                {
                    return Vector.Multiply(value, skalar);
                }

                public static Vector3 operator *(double skalar, Vector3 value)
                {
                    return Vector.Multiply(value, skalar);
                }
            }
        }
    }
}
