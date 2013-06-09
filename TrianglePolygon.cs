using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Artentus.Utils.Math;

namespace Artentus
{
    namespace Utils
    {
        namespace Geometry
        {
            /// <summary>
            /// Stellt ein Dreieck dar.
            /// </summary>
            public struct TrianglePolygon : IPolygon
            {
                public Point2D P1 { get; set; }
                public Point2D P2 { get; set; }
                public Point2D P3 { get; set; }

                public Point2D[] GetPoints()
                {
                    return new Point2D[] { P1, P2, P3 };
                }

                public IEnumerator<Point2D> GetEnumerator()
                {
                    return new PolygonEnumerator(this);
                }

                System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
                {
                    return new PolygonEnumerator(this);
                }
            }
        }
    }
}
