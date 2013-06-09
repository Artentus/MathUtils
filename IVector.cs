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
            public interface IVector : IEnumerable<double>, IEquatable<IVector>
            {
                /// <summary>
                /// Gibt die Koordinate an dem angegebenen Index zurück oder legt diese fest.
                /// </summary>
                /// <returns></returns>
                double this[int index] { get; set; }

                /// <summary>
                /// Gibt die Dimension dieses Vektors an.
                /// </summary>
                int Dimension { get; }
            }
        }
    }
}
