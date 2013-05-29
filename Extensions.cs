using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace Artentus
{
    namespace Utils
    {
        namespace Math
        {
            public static class Extensions
            {
                /// <summary>
                /// Konvertiert diesen Point in einen PointD.
                /// </summary>
                /// <param name="p"></param>
                /// <returns></returns>
                public static PointD ToPointD(this Point p)
                {
                    return new PointD(p);
                }

                /// <summary>
                /// Konvertiert diesen PointF in einen PointD.
                /// </summary>
                /// <param name="p"></param>
                /// <returns></returns>
                public static PointD ToPointD(this PointF p)
                {
                    return new PointD(p);
                }

                /// <summary>
                /// Konvertiert diese Size in eine SizeD.
                /// </summary>
                /// <param name="s"></param>
                /// <returns></returns>
                public static SizeD ToSizeD(this Size s)
                {
                    return new SizeD(s);
                }

                /// <summary>
                /// Konvertiert dies SizeF in eine SIzeD.
                /// </summary>
                /// <param name="s"></param>
                /// <returns></returns>
                public static SizeD ToSizeD(this SizeF s)
                {
                    return new SizeD(s);
                }

                /// <summary>
                /// Konvertiert dieses Rectangle in ein RectangleD.
                /// </summary>
                /// <param name="r"></param>
                /// <returns></returns>
                public static RectangleD ToRectangleD(this Rectangle r)
                {
                    return new RectangleD(r);
                }

                /// <summary>
                /// Konvertiert dieses RectangleF in ein RectangleD.
                /// </summary>
                /// <param name="r"></param>
                /// <returns></returns>
                public static RectangleD ToReactangleD(this RectangleF r)
                {
                    return new RectangleD(r);
                }
            }
        }
    }
}
