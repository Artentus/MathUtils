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
            public class Vector2 : Vector
            {
                /// <summary>
                /// Die X-Koordinate.
                /// </summary>
                public double X { get; set; }

                /// <summary>
                /// Die Y-Koordinate.
                /// </summary>
                public double Y { get; set; }

                protected override double[] GetCoordinates()
                {
                    double[] coords = { X, Y };
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
                    }
                }

                /// <summary>
                /// Erstellt einen neuen Vector2.
                /// </summary>
                public Vector2()
                {
                    X = 0.0;
                    Y = 0.0;
                }

                /// <summary>
                /// Erstellt einen neuen Vector2.
                /// </summary>
                /// <param name="x"></param>
                /// <param name="y"></param>
                public Vector2(double x, double y)
                {
                    X = x;
                    Y = y;
                }

                /// <summary>
                /// Erstellt einen neuen Vector2.
                /// </summary>
                /// <param name="p"></param>
                public Vector2(PointD p)
                {
                    X = p.X;
                    Y = p.Y;
                }

                /// <summary>
                /// Erstellt einen neuen Vector2.
                /// </summary>
                /// <param name="v"></param>
                public Vector2(Vector2 v)
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

                public override object Clone()
                {
                    return new Vector2(this);
                }

                public static Vector2 operator +(Vector2 left, Vector2 right)
                {
                    return (Vector2)Add(left, right);
                }

                public static Vector2 operator -(Vector2 left, Vector2 right)
                {
                    return (Vector2)Subtract(left, right);
                }

                public static Vector2 operator -(Vector2 value)
                {
                    return new Vector2(-value.X, -value.Y);
                }

                public static Vector2 operator *(Vector2 left, Vector2 right)
                {
                    return (Vector2)Multiplicate(left, right);
                }

                public static Vector2 operator *(Vector2 value, double skalar)
                {
                    return (Vector2)Multiplicate(value, skalar);
                }

                public static Vector2 operator *(double skalar, Vector2 value)
                {
                    return (Vector2)Multiplicate(value, skalar);
                }
            }
        }
    }
}
