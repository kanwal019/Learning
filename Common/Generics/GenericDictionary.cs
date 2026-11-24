using System.Collections.Generic;

namespace Common.Generics;

public class GenericDictionary<TKey, TValue> where TKey : notnull
{
    private readonly Dictionary<TKey, TValue> items;

    public GenericDictionary(IEqualityComparer<TKey> comparer = null)
    {
        // Preserve the caller's key comparison rules in a C# 15 collection expression.
        items = [with(comparer: comparer)];
    }

    public int Count => items.Count;

    public void Add(TKey key, TValue value) => items.Add(key, value);

    public TValue this[TKey key] => items[key];

    public bool TryGetValue(TKey key, out TValue value) => items.TryGetValue(key, out value);
}
