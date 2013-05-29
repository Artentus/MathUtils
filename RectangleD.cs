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
            /// Stellt einen Breich mit 4 Koordinaten in doppelter Genauigkeit dar.
            /// </summary>
            public struct RectangleD
            {
                /// <summary>
                /// Die Position dieses Rectangles auf der X-Achse.
                /// </summary>
                public double X { get; set; }

                /// <summary>
                /// Die Position dieses Rectangles auf der Y-Achse.
                /// </summary>
                public double Y { get; set; }

                /// <summary>
                /// Die Breite dieses Ranctangles.
                /// </summary>
                public double Width { get; set; }

                /// <summary>
                /// Die Höhe dieses Rectangles.
                /// </summary>
                public double Height { get; set; }

                /// <summary>
                /// Die Position dieses Rectangles.
                /// </summary>
                public PointD Location
                {
                    get
                    {
                        return new PointD(X, Y);
                    }
                    set
                    {
                        X = value.X;
                        Y = value.Y;
                    }
                }

                /// <summary>
                /// Die Größe dieses Rectangles.
                /// </summary>
                public SizeD Size
                {
                    get
                    {
                        return new SizeD(Width, Height);
                    }
                    set
                    {
                        Width = value.Width;
                        Height = value.Height;
                    }
                }

                /// <summary>
                /// Die Position des linken Randes dieses Rectangles.
                /// </summary>
                public double Left
                {
                    get
                    {
                        return X;
                    }
                    set
                    {
                        X = value;
                    }
                }

                /// <summary>
                /// Die Position des oberen Randes dieses Rectangles.
                /// </summary>
                public double Top
                {
                    get
                    {
                        return Y;
                    }
                    set
                    {
                        Y = value;
                    }
                }

                /// <summary>
                /// Die Position des rechten Randes dieses Rectangles.
                /// </summary>
                public double Right
                {
                    get
                    {
                        return X + Width;
                    }
                    set
                    {
                        Width = value - X;
                    }
                }

                /// <summary>
                /// Die Position des unteren Randes dieses Rectangles.
                /// </summary>
                public double Bottom
                {
                    get
                    {
                        return Y + Height;
                    }
                    set
                    {
                        Height = value - Y;
                    }
                }

                /// <summary>
                /// Erstellt ein neues RectangleD.
                /// </summary>
                /// <param name="x">Die Position des Rectangles auf der X-Achse.</param>
                /// <param name="y">Die Position des Rectangles auf der y-Achse.</param>
                /// <param name="width">Die Breite des Rectangles.</param>
                /// <param name="height">Die Höhe des Rectangles.</param>
                public RectangleD(double x, double y, double width, double height)
                    : this()
                {
                    X = x;
                    Y = y;
                    Width = width;
                    Height = height;
                }

                /// <summary>
                /// Erstellt ein neues RectangleD.
                /// </summary>
                /// <param name="location">Die Position des Rectangles.</param>
                /// <param name="size">Die Größe des Rectangles.</param>
                public RectangleD(PointD location, SizeD size)
                    : this()
                {
                    X = location.X;
                    Y = location.Y;
                    Width = size.Width;
                    Height = size.Height;
                }

                /// <summary>
                /// Erstellt ein neues RectangleD.
                /// </summary>
                /// <param name="rect">Der Bereich, aus dem das Rectangle erstellt werden soll.</param>
                public RectangleD(Rectangle rect)
                    : this()
                {
                    X = rect.X;
                    Y = rect.Y;
                    Width = rect.Width;
                    Height = rect.Height;
                }

                /// <summary>
                /// Erstellt ein neues RectangleD.
                /// </summary>
                /// <param name="rect">Der Bereich, aus dem das Rectangle erstellt werden soll.</param>
                public RectangleD(RectangleF rect)
                    : this()
                {
                    X = rect.X;
                    Y = rect.Y;
                    Width = rect.Width;
                    Height = rect.Height;
                }

                /// <summary>
                /// Konvertiert dieses RectangleD in ein Rectangle.
                /// </summary>
                /// <returns></returns>
                public Rectangle ToRectangle()
                {
                    return new Rectangle((int)X, (int)Y, (int)Width, (int)Height);
                }

                /// <summary>
                /// Konvertiert dieses RectangleD in ein RectangleF.
                /// </summary>
                /// <returns></returns>
                public RectangleF ToRectangleF()
                {
                    return new RectangleF((float)X, (float)Y, (float)Width, (float)Height);
                }

                /// <summary>
                /// Verschiebt dieses RectangleD um die angegebenen Werte.
                /// </summary>
                /// <param name="x"></param>
                /// <param name="y"></param>
                public void Offset(double x, double y)
                {
                    X += x;
                    Y += y;
                }

                /// <summary>
                /// Verschieb dieses RectangleD um den angegebenen PointD.
                /// </summary>
                /// <param name="p"></param>
                public void Offset(PointD p)
                {
                    X += p.X;
                    Y += p.Y;
                }

                /// <summary>
                /// Vergrüßert dieses RectangleD um die angegebenen Werte.
                /// </summary>
                /// <param name="width"></param>
                /// <param name="height"></param>
                public void Inflate(double width, double height)
                {
                    Width += width;
                    Height += height;
                }

                /// <summary>
                /// Vergößert dieses RectangleD um die angegebene SizeD.
                /// </summary>
                /// <param name="s"></param>
                public void Inflate(SizeD s)
                {
                    Width += s.Width;
                    Height += s.Height;
                }

                /// <summary>
                /// Prüft, ob sich ein Punkt innerhalb dieses RectangleDs befindet.
                /// </summary>
                /// <param name="p"></param>
                /// <returns></returns>
                public bool Contains(PointD p)
                {
                    //Minimal- und Maximalwert auf der X-Achse berechnen
                    var minX = System.Math.Min(Left, Right);
                    var maxX = System.Math.Max(Left, Right);

                    //Minimal- und Maximalwert auf der Y-Achse berechnen
                    var minY = System.Math.Min(Top, Bottom);
                    var maxY = System.Math.Max(Top, Bottom);

                    return (p.X >= minX && p.X <= maxX) && (p.Y >= minY && p.Y <= maxY);
                }

                /// <summary>
                /// Prüft, ob diese RectangleD ein anderes komplett einschließt.
                /// </summary>
                /// <param name="rect"></param>
                /// <returns></returns>
                public bool Contains(RectangleD rect)
                {
                    //Minimal- und Maximalwerte auf der X-Achse berechnen
                    var minX1 = System.Math.Min(Left, Right);
                    var maxX1 = System.Math.Max(Left, Right);
                    var minX2 = System.Math.Min(rect.Left, rect.Right);
                    var maxX2 = System.Math.Max(rect.Left, rect.Right);

                    //Minimal- und Maximalwerte auf der Y-Achse berechnen
                    var minY1 = System.Math.Min(Top, Bottom);
                    var maxY1 = System.Math.Max(Top, Bottom);
                    var minY2 = System.Math.Min(rect.Top, rect.Bottom);
                    var maxY2 = System.Math.Max(rect.Top, rect.Bottom);

                    return ((minX1 >= minX2 && minX1 <= maxX2) && (maxX1 >= minX2 && maxX1 <= maxX2)) && //Einschließung auf der X-Achse testen
                        ((minY1 >= minY2 && minY1 <= maxY2) && (maxY1 >= minY2 && maxY1 <= maxY2)); //Einschließung auf der Y-Achse testen
                }

                /// <summary>
                /// Prüft, ob dieses RectangleD sich mit einem anderen überschneidet.
                /// </summary>
                /// <param name="rect"></param>
                /// <returns></returns>
                public bool IntersectsWith(RectangleD rect)
                {
                    //Minimal- und Maximalwerte auf der X-Achse berechnen
                    var minX1 = System.Math.Min(Left, Right);
                    var maxX1 = System.Math.Max(Left, Right);
                    var minX2 = System.Math.Min(rect.Left, rect.Right);
                    var maxX2 = System.Math.Max(rect.Left, rect.Right);

                    //Minimal- und Maximalwerte auf der Y-Achse berechnen
                    var minY1 = System.Math.Min(Top, Bottom);
                    var maxY1 = System.Math.Max(Top, Bottom);
                    var minY2 = System.Math.Min(rect.Top, rect.Bottom);
                    var maxY2 = System.Math.Max(rect.Top, rect.Bottom);

                    return ((minX1 >= minX2 && minX1 <= maxX2) || (maxX1 >= minX2 && maxX1 <= maxX2) || (minX1 <= minX2 && maxX1 >= maxX2)) && //Überlagerung auf der X-Achse testen
                        ((minY1 >= minY2 && minY1 <= maxY2) || (maxY1 >= minY2 && maxY1 <= maxY2) || (minY1 <= minY2 && maxY1 >= maxY2)); //Überlagerung auf der Y-Achse testen
                }

                /// <summary>
                /// Bildet die Schnittmenge aus diesem und einem anderen RectangleD.
                /// </summary>
                /// <param name="rect"></param>
                public void Intersect(RectangleD rect)
                {
                    //Minimal- und Maximalwerte auf der X-Achse berechnen
                    var minX1 = System.Math.Min(Left, Right);
                    var maxX1 = System.Math.Max(Left, Right);
                    var minX2 = System.Math.Min(rect.Left, rect.Right);
                    var maxX2 = System.Math.Max(rect.Left, rect.Right);

                    //Minimal- und Maximalwerte auf der Y-Achse berechnen
                    var minY1 = System.Math.Min(Top, Bottom);
                    var maxY1 = System.Math.Max(Top, Bottom);
                    var minY2 = System.Math.Min(rect.Top, rect.Bottom);
                    var maxY2 = System.Math.Max(rect.Top, rect.Bottom);

                    //Minimalwerte beider Rechtecke berechnen
                    Left = System.Math.Max(minX1, minX2);
                    Right = System.Math.Min(maxX1, maxX2);
                    Top = System.Math.Max(minY1, minY2);
                    Bottom = System.Math.Min(maxY1, maxY2);
                }

                public static implicit operator RectangleD(Rectangle value)
                {
                    return new RectangleD(value);
                }

                public static implicit operator RectangleD(RectangleF value)
                {
                    return new RectangleD(value);
                }
            }
        }
    }
}
