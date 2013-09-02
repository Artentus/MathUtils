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
            /// Stellt Funktionen zur Transformation dreidimensionaler Vektoren bereit.
            /// </summary>
            public class Matrix4x4 : SquareMatrix
            {

                /// <summary>
                /// Erstellt eine neue Matrix4x4.
                /// </summary>
                public Matrix4x4()
                    : base(4) { }

                /// <summary>
                /// Erstellt aus der angegebenen Matrix eine Matrix4x4, sofern die Abmessungen stimmen.
                /// </summary>
                /// <param name="m"></param>
                public static new Matrix4x4 FromMatrix(Matrix m)
                {
                    if (m.ColumnCount == 4 && m.RowCount == 4)
                    {
                        var sm = new Matrix4x4();

                        for (int x = 0; x < sm.Size; x++)
                            for (int y = 0; y < sm.Size; y++)
                                sm[x, y] = m[x, y]; //Wert kopieren

                        return sm;
                    }
                    else //falsche Abmessungen
                        throw new ArgumentException("Die angegebene Matrix war nicht in eine Matrix4x4 konvertierbar.");
                }

                /// <summary>
                /// Erstellt eine 4x4-Einheitsmatrix.
                /// </summary>
                /// <returns></returns>
                public static Matrix4x4 GetIdentity()
                {
                    return Matrix4x4.FromMatrix(SquareMatrix.GetIdentity(4));
                }

                /// <summary>
                /// Berechnet die Adjunkte zu dieser Matrix4x4.
                /// </summary>
                /// <returns></returns>
                public new Matrix4x4 GetAdjugate()
                {
                    return Matrix4x4.FromMatrix(base.GetAdjugate());
                }

                /// <summary>
                /// Berechnet die invertierte Matrix dieser Matrix4x4.
                /// </summary>
                /// <returns></returns>
                public new Matrix4x4 GetInverse()
                {
                    return Matrix4x4.FromMatrix(base.GetInverse());
                }

                /// <summary>
                /// Erstellt eine Matrix, die eine Rotation um die X-Achse darstellt.
                /// </summary>
                public static Matrix4x4 RotationX(double angle)
                {
                    //Variablen vorbereiten
                    var m = Matrix4x4.GetIdentity();
                    var sin = System.Math.Sin(angle);
                    var cos = System.Math.Cos(angle);

                    //Werte festlegen
                    m[1, 1] = cos;
                    m[2, 2] = cos;
                    m[2, 1] = -sin;
                    m[1, 2] = sin;

                    return m;
                }

                /// <summary>
                /// Erstellt eine Matrix, die eine Rotation um die Y-Achse darstellt.
                /// </summary>
                public static Matrix4x4 RotationY(double angle)
                {
                    //Variablen vorbereiten
                    var m = Matrix4x4.GetIdentity();
                    var sin = System.Math.Sin(angle);
                    var cos = System.Math.Cos(angle);

                    //Werte festlegen
                    m[0, 0] = cos;
                    m[2, 2] = cos;
                    m[2, 0] = sin;
                    m[0, 2] = -sin;

                    return m;
                }

                /// <summary>
                /// Erstellt eine Matrix, die eine Rotation um die Z-Achse darstellt.
                /// </summary>
                public static Matrix4x4 RotationZ(double angle)
                {
                    //Variablen vorbereiten
                    var m = Matrix4x4.GetIdentity();
                    var sin = System.Math.Sin(angle);
                    var cos = System.Math.Cos(angle);

                    //Werte festlegen
                    m[0, 0] = cos;
                    m[1, 1] = cos;
                    m[1, 0] = sin;
                    m[0, 1] = -sin;

                    return m;
                }

                /// <summary>
                /// Erstellt eine Matrix, die eine Rotation darstellt.
                /// </summary>
                public static Matrix4x4 Rotation(double angleX, double angleY, double angleZ)
                {
                    //Einzelrotationen berechnen
                    var mX = Matrix4x4.RotationX(angleX);
                    var mY = Matrix4x4.RotationY(angleY);
                    var mZ = Matrix4x4.RotationZ(angleZ);

                    return mX * mY * mZ; //Matrizen multiplizieren
                }

                /// <summary>
                /// Erstellt eine Matrix, die eine Rotation darstellt.
                /// </summary>
                public static Matrix4x4 Rotation(Vector3 v)
                {
                    return Matrix4x4.Rotation(v.X, v.Y, v.Z);
                }

                /// <summary>
                /// Erstellt eine Matrix, die eine Skalierung darstellt.
                /// </summary>
                public static Matrix4x4 Scalation(double scaleX, double scaleY, double scaleZ)
                {
                    var m = Matrix4x4.GetIdentity();

                    //Werte zuweisen
                    m[0, 0] = scaleX;
                    m[1, 1] = scaleY;
                    m[2, 2] = scaleZ;

                    return m;
                }

                /// <summary>
                /// Erstellt eine Matrix, die eine Skalierung darstellt.
                /// </summary>
                public static Matrix4x4 Scalation(double scale)
                {
                    return Matrix4x4.Scalation(scale, scale, scale);
                }

                /// <summary>
                /// Erstellt eine Matrix, die eine Translation darstellt.
                /// </summary>
                public static Matrix4x4 Translation(double x, double y, double z)
                {
                    var m = Matrix4x4.GetIdentity();

                    //Werte zuweisen
                    m[3, 0] = x;
                    m[3, 1] = y;
                    m[3, 2] = z;

                    return m;
                }

                /// <summary>
                /// Erstellt eine Matrix, die eine Translation darstellt.
                /// </summary>
                public static Matrix4x4 Translation(Vector3 v)
                {
                    return Matrix4x4.Translation(v.X, v.Y, v.Z);
                }

                /// <summary>
                /// Erstellt eine matrix, die eine Projektion darstellt.
                /// </summary>
                public static Matrix4x4 Projection(Vector3 viewPoint)
                {
                    var m = Matrix4x4.GetIdentity();

                    //Werte zuweisen
                    m[3, 3] = 0;
                    m[2, 0] = -(viewPoint.X / viewPoint.Z);
                    m[2, 1] = -(viewPoint.Y / viewPoint.Z);
                    m[2, 3] = 1.0 / viewPoint.Z;

                    return m;
                }

                /// <summary>
                /// Wendet eine Matrix4x4 auf einen Vector4 an.
                /// </summary>
                public static Vector4 Multiply(Matrix4x4 m, Vector4 v)
                {
                    //Matrix mit Vektor multiplizieren
                    var resultMatrix = m * v.ToVerticalMatrix(); //Vektor in Matrix umwandeln

                    return new Vector4(resultMatrix[0, 0], resultMatrix[0, 1], resultMatrix[0, 2], resultMatrix[0, 3]); //neuen Vektor bilden
                }

                public static Matrix4x4 operator *(Matrix4x4 value, double skalar)
                {
                    return Matrix4x4.FromMatrix(Multiply(value, skalar));
                }

                public static Matrix4x4 operator *(double skalar, Matrix4x4 value)
                {
                    return Matrix4x4.FromMatrix(Multiply(value, skalar));
                }

                public static Matrix4x4 operator *(Matrix4x4 left, Matrix4x4 right)
                {
                    return Matrix4x4.FromMatrix(Multiply(left, right));
                }

                public static Vector4 operator *(Matrix4x4 m, Vector4 v)
                {
                    return Multiply(m, v);
                }

                public static Vector4 operator *(Vector4 v, Matrix4x4 m)
                {
                    return Multiply(m, v);
                }

                public static Matrix4x4 operator +(Matrix4x4 left, Matrix4x4 right)
                {
                    return Matrix4x4.FromMatrix(Add(left, right));
                }
            }
        }
    }
}
