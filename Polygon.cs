using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using Artentus.Utils.Math;

namespace Artentus
{
    namespace Utils
    {
        namespace Geometry
        {
            public static class Polygon
            {
                /// <summary>
                /// Berechnet den Flächeninhalt dieses Polygons.
                /// </summary>
                /// <returns></returns>
                public static double Area(this IPolygon value)
                {
                    var points = value.GetPoints();
                    var ret = 0.0;
                    var j = 1;
                    for (int i = 0; i < points.Length; i++)
                    {
                        ret += Vector2.GetVectorProduct(points[i].ToVector2(), points[j].ToVector2());
                        j++;
                        if (j == points.Length)
                            j = 0;
                    }
                    return System.Math.Abs(ret * 0.5);
                }

                /// <summary>
                /// Berechnet den Umfang dieses Polygons.
                /// </summary>
                /// <returns></returns>
                public static double Perimeter(this IPolygon value)
                {
                    var points = value.GetPoints();
                    var ret = 0.0;
                    var j = 1;
                    for (int i = 0; i < points.Length; i++)
                    {
                        ret += points[i].DistanceTo(points[j]);
                        j++;
                        if (j == points.Length)
                            j = 0;
                    }
                    return ret;
                }

                /// <summary>
                /// Bestimmt die convexe Hülle dieses Polygons.
                /// </summary>
                /// <returns></returns>
                public static IPolygon ConvexHull(this IPolygon value)
                {
                    if (value is CirclePolygon || value is RectanglePolygon)
                        return value; //Sonderfälle brauchen nicht berechnet zu werden

                    var points = value.GetPoints();
                    
                    //Startpunkt bestimmen
                    var startIndex = (from item in points orderby item.X, item.Y select Array.IndexOf<Point2D>(points, item)).First(); //Punkt mit kleinstem X- und Y-Wert ist definitiv Teil der Hülle

                    var pointsInHull = new bool[points.Length];
                    pointsInHull[startIndex] = true;

                    //vorheriger Punkt
                    Point2D old;
                    if (startIndex > 0)
                        old = points[startIndex - 1];
                    else
                        old = points[points.Length - 1];

                    var lastIndex = startIndex;
                    while (lastIndex != startIndex)
                    {
                        var nextIndex = lastIndex + 1;
                        if (nextIndex == points.Length)
                            nextIndex = 0;
                        var targetIndex = -1;
                        var targetAngle = double.MinValue;

                        while (nextIndex != lastIndex)
                        {
                            //Winkel zum neuen Punkt bestimmen
                            var angle = 0.0;
                            var v1 = old - points[lastIndex];
                            var v2 = points[nextIndex] - points[lastIndex];
                            var d1 = v1.Length();
                            var d2 = v2.Length();
                            angle = System.Math.Acos(Vector.GetScalarProduct(v1, v2) / (d1 * d2));

                            //wenn größer als alter Winkel, nächsten Punkt festlegen
                            if (angle > targetAngle)
                            {
                                targetIndex = nextIndex;
                                targetAngle = angle;
                            }

                            nextIndex++;
                            if (nextIndex == points.Length)
                                nextIndex = 0;
                        }

                        old = points[lastIndex];
                        lastIndex = targetIndex;
                        pointsInHull[targetIndex] = true;
                    }

                    //Punkte der Hülle bestimmen
                    var hullPoints = new List<Point2D>();
                    for (int i = 0; i < points.Length; i++)
                        if (pointsInHull[i])
                            hullPoints.Add(points[i]);

                    //neues Polygon erzeugen
                    var hull = new GeneralPolygon();
                    hull.Points.AddRange(hullPoints);

                    return hull;
                }

                /// <summary>
                /// Berechnet den Mittelpunkt dieses Polygons.
                /// </summary>
                /// <returns></returns>
                public static Point2D Center(this IPolygon value)
                {
                    var x = 0.0;
                    var y = 0.0;
                    var vectorProduct = 0.0;

                    var points = value.GetPoints();

                    var j = 1;
                    for (int i = 0; i < points.Length; i++)
                    {
                        var p1 = points[i];
                        var p2 = points[j];
                        var vp = Vector2.GetVectorProduct(p1.ToVector2(), p2.ToVector2());
                        vectorProduct += vp;
                        x += (p1 + p2).X * vp;
                        y += (p1 + p2).Y * vp;

                        j++;
                        if (j == points.Length)
                            j = 0;
                    }

                    return new Point2D(x / (3.0 * vectorProduct), y / (3.0 * vectorProduct));
                }

                /// <summary>
                /// Prüft, ob sich ein Punkt innerhalb dieses Polygons befindet.
                /// </summary>
                /// <param name="value"></param>
                /// <param name="p"></param>
                /// <returns></returns>
                public static bool Contains(this IPolygon value, Point2D p)
                {
                    var alpha = 0;
                    var points = value.GetPoints();
                    var v1 = points[points.Length - 1];

                    //Startquadranten initialisieren
                    var q1 = 0;
                    if (v1.Y <= p.Y)
                        if (v1.X <= p.X)
                            q1 = 0;
                        else
                            q1 = 1;
                    else if (v1.X <= p.X)
                        q1 = 3;
                    else
                        q1 = 2;

                    foreach (var v2 in points)
                    {
                        //Quadrant des Punktes bestimmen
                        var q2 = 0;
                        if (v2.Y <= p.Y)
                            if (v2.X <= p.X)
                                q2 = 0;
                            else
                                q2 = 1;
                        else if (v2.X <= p.X)
                            q2 = 3;
                        else
                            q2 = 2;

                        //Quadrant auf Gesammtdrehung anwenden
                        switch ((q2 - q1) & 3)
                        {
                            case 0:
                                break;
                            case 1:
                                alpha += 1;
                                break;
                            case 3:
                                alpha -= 1;
                                break;
                            default:
                                var zx = ((v2.X - v1.X) * (p.Y - v1.Y) / (v2.Y - v1.Y)) + v1.X;
                                if (p.X == zx)
                                    return true;
                                if ((p.X > zx) == (v2.Y > v1.Y))
                                    alpha -= 2;
                                else
                                    alpha += 2;
                                break;
                        }

                        v1 = v2;
                        q1 = q2;
                    }

                    return System.Math.Abs(alpha) == 4;
                }

                /// <summary>
                /// Prüft, ob sich dieses Polygon mit einem anderen überschneidet.
                /// </summary>
                /// <param name="first"></param>
                /// <param name="second"></param>
                /// <returns></returns>
                public static bool IntersectsWith(this IPolygon first, IPolygon second)
                {
                    //var p1 = first.GetPoints();
                    //var p2 = first.GetPoints();

                    //for (int i = 0; i < p1.Length; i++)
                    //{
                    //    Point2D point1;
                    //    Point2D point2;
                    //    point1 = p1[i];
                    //    if (i == p1.Length - 1)
                    //        point2 = p1[0];
                    //    else
                    //        point2 = p1[i + 1];
                    //    var p = point2 - point1;

                    //    var axis = (Vector2)(new Vector2(-p.Y, p.X).Normalize());
                    //    var v1 = Project(axis, first);
                    //    var v2 = Project(axis, second);

                    //    var dist = 0.0;
                    //    if (v1.X < v2.X)
                    //        dist = v2.X - v1.Y;
                    //    else
                    //        dist = v1.X - v2.Y;

                    //    if (dist > 0)
                    //        return false;
                    //}

                    //for (int i = 0; i < p2.Length; i++)
                    //{
                    //    Point2D point1;
                    //    Point2D point2;
                    //    point1 = p2[i];
                    //    if (i == p2.Length - 1)
                    //        point2 = p2[0];
                    //    else
                    //        point2 = p2[i + 1];
                    //    var p = point2 - point1;

                    //    var axis = (Vector2)(new Vector2(-p.Y, p.X).Normalize());
                    //    var v1 = Project(axis, first);
                    //    var v2 = Project(axis, second);

                    //    var dist = 0.0;
                    //    if (v1.X < v2.X)
                    //        dist = v2.X - v1.Y;
                    //    else
                    //        dist = v1.X - v2.Y;

                    //    if (dist > 0)
                    //        return false;
                    //}

                    //return true;
                    if (HasSeparatingAxis(first, second))
                        return false;

                    if (HasSeparatingAxis(second, first))
                        return false;

                    return true;
                }

                private static bool HasSeparatingAxis(IPolygon first, IPolygon second)
                {
                    var points = first.GetPoints();

                    var prev = points.Length - 1;
                    for (int i = 0; i < points.Length; i++)
                    {
                        var edge = points[i] - points[prev];

                        var v = new Vector2(edge.Y, -edge.X);

                        double aMin, aMax, bMin, bMax;
                        ProjectPolygon(v, first, out aMin, out aMax);
                        ProjectPolygon(v, second, out bMin, out bMax);

                        if ((aMax < bMin) || (bMax < aMin))
                            return true;

                        prev = i;
                    }

                    return false;
                }

                private static void ProjectPolygon(Vector2 axis, IPolygon poly, out double min, out double max)
                {
                    var points = poly.GetPoints();
                    min = Vector.GetScalarProduct(axis, points[0]);
                    max = min;

                    for (int i = 1; i < points.Length; i++)
                    {
                        var d = Vector.GetScalarProduct(axis, points[i]);
                        if (d < min)
                            min = d;
                        if (d > max)
                            max = d;
                    }
                }
            }
        }
    }
}
