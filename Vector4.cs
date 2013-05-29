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
            /// Ein dreidimensionaler Vektor mit homogener Koordinate.
            /// </summary>
            public class Vector4 : Vector
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

                protected override double[] GetCoordinates()
                {
                    double[] coords = { X, Y, Z, W };
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
                        case 3:
                            W = value;
                            break;
                    }
                }

                /// <summary>
                /// Erstellt einen neuen Vector4.
                /// </summary>
                public Vector4()
                {
                    X = 0.0;
                    Y = 0.0;
                    Z = 0.0;
                    W = 0.0;
                }

                /// <summary>
                /// Erstellt einen neuen Vector4.
                /// </summary>
                /// <param name="x"></param>
                /// <param name="y"></param>
                /// <param name="z"></param>
                /// <param name="w"></param>
                public Vector4(double x, double y, double z, double w)
                {
                    X = x;
                    Y = y;
                    Z = z;
                    W = w;
                }

                /// <summary>
                /// Erstellt einen neuen Vector4.
                /// </summary>
                /// <param name="v"></param>
                public Vector4(Vector4 v)
                {
                    X = v.X;
                    Y = v.Y;
                    Z = v.Z;
                    W = v.W;
                }

                public override object Clone()
                {
                    return new Vector4(this);
                }

                public static Vector4 operator +(Vector4 left, Vector4 right)
                {
                    return (Vector4)Add(left, right);
                }

                public static Vector4 operator -(Vector4 left, Vector4 right)
                {
                    return (Vector4)Subtract(left, right);
                }

                public static Vector4 operator -(Vector4 value)
                {
                    return new Vector4(-value.X, -value.Y, -value.Z, -value.W);
                }

                public static Vector4 operator *(Vector4 left, Vector4 right)
                {
                    return (Vector4)Multiplicate(left, right);
                }

                public static Vector4 operator *(Vector4 value, double skalar)
                {
                    return (Vector4)Multiplicate(value, skalar);
                }

                public static Vector4 operator *(double skalar, Vector4 value)
                {
                    return (Vector4)Multiplicate(value, skalar);
                }
            }
        }
    }
}
