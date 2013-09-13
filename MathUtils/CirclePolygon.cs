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
            /// Stellt einen Kreis dar.
            /// </summary>
            public sealed class CirclePolygon : IPolygon
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

                private static Point2D[] CalculatePoints(Point2D center, double radius)
                {
                    //Anzahl Punkte bestimmen
                    var perimeter = 2 * System.Math.PI * radius;
                    var pointCount = (int)(perimeter / 10.0); //durch 10 teilen um unnötigen Rechenaufwand zu vermeiden
                    var points = new Point2D[pointCount];

                    //Punkte berechnen
                    var angleStep = (2 * System.Math.PI) / (double)pointCount;
                    for (int i = 0; i < pointCount; i++)
                        points[i] = MathHelper.GetPointOnCircle(center, i * angleStep, radius);

                    return points;
                }

                public CirclePolygon(Point2D center, double radius)
                {
                    untransformedPoints = CalculatePoints(center, radius);
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
