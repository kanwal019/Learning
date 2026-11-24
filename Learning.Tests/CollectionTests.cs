using Common.Generics;
using Xunit;

namespace Learning.Tests;

public class CollectionTests
{
    [Fact]
    public void ListGrowsBeyondInitialCapacityAndPreservesOrder()
    {
        var list = new GenericList<string>(capacity: 1);
        list.Add("first");
        list.Add("second");
        Assert.Equal(2, list.Count);
        Assert.Equal("first", list[0]);
        Assert.Equal("second", list[1]);
    }

    [Fact]
    public void ListSupportsDefaultCapacityAndRejectsInvalidIndexes()
    {
        var list = new GenericList<int>();
        Assert.Equal(0, list.Count);
        Assert.Throws<ArgumentOutOfRangeException>(() => list[-1]);
        Assert.Throws<ArgumentOutOfRangeException>(() => list[0]);
        Assert.Throws<ArgumentOutOfRangeException>(() => new GenericList<int>(-1));
    }

    [Fact]
    public void DictionaryUsesTheSuppliedComparerForLookupAndDuplicateDetection()
    {
        var dictionary = new GenericDictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        dictionary.Add("Book", 42);
        Assert.Equal(1, dictionary.Count);
        Assert.Equal(42, dictionary["BOOK"]);
        Assert.True(dictionary.TryGetValue("book", out var value));
        Assert.Equal(42, value);
        Assert.Throws<ArgumentException>(() => dictionary.Add("bOoK", 43));
        Assert.False(dictionary.TryGetValue("missing", out _));
    }

    [Fact]
    public void DictionaryKeepsDefaultCaseSensitiveComparison()
    {
        var dictionary = new GenericDictionary<string, int>();
        dictionary.Add("book", 1);
        dictionary.Add("BOOK", 2);
        Assert.Equal(2, dictionary.Count);
        Assert.Equal(1, dictionary["book"]);
        Assert.Throws<KeyNotFoundException>(() => dictionary["missing"]);
    }
}
