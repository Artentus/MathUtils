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
            /// Ein zweidimensionaler Vektor.
            /// </summary>
            public struct Vector2 : IVector
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
                /// Gibt 2 zurück.
                /// </summary>
                public int Dimension { get { return 2; } }

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
                            default:
                                throw new ArgumentException("Der angegebene Index war für einen zweidimensionalen Vektor zu hoch.");
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
                            default:
                                throw new ArgumentException("Der angegebene Index war für einen zweidimensionalen Vektor zu hoch.");
                        }
                    }
                }

                /// <summary>
                /// Erstellt einen neuen Vector2.
                /// </summary>
                public Vector2(double x, double y)
                    : this()
                {
                    X = x;
                    Y = y;
                }

                /// <summary>
                /// Erstellt einen neuen Vector2.
                /// </summary>
                public Vector2(Vector2 v)
                    : this()
                {
                    X = v.X;
                    Y = v.Y;
                }

                /// <summary>
                /// Gibt das Kreuzprodukt dieses Vektors zurück.
                /// </summary>
                public Vector2 CrossProduct
                {
                    get
                    {
                        return new Vector2(-Y, X);
                    }
                }

                /// <summary>
                /// Berechnet das Vektorprodukt aus zwei Vektoren.
                /// </summary>
                public static double GetVectorProduct(Vector2 left, Vector2 right)
                {
                    return left.X * right.Y - left.Y * right.X;
                }

                /// <summary>
                /// Berechnet den Winkel zwischen zwei Vektoren.
                /// </summary>
                public static double GetAngle(Vector2 v1, Vector2 v2)
                {
                    return System.Math.Atan2(v1.Y, v1.X) - System.Math.Atan2(v2.Y, v2.X);
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

                public static Vector2 operator +(Vector2 left, Vector2 right)
                {
                    return Vector.Add(left, right);
                }

                public static Vector2 operator -(Vector2 left, Vector2 right)
                {
                    return Vector.Subtract(left, right);
                }

                public static Vector2 operator -(Vector2 value)
                {
                    return Vector.Negate(value);
                }

                public static Vector2 operator *(Vector2 value, double skalar)
                {
                    return Vector.Multiply(value, skalar);
                }

                public static Vector2 operator *(double skalar, Vector2 value)
                {
                    return Vector.Multiply(value, skalar);
                }
            }
        }
    }
}
