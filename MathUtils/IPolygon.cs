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
                Point2D[] GetPoints();

                /// <summary>
                /// Gibt die Punkte des Polygons zurück, ohne die Transformationsmatrix zu berücksichtigen.
                /// </summary>
                Point2D[] GetUntransformedPoints();

                /// <summary>
                /// Die Transformationsmatrix für dieses Polygon.
                /// </summary>
                Matrix3x3 TransformationMatrix { get; set; }
            }
        }
    }
}
