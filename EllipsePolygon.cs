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
            public struct EllipsePolygon : IPolygon
            {
                Point2D _center;
                double _radiusX;
                double _radiusY;
                Point2D[] points;

                public Point2D[] GetPoints()
                {
                    //wenn keine Punkte vorhanden dann berechnen
                    if (points == null)
                        CalculatePoints();

                    return points;
                }

                /// <summary>
                /// Der Mittelpunkt dieser Ellipse.
                /// </summary>
                public Point2D Center
                {
                    get
                    {
                        return _center;
                    }
                    set
                    {
                        _center = value;
                        points = null; //Punkte müssen neu berechnet werden
                    }
                }

                /// <summary>
                /// Der Radius in X-Richtung dieser Ellipse.
                /// </summary>
                public double RadiusX
                {
                    get
                    {
                        return _radiusX;
                    }
                    set
                    {
                        _radiusX = value;
                        points = null; //Punkte müssen neu berechnet werden
                    }
                }

                /// <summary>
                /// Der Radius in Y-Richtung dieser Ellipse.
                /// </summary>
                public double RadiusY
                {
                    get
                    {
                        return _radiusY;
                    }
                    set
                    {
                        _radiusY = value;
                        points = null; //Punkte müssen neu berechnet werden
                    }
                }

                private void CalculatePoints()
                {
                    //Anzahl Punkte bestimmen
                    var perimeter = 2 * System.Math.PI * System.Math.Max(RadiusX, RadiusY);
                    var pointCount = (int)(perimeter / 10.0); //durch 10 teilen um unnötigen Rechenaufwand zu vermeiden
                    points = new Point2D[pointCount];
                    var yFactor = RadiusY / RadiusX;

                    //Punkte berechnen
                    var angleStep = (2 * System.Math.PI) / (double)pointCount;
                    for (int i = 0; i < pointCount; i++)
                    {
                        points[i] = MathHelper.GetPointOnCircle(Center, i * angleStep, RadiusX);
                        points[i].Y *= yFactor;
                    }
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
