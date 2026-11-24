using System;
using CustomMathEnums = FrameworkLibrary.Helpers.Enums.CustomMathEnums;

namespace FrameworkLibrary.Classes;

public class CustomMath : IDisposable
{
    private readonly CustomMathHandle instance;

    public CustomMath(int input)
    {
        instance = UnmanagedWrapper.Create(input);
        if (instance.IsInvalid)
        {
            instance.Dispose();
            throw new InvalidOperationException("The native CustomMath instance could not be created.");
        }
    }

    public int GetValue(CustomMathEnums.MethodType methodName)
    {
        ObjectDisposedException.ThrowIf(instance.IsClosed, this);
        return UnmanagedWrapper.GetValue(instance, methodName);
    }

    public void Delete() => Dispose();

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            instance.Dispose();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
