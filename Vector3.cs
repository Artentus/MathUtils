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
            public class Vector3 : Vector
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

                protected override double[] GetCoordinates()
                {
                    double[] coords = { X, Y, Z };
                    return coords;
                }

                protected override void SetCoordinate(int index, double value)
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
                    }
                }

                /// <summary>
                /// Erstellt einen neuen Vector3.
                /// </summary>
                public Vector3()
                {
                    X = 0.0;
                    Y = 0.0;
                    Z = 0.0;
                }

                /// <summary>
                /// Erstellt einen neuen Vector3.
                /// </summary>
                /// <param name="x"></param>
                /// <param name="y"></param>
                /// <param name="z"></param>
                public Vector3(double x, double y, double z)
                {
                    X = x;
                    Y = y;
                    Z = z;
                }

                /// <summary>
                /// Erstellt einen neuen Vector3.
                /// </summary>
                /// <param name="v"></param>
                public Vector3(Vector3 v)
                {
                    X = v.X;
                    Y = v.Y;
                    Z = v.Z;
                }

                /// <summary>
                /// Berechnet das Kreuzprodukt (Vektorprodukt im R3) aus zwei Vektoren.
                /// </summary>
                /// <param name="left"></param>
                /// <param name="right"></param>
                /// <returns></returns>
                public static Vector3 GetCrossProduct(Vector3 left, Vector3 right)
                {
                    return new Vector3(left.Y * right.Z - left.Z * right.Y, left.Z * right.X - left.X * right.Z, left.X * right.Y - left.Y * right.X);
                }

                public override object Clone()
                {
                    return new Vector3(this);
                }

                public static Vector3 operator +(Vector3 left, Vector3 right)
                {
                    return (Vector3)Add(left, right);
                }

                public static Vector3 operator -(Vector3 left, Vector3 right)
                {
                    return (Vector3)Subtract(left, right);
                }

                public static Vector3 operator -(Vector3 value)
                {
                    return new Vector3(-value.X, -value.Y, -value.Z);
                }

                public static Vector3 operator *(Vector3 left, Vector3 right)
                {
                    return (Vector3)Multiplicate(left, right);
                }

                public static Vector3 operator *(Vector3 value, double skalar)
                {
                    return (Vector3)Multiplicate(value, skalar);
                }

                public static Vector3 operator *(double skalar, Vector3 value)
                {
                    return (Vector3)Multiplicate(value, skalar);
                }
            }
        }
    }
}
