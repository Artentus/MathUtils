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
            public struct Point3D : IVector
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
                /// Erstellt einen neuen Point3D.
                /// </summary>
                public Point3D(double x, double y, double z)
                    : this()
                {
                    X = x;
                    Y = y;
                    Z = z;
                }

                /// <summary>
                /// Erstellt einen neuen Point3D.
                /// </summary>
                public Point3D(Point3D p)
                    : this()
                {
                    X = p.X;
                    Y = p.Y;
                    Z = p.Z;
                }

                /// <summary>
                /// Konvertiert diesen Point3D in einen Vector3.
                /// </summary>
                /// <returns></returns>
                public Vector3 ToVector3()
                {
                    return new Vector3(X, Y, Z);
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

                public static Point3D operator +(Point3D left, Point3D right)
                {
                    return (Point3D)Vector.Add(left, right);
                }

                public static Point3D operator +(Point3D left, Vector3 right)
                {
                    return (Point3D)Vector.Add(left, right);
                }

                public static Point3D operator -(Point3D left, Vector3 right)
                {
                    return (Point3D)Vector.Subtract(left, right);
                }

                public static Vector3 operator -(Point3D left, Point3D right)
                {
                    var p = (Point3D)Vector.Subtract(left, right);
                    return new Vector3(p.X, p.Y, p.Z);
                }

                public static Point3D operator -(Point3D value)
                {
                    return new Point3D(-value.X, -value.Y, -value.Z);
                }
            }
        }
    }
}
