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
            /// Stellt ein lineares Gleichungssystem dar.
            /// </summary>
            public sealed class LinearSystem
            {
                /// <summary>
                /// Die Gleichungen in diesem Gleichungssystem.
                /// </summary>
                public List<LinearEquation> Equations { get; private set; }

                public LinearSystem()
                {
                    Equations = new List<LinearEquation>();
                }

                /// <summary>
                /// Löst dieses Gleichungssystem.
                /// </summary>
                /// <returns></returns>
                public double[] Solve()
                {
                    //Prüfen, ob das System die Bedingungen erfüllt
                    if (!Equations.All((item) => item.Coefficients.Count == Equations.Count))
                        throw new ArgumentException("Dieses Gleichungssystem ist nicht gültig.");

                    //Matrix erstellen
                    var left = new SquareMatrix(Equations.Count);
                    var right = new double[Equations.Count];
                    for (int i = 0; i < Equations.Count; i++)
                    {
                        for (int j = 0; j < Equations.Count; j++)
                            left[j, i] = Equations[i].Coefficients[j];

                        right[i] = Equations[i].Constant;
                    }

                    return SquareMatrix.SolveLinearSystem(left, right);
                }
            }
        }
    }
}
