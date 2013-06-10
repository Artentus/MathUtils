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
            /// Stellt ein Polygon beliebiger Form dar.
            /// </summary>
            public struct GeneralPolygon : IPolygon
            {
                List<Point2D> _points;

                /// <summary>
                /// Die Punkte dieses Polygons.
                /// </summary>
                public List<Point2D> Points
                {
                    get
                    {
                        if (_points == null)
                            _points = new List<Point2D>();
                        return _points;
                    }
                }

                public Point2D[] GetPoints()
                {
                    return Points.ToArray();
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
