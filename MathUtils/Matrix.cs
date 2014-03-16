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
            /// Repräsentiert eine Matrix.
            /// </summary>
            public class Matrix : IEnumerable<double>, ICloneable, IEquatable<Matrix>
            {
                double[,] values;
                
                /// <summary>
                /// Gibt die Anzahl der Spalten dieser Matrix an.
                /// </summary>
                public int ColumnCount { get; private set; }

                /// <summary>
                /// Gibt die Anzahl der Zeilen dieser Matrix an.
                /// </summary>
                public int RowCount { get; private set; }

                /// <summary>
                /// Gibt der Wert an der angegebenen Position in der Matrix zurück oder legt diesen fest.
                /// </summary>
                /// <param name="x"></param>
                /// <param name="y"></param>
                /// <returns></returns>
                public double this[int x, int y]
                {
                    get
                    {
                        return values[x, y];
                    }
                    set
                    {
                        values[x, y] = value;
                    }
                }

                /// <summary>
                /// Erstellt eine neue Matrix.
                /// </summary>
                /// <param name="columns">Die Spaltenanzahl der Matrix.</param>
                /// <param name="rows">Die Zeilenanzahl der Matrix.</param>
                public Matrix(int columns, int rows)
                {
                    ColumnCount = columns;
                    RowCount = rows;             
                    values = new double[columns, rows];
                }

                public override string ToString()
                {
                    var s = "{"; //öffnende Klammer

                    for (int y = 0; y < RowCount; y++)
                    {
                        if (y > 0) //Zeilenumbruch nach jeder Zeile
                            s += '\n';

                        for (int x = 0; x < ColumnCount; x++)
                        {
                            s += this[x, y].ToString();
                            
                            if (!(y == (RowCount - 1) && x == (ColumnCount -1)))
                                s += ", ";
                        }
                    }

                    s += '}'; //schließende Klammer

                    return s;
                }

                /// <summary>
                /// Erstellt die transponierte Matrix zu dieser Matrix.
                /// </summary>
                public Matrix GetTranspose()
                {
                    //Matrix mit umgekehrten Abmessungen erstellen
                    var m = new Matrix(RowCount, ColumnCount);

                    for (int x = 0; x < ColumnCount; x++)
                        for (int y = 0; y < RowCount; y++)
                            m[y, x] = this[x, y]; //Koordinaten tauschen

                    return m;
                }

                /// <summary>
                /// Prüft, ob zwei Matrizen vom selben Typ sind.
                /// </summary>
                /// <param name="left"></param>
                /// <param name="right"></param>
                /// <returns></returns>
                public static bool CheckForSameType(Matrix left, Matrix right)
                {
                    return left.ColumnCount == right.ColumnCount && left.RowCount == right.RowCount;
                }

                /// <summary>
                /// Erstellt die um die angegebene Spalte und Zeile verringerte Matrix dieser Matrix.
                /// </summary>
                /// <param name="column"></param>
                /// <param name="row"></param>
                /// <returns></returns>
                public Matrix GetSubMatrix(int column, int row)
                {
                    var m = new Matrix(ColumnCount - 1, RowCount - 1); //Matrix mit reduzierter Größe erstellen

                    int x = 0;
                    for (int i = 0; i < ColumnCount; i++)
                        if (i != column) //prüfen, ob Spalte auslassen
                        {
                            int y = 0;
                            for (int j = 0; j < RowCount; j++)
                                if (j != row) //prüfen, ob Zeile auslassen
                                {
                                    m[x, y] = this[i, j];
                                    y++;
                                }
                            x++;
                        }

                    return m;
                }

                /// <summary>
                /// Multipliziert eine Matrix mit einem Skalrafaktor.
                /// </summary>
                /// <param name="value"></param>
                /// <param name="skalar"></param>
                /// <returns></returns>
                public static Matrix Multiply(Matrix value, double skalar)
                {
                    var m = new Matrix(value.ColumnCount, value.RowCount); //neue Matrix erstellen

                    for (int x = 0; x < value.ColumnCount; x++)
                        for (int y = 0; y < value.RowCount; y++)
                            m[x, y] = value[x, y] * skalar; //mit Skalarfaktor multiplizieren

                    return m;
                }

                /// <summary>
                /// Multipliziert zwei Matrizen miteinander, sofern diese kompatibel sind.
                /// </summary>
                /// <param name="left"></param>
                /// <param name="right"></param>
                /// <returns></returns>
                public static Matrix Multiply(Matrix left, Matrix right)
                {
                    if (left.ColumnCount == right.RowCount)
                    {
                        var m = new Matrix(right.ColumnCount, left.RowCount); //Matrix mit richtigen Abmessungen erstellen

                        for (int x = 0; x < m.ColumnCount; x++)
                            for (int y = 0; y < m.RowCount; y++)
                            {
                                var value = 0.0;
                                for (int i = 0; i < left.ColumnCount; i++)
                                    value += left[i, y] * right[x, i]; //Wert für diese Position berechnen

                                m[x, y] = value; //Wert zuweisen
                            }

                        return m;
                    }
                    else //Matrizen sind nicht kompatibel
                        throw new ArgumentException("Diese Matrizen können nicht miteinander multipliziert werden.");
                }

                /// <summary>
                /// Addiert zwei Matrizen miteinander, sofern sie kompatibel sind.
                /// </summary>
                /// <param name="left"></param>
                /// <param name="right"></param>
                /// <returns></returns>
                public static Matrix Add(Matrix left, Matrix right)
                {
                    if (Matrix.CheckForSameType(left, right))
                    {
                        var m = new Matrix(left.ColumnCount, left.RowCount); //neue Matrix erstellen

                        for (int x = 0; x < m.ColumnCount; x++)
                            for (int y = 0; y < m.RowCount; y++)
                                m[x, y] = left[x, y] + right[x, y]; //Werte addieren

                        return m;
                    }
                    else //Matrizen sind nicht kompatibel
                        throw new ArgumentException("Diese Matrizen können nicht addiert werden.");
                }

                internal void MultiplyRow(int row, double factor)
                {
                    for (int x = 0; x < ColumnCount; x++)
                        this[x, row] *= factor;
                }

                internal void SubtractRows(int row1, int row2, double factor)
                {
                    for (int x = 0; x < ColumnCount; x++)
                        this[x, row1] -= this[x, row2] * factor;
                }

                internal void ChangeRows(int row1, int row2)
                {
                    for (int x = 0; x < ColumnCount; x++)
                    {
                        var tempVal = this[x, row1];
                        this[x, row1] = this[x, row2];
                        this[x, row2] = tempVal;
                    }
                }

                public static Matrix operator *(Matrix value, double skalar)
                {
                    return Multiply(value, skalar);
                }

                public static Matrix operator *(double skalar, Matrix value)
                {
                    return Multiply(value, skalar);
                }

                public static Matrix operator *(Matrix left, Matrix right)
                {
                    return Multiply(left, right);
                }

                public static Matrix operator +(Matrix left, Matrix right)
                {
                    return Add(left, right);
                }

                public IEnumerator<double> GetEnumerator()
                {
                    return new MatrixEnumerator(this);
                }

                System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
                {
                    return new MatrixEnumerator(this);
                }

                internal class MatrixEnumerator : IEnumerator<double>
                {
                    Matrix m;
                    int index;

                    internal MatrixEnumerator(Matrix m)
                    {
                        this.m = m;
                        index = -1;
                    }

                    public double Current
                    {
                        get
                        {
                            //Koordinaten berechnen
                            var y = index % m.ColumnCount;
                            var x = index / m.RowCount;

                            return m[x, y];
                        }
                    }

                    public void Dispose() { }

                    object System.Collections.IEnumerator.Current
                    {
                        get
                        {
                            return Current;
                        }
                    }

                    public bool MoveNext()
                    {
                        index++;
                        return index < m.ColumnCount + m.RowCount;
                    }

                    public void Reset()
                    {
                        index = -1;
                    }
                }

                public virtual object Clone()
                {
                    var m = new Matrix(ColumnCount, RowCount); //neue Matrix erstellen

                    //alle Werte kopieren
                    for (int x = 0; x < ColumnCount; x++)
                        for (int y = 0; y < RowCount; y++)
                            m[x, y] = this[x, y];

                    return m;
                }

                public bool Equals(Matrix other)
                {
                    //wenn nícht von gleichem Typ dann false
                    if (!Matrix.CheckForSameType(this, other))
                        return false;

                    //alle Werte prüfen
                    for (int x = 0; x < ColumnCount; x++)
                        for (int y = 0; y < RowCount; y++)
                            if (this[x, y] != other[x, y])
                                return false;

                    return true;
                }
            }
        }
    }
}
