using FrameworkLibrary.Classes;
using FrameworkLibrary.Helpers.Enums;
using Xunit;

namespace Learning.Tests;

public class NativeInteropTests
{
    [Theory]
    [InlineData(CustomMathEnums.MethodType.Square, 5, 25)]
    [InlineData(CustomMathEnums.MethodType.SquareRoot, 25, 5)]
    [InlineData(CustomMathEnums.MethodType.Factorial, 5, 120)]
    public void GeneratedImportsReturnTheNativeResult(CustomMathEnums.MethodType method, int input, int expected)
    {
        using var math = new CustomMath(input);
        Assert.Equal(expected, math.GetValue(method));
    }

    [Fact]
    public void DeleteAndDisposeAreIdempotentAndRejectFurtherCalls()
    {
        var math = new CustomMath(5);
        math.Delete();
        math.Delete();
        math.Dispose();
        Assert.Throws<ObjectDisposedException>(() => math.GetValue(CustomMathEnums.MethodType.Square));
    }
}
