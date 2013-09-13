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
            public struct Point2D : IVector
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
                /// Erstellt einen neuen Point2D.
                /// </summary>
                public Point2D(double x, double y)
                    : this()
                {
                    X = x;
                    Y = y;
                }

                /// <summary>
                /// Erstellt einen neuen Point2D.
                /// </summary>
                public Point2D(Point2D p)
                    : this()
                {
                    X = p.X;
                    Y = p.Y;
                }

                /// <summary>
                /// Erstellt einen neuen Point2D.
                /// </summary>
                public Point2D(Point p)
                    : this()
                {
                    X = p.X;
                    Y = p.Y;
                }

                /// <summary>
                /// Erstellt einen neuen Point2D.
                /// </summary>
                public Point2D(PointF p)
                    : this()
                {
                    X = p.X;
                    Y = p.Y;
                }

                /// <summary>
                /// Gibt das Kreuzprodukt dieses Vektors zurück.
                /// </summary>
                public Point2D CrossProduct
                {
                    get
                    {
                        return new Point2D(-Y, X);
                    }
                }

                /// <summary>
                /// Konvertiert diesen Point2D zu einem Point.
                /// </summary>
                public Point ToPoint()
                {
                    return new Point((int)X, (int)Y);
                }

                /// <summary>
                /// Konvertiert diesen Point2D zu einem PointF.
                /// </summary>
                public PointF ToPointF()
                {
                    return new PointF((float)X, (float)Y);
                }

                /// <summary>
                /// Berechnet das Vektorprodukt aus zwei Vektoren.
                /// </summary>
                public static double GetVectorProduct(Point2D left, Point2D right)
                {
                    return left.X * right.Y - left.Y * right.X;
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

                public static Point2D operator +(Point2D left, Point2D right)
                {
                    return Vector.Add(left, right);
                }

                public static Point2D operator +(Point2D left, Vector2 right)
                {
                    return Vector.Add(left, right.As<Point2D>());
                }

                public static Point2D operator -(Point2D left, Vector2 right)
                {
                    return Vector.Subtract(left, right.As<Point2D>());
                }

                public static Vector2 operator -(Point2D left, Point2D right)
                {
                    return Vector.Subtract(left, right).As<Vector2>();
                }

                public static Point2D operator -(Point2D value)
                {
                    return Vector.Negate(value);
                }
            }
        }
    }
}
