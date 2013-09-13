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
            public sealed class RectanglePolygon : IPolygon
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

                private static Point2D[] CalculatePoints(Point2D location, Vector2 size)
                {
                    var points = new Point2D[4]; //bei Rechteck immer vier Punkte
                    
                    //Punkte festlegen
                    points[0] = location;
                    points[1] = new Point2D(location.X + size.X, location.Y);
                    points[2] = location + size;
                    points[3] = new Point2D(location.X, location.Y + size.Y);

                    return points;
                }

                public RectanglePolygon(Point2D location, Vector2 size)
                {
                    untransformedPoints = CalculatePoints(location, size);
                    points = untransformedPoints;
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
