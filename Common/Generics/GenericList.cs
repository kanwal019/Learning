using System.Collections.Generic;

namespace Common.Generics;

public class GenericList<T>
{
    private readonly List<T> items;

    public GenericList(int capacity = 0)
    {
        // C# 15 passes the constructor argument through the collection expression.
        items = [with(capacity: capacity)];
    }

    public int Count => items.Count;

    public void Add(T value) => items.Add(value);

    public T this[int index] => items[index];
}
