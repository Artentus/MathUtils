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
            /// Stellt einen Kreisbogen dar.
            /// </summary>
            public struct ArcPolygon : IPolygon
            {
                Point2D _center;
                double _radius;
                double _startAngle;
                double _sweepAngle;
                Point2D[] points;

                public Point2D[] GetPoints()
                {
                    //wenn keine Punkte vorhanden dann berechnen
                    if (points == null)
                        CalculatePoints();

                    return points;
                }

                /// <summary>
                /// Der Mittelpunkt dieses Kreisbogens.
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
                /// Der Radius dieses Kreisbogens.
                /// </summary>
                public double Radius
                {
                    get
                    {
                        return _radius;
                    }
                    set
                    {
                        _radius = value;
                        points = null; //Punkte müssen neu berechnet werden
                    }
                }

                /// <summary>
                /// Der Startwinkel des Kreisbogens.
                /// </summary>
                public double StartAngle
                {
                    get
                    {
                        return _startAngle;
                    }
                    set
                    {
                        _startAngle = value;
                        points = null; //Punkte müssen neu berechnet werden
                    }
                }

                /// <summary>
                /// Der Winkel des Kreisbogens.
                /// </summary>
                public double SweepAngle
                {
                    get
                    {
                        return _sweepAngle;
                    }
                    set
                    {
                        _sweepAngle = value;
                        points = null; //Punkte müssen neu berechnet werden
                    }
                }

                private void CalculatePoints()
                {
                    //Anzahl Punkte bestimmen
                    var perimeter = SweepAngle * Radius;
                    var pointCount = (int)(perimeter / 10.0); //durch 10 teilen um unnötigen Rechenaufwand zu vermeiden
                    points = new Point2D[pointCount];

                    //Punkte berechnen
                    var angleStep = SweepAngle / (double)pointCount;
                    for (int i = 0; i < pointCount; i++)
                        points[i] = MathHelper.GetPointOnCircle(Center, i * angleStep + StartAngle, Radius);
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
