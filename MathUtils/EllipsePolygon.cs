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
            /// Stellt eine Ellipse dar.
            /// </summary>
            public sealed class EllipsePolygon : IPolygon
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

                private static Point2D[] CalculatePoints(Point2D center, double radiusX, double radiusY)
                {
                    //Anzahl Punkte bestimmen
                    var perimeter = 2 * System.Math.PI * System.Math.Max(radiusX, radiusY);
                    var pointCount = (int)(perimeter / 10.0); //durch 10 teilen um unnötigen Rechenaufwand zu vermeiden
                    var points = new Point2D[pointCount];
                    var yFactor = radiusY / radiusX;

                    //Punkte berechnen
                    var angleStep = (2 * System.Math.PI) / (double)pointCount;
                    for (int i = 0; i < pointCount; i++)
                    {
                        points[i] = MathHelper.GetPointOnCircle(center, i * angleStep, radiusX);
                        points[i].Y *= yFactor;
                    }

                    return points;
                }

                public EllipsePolygon(Point2D center, double radiusX, double radiusY)
                {
                    untransformedPoints = CalculatePoints(center, radiusX, radiusY);
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
