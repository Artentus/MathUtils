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
            /// Stellt eine lineare Gleichung dar.
            /// </summary>
            public sealed class LinearEquation
            {
                /// <summary>
                /// Die Koeffizienten dieser linearen Gleichung.
                /// </summary>
                public List<double> Coefficients { get; private set; }

                /// <summary>
                /// Die Konstante dieser linearen Gleichung.
                /// </summary>
                public double Constant { get; set; }

                public LinearEquation()
                {
                    Coefficients = new List<double>();
                }
            }
        }
    }
}
