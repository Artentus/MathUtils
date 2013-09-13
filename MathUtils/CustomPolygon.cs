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
            public sealed class CustomPolygon : IPolygon
            {
                Point2D[] points;
                Point2D[] untransformedPoints;
                Matrix3x3 matrix;

                public Point2D[] GetPoints()
                {
                    return points;
                }

                public Point2D[] GetUntransformedPoints()
                {
                    return untransformedPoints;
                }

                public Matrix3x3 TransformationMatrix
                {
                    get
                    {
                        return matrix;
                    }
                    set
                    {
                        matrix = value;
                        if (value == null)
                            points = untransformedPoints;
                        else
                        {
                            points = new Point2D[untransformedPoints.Length];
                            for (int i = 0; i < points.Length; i++)
                                points[i] = untransformedPoints[i] * matrix;
                        }
                    }
                }

                public CustomPolygon(Point2D[] points)
                {
                    untransformedPoints = points;
                    this.points = points;
                }

                public IEnumerator<Point2D> GetEnumerator()
                {
                    return (points as IEnumerable<Point2D>).GetEnumerator();
                }

                System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
                {
                    return points.GetEnumerator();
                }
            }
        }
    }
}
