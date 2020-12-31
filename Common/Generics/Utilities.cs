using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Generics
{
    public class Utilities<T> where T : IComparable, new()
    {
        public T Max(T a, T b)
        {
            return a.CompareTo(b) > 0 ? a : b;
        }

        public void Instantiate(T value)
        {
            var obj = new T();
        }
    }
}
