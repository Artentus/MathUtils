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
            /// Speichert Breite und Höhe eines Bereichs in doppelter Genauigkeit.
            /// </summary>
            public struct SizeD
            {
                /// <summary>
                /// Die Breite dieser SizeD.
                /// </summary>
                public double Width { get; set; }

                /// <summary>
                /// Die Höhe dieser SizeD.
                /// </summary>
                public double Height { get; set; }

                /// <summary>
                /// Erstellt eine neue SizeD.
                /// </summary>
                /// <param name="width">Die Breite der SizeD.</param>
                /// <param name="height">Die Höhe der SizeD.</param>
                public SizeD(double width, double height)
                    : this()
                {
                    Width = width;
                    Height = height;
                }

                /// <summary>
                /// Erstellt eine neue SizeD.
                /// </summary>
                /// <param name="s">Die Abmessungen der SizeD.</param>
                public SizeD(Size s)
                    : this()
                {
                    Width = s.Width;
                    Height = s.Height;
                }

                /// <summary>
                /// Erstellt eine neue SizeD.
                /// </summary>
                /// <param name="s">Die Abmessungen der SizeD.</param>
                public SizeD(SizeF s)
                    : this()
                {
                    Width = s.Width;
                    Height = s.Height;
                }

                /// <summary>
                /// Erstellt eine neue SizeD.
                /// </summary>
                /// <param name="p">Ein PointD, dessen Koordinaten die Abmessungen der SizeD darstellen.</param>
                public SizeD(PointD p)
                    : this()
                {
                    Width = p.X;
                    Height = p.Y;
                }

                /// <summary>
                /// Konvertiert diese SizeD in eine Size.
                /// </summary>
                /// <returns></returns>
                public Size ToSize()
                {
                    return new Size((int)Width, (int)Height);
                }

                /// <summary>
                /// Konvertiert diese SizeD in eine SizeF.
                /// </summary>
                /// <returns></returns>
                public SizeF ToSizeF()
                {
                    return new SizeF((float)Width, (float)Height);
                }

                /// <summary>
                /// Konvertiert diese SizeD in einen PointD.
                /// </summary>
                /// <returns></returns>
                public PointD ToPointD()
                {
                    return new PointD(Width, Height);
                }

                public static implicit operator SizeD(Size value)
                {
                    return new SizeD(value);
                }

                public static implicit operator SizeD(SizeF value)
                {
                    return new SizeD(value);
                }

                public static SizeD operator +(SizeD left, SizeD right)
                {
                    return new SizeD(left.Width + right.Width, left.Height + right.Height);
                }

                public static SizeD operator -(SizeD value)
                {
                    return new SizeD(-value.Width, -value.Height);
                }

                public static SizeD operator -(SizeD left, SizeD right)
                {
                    return new SizeD(left.Width - right.Width, left.Height - right.Height);
                }
            }
        }
    }
}
