using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Artentus
{
    namespace Utils
    {
        namespace Math
        {
            /// <summary>
            /// Stellt Funktionen zur Transformation zweidimensionaler Vektoren bereit.
            /// </summary>
            public class Matrix3x3 : SquareMatrix
            {
                /// <summary>
                /// Erstellt eine neue Matrix4x4.
                /// </summary>
                public Matrix3x3()
                    : base(3) { }

                /// <summary>
                /// Erstellt aus der angegebenen Matrix eine Matrix4x4, sofern die Abmessungen stimmen.
                /// </summary>
                public static new Matrix3x3 FromMatrix(Matrix m)
                {
                    if (m.ColumnCount == 3 && m.RowCount == 3)
                    {
                        var sm = new Matrix3x3();

                        for (int x = 0; x < sm.Size; x++)
                            for (int y = 0; y < sm.Size; y++)
                                sm[x, y] = m[x, y]; //Wert kopieren

                        return sm;
                    }
                    else //falsche Abmessungen
                        throw new ArgumentException("Die angegebene Matrix war nicht in eine Matrix3x3 konvertierbar.");
                }

                /// <summary>
                /// Erstellt eine 3x3-Einheitsmatrix.
                /// </summary>
                public static Matrix3x3 GetIdentity()
                {
                    return Matrix3x3.FromMatrix(SquareMatrix.GetIdentity(3));
                }

                /// <summary>
                /// Berechnet die Adjunkte zu dieser Matrix3x3.
                /// </summary>
                public new Matrix3x3 GetAdjugate()
                {
                    return Matrix3x3.FromMatrix(base.GetAdjugate());
                }

                /// <summary>
                /// Berechnet die invertierte Matrix dieser Matrix3x3.
                /// </summary>
                public new Matrix3x3 GetInverse()
                {
                    return Matrix3x3.FromMatrix(base.GetInverse());
                }

                /// <summary>
                /// Erstellt eine Matrix, die eine Skalierung darstellt.
                /// </summary>
                public static Matrix3x3 Scalation(double scaleX, double scaleY)
                {
                    var m = Matrix3x3.GetIdentity();

                    m[0, 0] = scaleX;
                    m[1, 1] = scaleY;

                    return m;
                }

                /// <summary>
                /// Erstellt eine Matrix, die eine Skalierung darstellt.
                /// </summary>
                public static Matrix3x3 Scalation(double scale)
                {
                    return Scalation(scale, scale);
                }

                /// <summary>
                /// Erstellt eine Matrix, die eine Translation darstellt.
                /// </summary>
                public static Matrix3x3 Translation(double x, double y)
                {
                    var m = Matrix3x3.GetIdentity();

                    m[2, 0] = x;
                    m[2, 1] = y;

                    return m;
                }

                /// <summary>
                /// Erstellt eine Matrix, die eine Translation darstellt.
                /// </summary>
                public static Matrix3x3 Translation(Vector2 v)
                {
                    return Translation(v.X, v.Y);
                }

                /// <summary>
                /// Erstellt eine Matrix, die eine Rotation darstellt.
                /// </summary>
                public static Matrix3x3 Rotation(double angle)
                {
                    var m = Matrix3x3.GetIdentity();

                    var sin = System.Math.Sin(angle);
                    var cos = System.Math.Cos(angle);

                    m[0, 0] = cos;
                    m[0, 1] = sin;
                    m[1, 0] = -sin;
                    m[1, 1] = cos;

                    return m;
                }

                /// <summary>
                /// Wendet eine Matrix3x3 auf einen Vector2 an.
                /// </summary>
                public static Vector2 Multiply(Matrix3x3 m, Vector2 v)
                {
                    //Matrix mit Vektor multiplizieren
                    var resultMatrix = m * new Vector3(v.X, v.Y, 1).ToVerticalMatrix(); //Vektor in Matrix umwandeln

                    return new Vector2(resultMatrix[0, 0], resultMatrix[0, 1]); //neuen Vektor bilden
                }

                /// <summary>
                /// Wendet eine Matrix3x3 auf einen Point2D an.
                /// </summary>
                public static Point2D Multiply(Matrix3x3 m, Point2D v)
                {
                    //Matrix mit Vektor multiplizieren
                    var resultMatrix = m * new Vector3(v.X, v.Y, 1).ToVerticalMatrix(); //Vektor in Matrix umwandeln

                    return new Point2D(resultMatrix[0, 0], resultMatrix[0, 1]); //neuen Vektor bilden
                }

                public static Matrix3x3 operator *(Matrix3x3 value, double skalar)
                {
                    return Matrix3x3.FromMatrix(Multiply(value, skalar));
                }

                public static Matrix3x3 operator *(double skalar, Matrix3x3 value)
                {
                    return Matrix3x3.FromMatrix(Multiply(value, skalar));
                }

                public static Matrix3x3 operator *(Matrix3x3 left, Matrix3x3 right)
                {
                    return Matrix3x3.FromMatrix(Multiply(left, right));
                }

                public static Vector2 operator *(Matrix3x3 m, Vector2 v)
                {
                    return Multiply(m, v);
                }

                public static Vector2 operator *(Vector2 v, Matrix3x3 m)
                {
                    return Multiply(m, v);
                }

                public static Point2D operator *(Matrix3x3 m, Point2D v)
                {
                    return Multiply(m, v);
                }

                public static Point2D operator *(Point2D v, Matrix3x3 m)
                {
                    return Multiply(m, v);
                }

                public static Matrix3x3 operator +(Matrix3x3 left, Matrix3x3 right)
                {
                    return Matrix3x3.FromMatrix(Add(left, right));
                }
            }
        }
    }
}
