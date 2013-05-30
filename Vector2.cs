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
                /// <param name="index"></param>
                /// <returns></returns>
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
                /// <param name="x"></param>
                /// <param name="y"></param>
                public Vector2(double x, double y)
                    : this()
                {
                    X = x;
                    Y = y;
                }

                /// <summary>
                /// Erstellt einen neuen Vector2.
                /// </summary>
                /// <param name="p"></param>
                public Vector2(PointD p)
                    : this()
                {
                    X = p.X;
                    Y = p.Y;
                }

                /// <summary>
                /// Erstellt einen neuen Vector2.
                /// </summary>
                /// <param name="s"></param>
                public Vector2(SizeD s)
                    : this()
                {
                    X = s.Width;
                    Y = s.Height;
                }

                /// <summary>
                /// Erstellt einen neuen Vector2.
                /// </summary>
                /// <param name="v"></param>
                public Vector2(Vector2 v)
                    : this()
                {
                    X = v.X;
                    Y = v.Y;
                }

                /// <summary>
                /// Berechnet das Vektorprodukt aus zwei Vektoren.
                /// </summary>
                /// <param name="left"></param>
                /// <param name="right"></param>
                /// <returns></returns>
                public double GetVectorProduct(Vector2 left, Vector2 right)
                {
                    return left.X * right.Y - left.Y * right.X;
                }

                /// <summary>
                /// Konvertiert diesen Vector2 in einen PointD.
                /// </summary>
                /// <returns></returns>
                public PointD ToPointD()
                {
                    return new PointD(X, Y);
                }

                /// <summary>
                /// Konvertiert diesen Vector2 in eine SizeD.
                /// </summary>
                /// <returns></returns>
                public SizeD ToSizeD()
                {
                    return new SizeD(X, Y);
                }

                public IEnumerator<double> GetEnumerator()
                {
                    return new VectorEnumerator(this);
                }

                System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
                {
                    return new VectorEnumerator(this);
                }

                public static implicit operator Vector2(Point value)
                {
                    return new Vector2(value);
                }

                public static implicit operator Vector2(PointF value)
                {
                    return new Vector2(value);
                }

                public static implicit operator Vector2(PointD value)
                {
                    return new Vector2(value);
                }

                public static implicit operator Vector2(Size value)
                {
                    return new Vector2(value);
                }

                public static implicit operator Vector2(SizeF value)
                {
                    return new Vector2(value);
                }

                public static implicit operator Vector2(SizeD value)
                {
                    return new Vector2(value);
                }

                public static Vector2 operator +(Vector2 left, Vector2 right)
                {
                    return (Vector2)Vector.Add(left, right);
                }

                public static Vector2 operator -(Vector2 left, Vector2 right)
                {
                    return (Vector2)Vector.Subtract(left, right);
                }

                public static Vector2 operator -(Vector2 value)
                {
                    return new Vector2(-value.X, -value.Y);
                }

                public static Vector2 operator *(Vector2 left, Vector2 right)
                {
                    return (Vector2)Vector.Multiply(left, right);
                }

                public static Vector2 operator *(Vector2 value, double skalar)
                {
                    return (Vector2)Vector.Multiply(value, skalar);
                }

                public static Vector2 operator *(double skalar, Vector2 value)
                {
                    return (Vector2)Vector.Multiply(value, skalar);
                }
            }
        }
    }
}
