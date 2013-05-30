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
            internal class VectorEnumerator : IEnumerator<double>
            {
                private IVector v;
                private int index;

                internal VectorEnumerator(IVector v)
                {
                    this.v = v;
                    index = -1;
                }

                public double Current
                {
                    get
                    {
                        return v[index];
                    }
                }

                public void Dispose() { }

                object System.Collections.IEnumerator.Current
                {
                    get
                    {
                        return v[index];
                    }
                }

                public bool MoveNext()
                {
                    index++;
                    return index < v.Dimension;
                }

                public void Reset()
                {
                    index = -1;
                }
            }
        }
    }
}
