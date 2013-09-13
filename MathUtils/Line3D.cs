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
            /// Stellt eine Gerade im dreidimensionalen Raum dar.
            /// </summary>
            public struct Line3D
            {
                /// <summary>
                /// Ein beliebiger Punkt auf der Geraden.
                /// </summary>
                public Point3D P { get; set; }

                /// <summary>
                /// Der Steigungsvektor der Geraden.
                /// </summary>
                public Vector3 V { get; set; }

                /// <summary>
                /// Berechnet einen Punkt auf der Geraden, der {f * V} von {P} entfernt ist.
                /// </summary>
                /// <returns></returns>
                public Point3D GetPoint(double f)
                {
                    return P + (f * V);
                }

                /// <summary>
                /// Bestimmt, ob diese Gerade parallel zu einer anderen ist.
                /// </summary>
                /// <returns></returns>
                public bool ParallelTo(Line3D other)
                {
                    var factorX = System.Math.Abs(V.X / other.V.X);
                    var factorY = System.Math.Abs(V.Y / other.V.Y);
                    var factorZ = System.Math.Abs(V.Z / other.V.Z);

                    return (factorX == factorY) && (factorX == factorZ);
                }

                /// <summary>
                /// Bestimmt, ob eine andere Line3D-Instanz die selbe Gerade beschreibt.
                /// </summary>
                /// <returns></returns>
                public bool IsSame(Line3D other)
                {
                    if (!ParallelTo(other))
                        return false;

                    var factor = (other.P.X - P.X) / V.X;
                    var y = other.P.Y + (factor * other.V.Y);
                    var z = other.P.Z + (factor * other.V.Z);

                    return (y == P.Y) && (z == P.Z);
                }

                private bool IntersectsInner(Line3D other, out double f1, out double f2)
                {
                    f2 = (-(P.X * V.Y - P.Y * V.X - other.P.X * V.Y + other.P.Y * V.X)) / (V.X * other.V.Y - V.Y * other.V.X);
                    f1 = ((other.P.X + f2 * other.V.X) - P.X) / V.X;

                    return P.Z + f1 * V.Z == other.P.Z + f2 * other.V.Z;
                }

                /// <summary>
                /// Prüft, ob sich diese Gerade mit einer anderen schneidet.
                /// </summary>
                /// <returns></returns>
                public bool IntersectsWith(Line3D other)
                {
                    double f1, f2;
                    return IntersectsInner(other, out f1, out f2);
                }

                /// <summary>
                /// Berechnet den Schnittpunkt zweier Geraden.
                /// </summary>
                /// <returns></returns>
                public Point3D IntersectionWith(Line3D other)
                {
                    double f1, f2;
                    if (IntersectsInner(other, out f1, out f2))
                        return GetPoint(f1);
                    else
                        throw new InvalidOperationException("Diese beiden Geraden schneiden sich nicht.");
                }

                /// <summary>
                /// Berechnet die Entfernung von dieser Geraden zu einer anderen.
                /// </summary>
                /// <returns></returns>
                public double DistanceTo(Line3D other)
                {
                    return Vector.DotProduct(P - other.P, Vector3.GetCrossProduct(V, other.V).Normalize());
                }
            }
        }
    }
}
