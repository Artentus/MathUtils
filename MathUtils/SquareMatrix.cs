using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Artentus
{
    namespace Utils
    {
        namespace Math
        {
            /// <summary>
            /// Repräsentiert eine quadratische Matrix.
            /// </summary>
            public class SquareMatrix : Matrix
            {
                /// <summary>
                /// Gibt die Größe dieser quadratischen Matrix an.
                /// </summary>
                public int Size { get; protected set; }

                /// <summary>
                /// Gibt an, ob diese quadratische Matrix singulär ist.
                /// Eine singuläre Matrix besitzt keine Inverse.
                /// </summary>
                public bool IsSingular
                {
                    get
                    {
                        return GetDeterminant() == 0;
                    }
                }

                /// <summary>
                /// Erstellt eine neue quadratische Matrix.
                /// </summary>
                /// <param name="size">Die Größe der Matrix.</param>
                public SquareMatrix(int size)
                    : base(size, size)
                {
                    Size = size;
                }

                /// <summary>
                /// Erstellt aus der angegebenen Matrix eine quadratische Matrix, sofern die Abmessungen stimmen.
                /// </summary>
                /// <param name="m"></param>
                public static SquareMatrix FromMatrix(Matrix m)
                {
                    if (m.ColumnCount == m.RowCount)
                    {
                        var sm = new SquareMatrix(m.ColumnCount);

                        for (int x = 0; x < sm.Size; x++)
                            for (int y = 0; y < sm.Size; y++)
                                sm[x, y] = m[x, y]; //Wert kopieren

                        return sm;
                    }
                    else //falsche Abmessungen
                        throw new ArgumentException("Die angegebene Matrix war nicht in eine quadratische Matrix konvertierbar.");
                }

                /// <summary>
                /// Erstellt eine Einheitsmatrix mit der angegebenen Größe.
                /// </summary>
                /// <param name="size"></param>
                /// <returns></returns>
                public static SquareMatrix GetIdentity(int size)
                {
                    var m = new SquareMatrix(size); //Matrix erstellen

                    //Diagonale auf 1 setzen
                    for (int i = 0; i < size; i++)
                        m[i, i] = 1; 

                    return m;
                }

                /// <summary>
                /// Berechnet die Determinante dieser quadratischen Matrix.
                /// </summary>
                /// <returns></returns>
                public double GetDeterminant()
                {
                    return GetDeterminant(this);
                }

                private static double GetDeterminant(SquareMatrix m)
                {
                    if (m.Size == 1) //bei größe 1 ist die Determinante der einzige Wert
                        return m[0, 0];

                    var det = 0.0;
                    for (int i = 0; i < m.Size; i++)
                    {
                        //Vorzeichen beachten
                        var factor = 1;
                        if (i % 2 == 0)
                            factor = -1;

                        //reduzierte Matrix bilden
                        var reduced = SquareMatrix.FromMatrix(m.GetSubMatrix(i, 0));

                        //Determinante berechnen
                        det += m[i, 0] * factor * reduced.GetDeterminant();
                    }

                    return det;
                }

                /// <summary>
                /// Berechnet die Adjunkte zu dieser quadratischen Matrix.
                /// </summary>
                /// <returns></returns>
                public SquareMatrix GetAdjugate()
                {
                    var m = new SquareMatrix(Size); //neue Matrix erstellen

                    for (int x = 0; x < Size; x++)
                        for (int y = 0; y < Size; y++)
                        {
                            //Vorzeichen beachten
                            var factor = 1;
                            if ((x + y) % 2 == 0)
                                factor = -1;

                            //reduzierte Matrix bilden
                            var reduced = SquareMatrix.FromMatrix(GetSubMatrix(x, y));

                            //Wert berechnen
                            m[x, y] = reduced.GetDeterminant() * factor;
                        }

                    return SquareMatrix.FromMatrix(m.GetTranspose());
                }

                private Matrix GaussJordan(Matrix input)
                {
                    var myClone = (SquareMatrix)this.Clone();

                    //Zeilen voneinander subtrahieren
                    for (int x = 0; x < ColumnCount; x++)
                    {
                        //Zeilen tauschen, wenn erforderlich
                        var next = x;
                        while (myClone[x, x] == 0) //nächste gültige Zeile ermitteln
                        {
                            next++;

                            if (next >= Size) //keine gültige Zeile vorhanden
                                throw new InvalidOperationException("Dieses Gleichungsystem kann nicht aufgelöst werden.");

                            if (myClone[next, x] != 0)
                            {
                                //aktuelle Zeile mit gültiger Zeile austauschen
                                myClone.ChangeRows(x, next);
                                input.ChangeRows(x, next);
                            }
                        }

                        //Subtrahieren
                        for (int y = 0; y < RowCount; y++)
                            if (x != y) //Diagonale auslassen
                                if (myClone[x, y] != 0) //bei Zellenwert 0 keine Berechnung erforderlich
                                {
                                    var f = myClone[x, y] / myClone[x, x]; //Faktor berechnen
                                    myClone.SubtractRows(y, x, f);
                                    input.SubtractRows(y, x, f);
                                }
                        }

                    //Diagonale auf 1 setzen
                    for (int i = 0; i < Size; i++)
                    {
                        //Zeile so multiplizieren, dass (i,i) 1 wird
                        var f = 1 / myClone[i, i];
                        myClone.MultiplyRow(i, f);
                        input.MultiplyRow(i, f);
                    }

                    return input;
                }

                /// <summary>
                /// Berechnet die invertierte Matrix dieser quadratischen Matrix.
                /// </summary>
                /// <returns></returns>
                public SquareMatrix GetInverse()
                {
                    return (SquareMatrix)GaussJordan(SquareMatrix.GetIdentity(Size));
                }

                internal static double[] SolveLinearSystem(SquareMatrix left, double[] right)
                {
                    var rightMatrix = new Matrix(1, right.Length);
                    for (int i = 0; i < right.Length; i++)
                        rightMatrix[0, i] = right[i];

                    var resultMatrix = left.GaussJordan(rightMatrix);

                    var result = new double[right.Length];
                    for (int i = 0; i < right.Length; i++)
                        result[i] = resultMatrix[0, i];

                    return result;
                }

                public override object Clone()
                {
                    var m = new SquareMatrix(Size); //neue Matrix erstellen

                    //alle Werte kopieren
                    for (int x = 0; x < ColumnCount; x++)
                        for (int y = 0; y < RowCount; y++)
                            m[x, y] = this[x, y];

                    return m;
                }

                public static SquareMatrix operator *(SquareMatrix value, double skalar)
                {
                    return SquareMatrix.FromMatrix(Multiply(value, skalar));
                }

                public static SquareMatrix operator *(double skalar, SquareMatrix value)
                {
                    return SquareMatrix.FromMatrix(Multiply(value,skalar));
                }

                public static SquareMatrix operator *(SquareMatrix left, SquareMatrix right)
                {
                    return SquareMatrix.FromMatrix(Multiply(left, right));
                }

                public static SquareMatrix operator +(SquareMatrix left, SquareMatrix right)
                {
                    return SquareMatrix.FromMatrix(Add(left, right));
                }
            }
        }
    }
}
