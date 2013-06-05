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
            public interface IPolygon : IEnumerable<Point2D>
            {
                Point2D[] GetPoints();
            }
        }
    }
}
