using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using System.Drawing;
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
                public static double Area(this IPolygon value)
                {
                    var points = value.GetPoints();
                    var ret = 0.0;
                    var j = 1;
                    for (int i = 0; i < points.Length; i++)
                    {
                        ret += Point2D.GetVectorProduct(points[i], points[j]);
                        j++;
                        if (j == points.Length)
                            j = 0;
                    }
                    return System.Math.Abs(ret * 0.5);
                }

                /// <summary>
                /// Berechnet den Umfang dieses Polygons.
                /// </summary>
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
                public static IPolygon ConvexHull(this IPolygon value)
                {
                    var startPoint = value.GetPoints().OrderBy(item => item.Y).ThenBy(item => item.X).First();

                    var comp = new AngleComparer(startPoint);
                    var sorted = value.GetPoints().OrderBy(item => item, comp).ToArray();

                    var result = new Stack<Point2D>();
                    result.Push(sorted[0]);
                    result.Push(sorted[1]);

                    for (int i = 2; i < sorted.Length; i++)
                    {
                        while (GetSweepDirection(result.ElementAt(1), result.ElementAt(0), sorted[i]) > 0)
                            result.Pop();

                        result.Push(sorted[i]);
                    }
                    return new CustomPolygon(result.ToArray());
                }

                private class AngleComparer : IComparer<Point2D>
                {
                    Point2D start;

                    internal AngleComparer(Point2D start)
                    {
                        this.start = start;
                    }

                    public int Compare(Point2D a, Point2D b)
                    {
                        double angleA = (start.X - a.X) / (a.Y - start.Y);
                        double angleB = (start.X - b.X) / (b.Y - start.Y);

                        var result = (-angleA).CompareTo(-angleB);

                        if (result == 0)
                        {
                            var distA = a.DistanceTo(start);
                            var distB = b.DistanceTo(start);

                            result = distA.CompareTo(distB);
                        }

                        return result;
                    }
                }

                private static double GetSweepDirection(Point2D p1, Point2D p2, Point2D p3)
                {
                    return (p2.X - p1.X) * (p3.Y - p1.Y) - (p2.Y - p1.Y) * (p3.X - p1.X);
                }

                /// <summary>
                /// Berechnet den Mittelpunkt dieses Polygons.
                /// </summary>
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
                        var vp = Point2D.GetVectorProduct(p1, p2);
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
                public static bool Contains(this IPolygon value, Point2D p)
                {
                    var alpha = 0;
                    var points = value.GetPoints();
                    var v1 = points[points.Length - 1];

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
                /// Prüft, ob diese Polygon ein anderes komplett einschließt.
                /// </summary>
                public static bool Contains(this IPolygon first, IPolygon second)
                {
                    foreach (var p in second.GetPoints())
                        if (!first.Contains(p))
                            return false;
                    return true;
                }

                /// <summary>
                /// Prüft, ob sich dieses Polygon mit einem anderen überschneidet.
                /// </summary>
                public static bool IntersectsWith(this IPolygon first, IPolygon second, out Vector2 minimumTranslationVector)
                {
                    double minOverlap = double.MaxValue;
                    minimumTranslationVector = new Vector2();

                    if (HasSeparatingAxis(first, second, ref minOverlap, ref minimumTranslationVector))
                        return false;

                    if (HasSeparatingAxis(second, first, ref minOverlap, ref minimumTranslationVector))
                        return false;

                    Vector2 d = Center(first) - Center(second);
                    if (Vector.DotProduct(d, minimumTranslationVector) > 0)
                        minimumTranslationVector = -minimumTranslationVector;
                    minimumTranslationVector = minOverlap * minimumTranslationVector;

                    return true;
                }		

                private static bool HasSeparatingAxis(IPolygon first, IPolygon second, ref double minOverlap, ref Vector2 axis)
                {
                    var points = first.GetPoints();

                    var prev = points.Length - 1;
                    for (int i = 0; i < points.Length; i++)
                    {
                        var edge = points[i] - points[prev];

                        var v = edge.CrossProduct.Normalize();

                        double aMin, aMax, bMin, bMax;
                        ProjectPolygon(v, first, out aMin, out aMax);
                        ProjectPolygon(v, second, out bMin, out bMax);


                        if ((aMax < bMin) || (bMax < aMin))
                            return true;
                        double overlapping = aMax < bMax ? aMax - bMin : bMax - aMin;
                        if (overlapping < minOverlap)
                        {
                            minOverlap = overlapping;
                            axis = v;
                        }

                        prev = i;
                    }

                    return false;
                }

                private static void ProjectPolygon(Vector2 axis, IPolygon poly, out double min, out double max)
                {
                    var points = poly.GetPoints();
                    var axisAsPoint = axis.As<Point2D>();
                    min = Vector.DotProduct(axisAsPoint, points[0]);
                    max = min;

                    for (int i = 1; i < points.Length; i++)
                    {
                        var d = Vector.DotProduct(axisAsPoint, points[i]);
                        if (d < min)
                            min = d;
                        if (d > max)
                            max = d;
                    }
                }

                /// <summary>
                /// Gibt die Boundingbox dieses Polygons zurück.
                /// </summary>
                public static RectangleF BoundingBox(this IPolygon value)
                {
                    var minX = double.MaxValue;
                    var maxX = double.MinValue;
                    var minY = double.MaxValue;
                    var maxY = double.MinValue;

                    foreach (var p in value.GetPoints())
                    {
                        if (p.X < minX)
                            minX = p.X;

                        if (p.X > maxX)
                            maxX = p.X;

                        if (p.Y < minY)
                            minY = p.Y;

                        if (p.Y > maxY)
                            maxY = p.Y;
                    }

                    return new RectangleF((float)minX, (float)minY, (float)(maxX - minX), (float)(maxY - minY));
                }
            }
        }
    }
}
