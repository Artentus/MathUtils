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
            public struct CirclePolygon : IPolygon
            {
                Point2D _center;
                double _radius;
                Point2D[] points;

                public Point2D[] GetPoints()
                {
                    //wenn keine Punkte vorhanden dann berechnen
                    if (points == null)
                        CalculatePoints();

                    return points;
                }

                /// <summary>
                /// Der Mittelpunkt dieses Kreises.
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
                /// Der Radius dieses Kreises.
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

                private void CalculatePoints()
                {
                    //Anzahl Punkte bestimmen
                    var perimeter = 2 * System.Math.PI * Radius;
                    var pointCount = (int)(perimeter / 10.0); //durch 10 teilen um unnötigen Rechenaufwand zu vermeiden
                    points = new Point2D[pointCount];

                    //Punkte berechnen
                    var angleStep = (2 * System.Math.PI) / (double)pointCount;
                    for (int i = 0; i < pointCount; i++)
                        points[i] = MathHelper.GetPointOnCircle(Center, i * angleStep, Radius);
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
