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
            /// Stellt ein Polygon dar.
            /// </summary>
            public interface IPolygon : IEnumerable<Point2D>
            {
                /// <summary>
                /// Gibt die Punkte des Polygons zurück.
                /// </summary>
                /// <returns></returns>
                Point2D[] GetPoints();
            }
        }
    }
}
