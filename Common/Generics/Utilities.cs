using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Generics
{
    public class Utilities<T> where T : IComparable
    {
        public T Max(T a, T b)
        {
            return a.CompareTo(b) > 0 ? a : b;
        }
    }
}
