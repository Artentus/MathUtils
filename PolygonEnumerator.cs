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
            internal class PolygonEnumerator : IEnumerator<Point2D>
            {
                IPolygon p;
                int index;

                internal PolygonEnumerator(IPolygon p)
                {
                    this.p = p;
                    index = -1;
                }

                public Point2D Current
                {
                    get
                    {
                        return p.GetPoints()[index];
                    }
                }

                public void Dispose() { }

                object System.Collections.IEnumerator.Current
                {
                    get
                    {
                        return p.GetPoints()[index];
                    }
                }

                public bool MoveNext()
                {
                    index++;
                    return index < p.GetPoints().Length;
                }

                public void Reset()
                {
                    index = -1;
                }
            }
        }
    }
}
