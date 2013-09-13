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
            /// Ein dreidimensionaler Vektor mit homogener Koordinate.
            /// </summary>
            public struct Vector4 : IVector
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
                /// Die homogene Koordinate.
                /// </summary>
                public double W { get; set; }

                /// <summary>
                /// Gibt 4 zurück.
                /// </summary>
                public int Dimension { get { return 4; } }

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
                            case 3:
                                return W;
                            default:
                                throw new ArgumentException("Der angegebene Index war für einen vierdimensionalen Vektor zu hoch.");
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
                            case 3:
                                W = value;
                                break;
                            default:
                                throw new ArgumentException("Der angegebene Index war für einen vierdimensionalen Vektor zu hoch.");
                        }
                    }
                }

                /// <summary>
                /// Erstellt einen neuen Vector4.
                /// </summary>
                public Vector4(double x, double y, double z, double w)
                    : this()
                {
                    X = x;
                    Y = y;
                    Z = z;
                    W = w;
                }

                /// <summary>
                /// Erstellt einen neuen Vector4.
                /// </summary>
                public Vector4(Vector4 v)
                    : this()
                {
                    X = v.X;
                    Y = v.Y;
                    Z = v.Z;
                    W = v.W;
                }

                /// <summary>
                /// Projeziert diesen dreidimensionalen Vektor in die zweidimensionale Ebene.
                /// </summary>
                public Vector3 ProjectPerspective(Size deviceSize, Vector3 viewPoint)
                {
                    var perspectiveMatrix = Matrix4x4.Projection(viewPoint);

                    var vectorInAspect = this;
                    if (deviceSize.Width > deviceSize.Height)
                        vectorInAspect.Y *= (double)deviceSize.Width / (double)deviceSize.Height;
                    else
                        vectorInAspect.X *= (double)deviceSize.Height / (double)deviceSize.Width;

                    var perspectiveVector = vectorInAspect * perspectiveMatrix;
                    perspectiveVector.X /= perspectiveVector.W;
                    perspectiveVector.Y /= perspectiveVector.W;
                    perspectiveVector.Z /= perspectiveVector.W;

                    var deviceMatrix = Matrix4x4.Scalation(deviceSize.Width / 2.0, deviceSize.Height / 2.0, 1);
                    var deviceVector = perspectiveVector * deviceMatrix;

                    return new Vector3(deviceVector.X + deviceSize.Width / 2, (deviceSize.Height / 2) - deviceVector.Y, deviceVector.Z);
                }

                /// <summary>
                /// Projeziert diesen dreidimensionalen Vektor in die zweidimensionale Ebene. Als Viewpoint wird dabei (0 0 1) verwendet.
                /// </summary>
                public Vector3 ProjectPerspective(Size deviceSize)
                {
                    return ProjectPerspective(deviceSize, new Vector3(0, 0, 1));
                }

                public IEnumerator<double> GetEnumerator()
                {
                    return new VectorEnumerator(this);
                }

                public bool Equals(IVector other)
                {
                    return Vector.CheckForEquality(this, other);
                }

                System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
                {
                    return new VectorEnumerator(this);
                }

                public static Vector4 operator +(Vector4 left, Vector4 right)
                {
                    return Vector.Add(left, right);
                }

                public static Vector4 operator -(Vector4 left, Vector4 right)
                {
                    return Vector.Subtract(left, right);
                }

                public static Vector4 operator -(Vector4 value)
                {
                    return Vector.Negate(value);
                }

                public static Vector4 operator *(Vector4 value, double skalar)
                {
                    return Vector.Multiply(value, skalar);
                }

                public static Vector4 operator *(double skalar, Vector4 value)
                {
                    return Vector.Multiply(value, skalar);
                }
            }
        }
    }
}
