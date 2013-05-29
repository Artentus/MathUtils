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
            /// Stellt ein zweidimensionales Koordinatenpaar in doppelter Genauigkeit dar.
            /// </summary>
            public struct PointD
            {
                /// <summary>
                /// Die X-Koordinate dieses PointDs.
                /// </summary>
                public double X { get; set; }

                /// <summary>
                /// Die Y-Koordinate dieses PointDs.
                /// </summary>
                public double Y { get; set; }

                /// <summary>
                /// Erstellt einen neuen PointD.
                /// </summary>
                /// <param name="x">Die X-Koordinate des PointD.</param>
                /// <param name="y">Die Y-Koordinate des PointD.</param>
                public PointD(double x, double y)
                    : this()
                {
                    X = x;
                    Y = y;
                }

                /// <summary>
                /// Erstellt einen neuen PointD.
                /// </summary>
                /// <param name="p">Die Position des PointD.</param>
                public PointD(Point p)
                    : this()
                {
                    X = p.X;
                    Y = p.Y;
                }

                /// <summary>
                /// Erstellt einen neuen PointD.
                /// </summary>
                /// <param name="p">Die Position des PointD.</param>
                public PointD(PointF p)
                    : this()
                {
                    X = p.X;
                    Y = p.Y;
                }

                /// <summary>
                /// Konvertiert diesen PointD in einen Point.
                /// </summary>
                /// <returns></returns>
                public Point ToPoint()
                {
                    return new Point((int)X, (int)Y);
                }

                /// <summary>
                /// Konvertiert diesen PointD in einen PointF.
                /// </summary>
                /// <returns></returns>
                public PointF ToPointF()
                {
                    return new PointF((float)X, (float)Y);
                }

                /// <summary>
                /// Verschiebt diesen PointD um die angegebenen Koordinaten.
                /// </summary>
                /// <param name="p"></param>
                public void Offset(PointD p)
                {
                    X += p.X;
                    Y += p.Y;
                }

                /// <summary>
                /// Verschiebt diesen PointD um die angegebenen Koordinaten.
                /// </summary>
                /// <param name="x"></param>
                /// <param name="y"></param>
                public void Offset(double x, double y)
                {
                    X += x;
                    Y += y;
                }

                public static implicit operator PointD(Point value)
                {
                    return new PointD(value);
                }

                public static implicit operator PointD(PointF value)
                {
                    return new PointD(value);
                }

                public static PointD operator +(PointD left, PointD right)
                {
                    return new PointD(left.X + right.X, left.Y + right.Y);
                }

                public static PointD operator -(PointD value)
                {
                    return new PointD(-value.X, -value.Y);
                }

                public static PointD operator -(PointD left, PointD right)
                {
                    return new PointD(left.X - right.X, left.Y - right.Y);
                }
            }
        }
    }
}
