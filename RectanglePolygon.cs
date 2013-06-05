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
            public struct RectanglePolygon : IPolygon
            {
                Point2D _location;
                Vector2 _size;
                Point2D[] points;

                public Point2D[] GetPoints()
                {
                    //wenn keine Punkte vorhanden dann berechnen
                    if (points == null)
                        CalculatePoints();

                    return points;  //Punkte müssen neu berechnet werden
                }

                /// <summary>
                /// Die Position dieses Rechtecks.
                /// </summary>
                public Point2D Location
                {
                    get
                    {
                        return _location;
                    }
                    set
                    {
                        _location = value;
                        points = null;  //Punkte müssen neu berechnet werden
                    }
                }

                /// <summary>
                /// Die Größe dieses Rechtecks.
                /// </summary>
                public Vector2 Size
                {
                    get
                    {
                        return _size;
                    }
                    set
                    {
                        _size = value;
                        points = null;
                    }
                }

                private void CalculatePoints()
                {
                    points = new Point2D[4]; //bei Rechteck immer vier Punkte
                    
                    //Punkte festlegen
                    points[0] = Location;
                    points[1] = new Point2D(Location.X + Size.X, Location.Y);
                    points[2] = Location + Size;
                    points[3] = new Point2D(Location.X, Location.Y + Size.Y);
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
