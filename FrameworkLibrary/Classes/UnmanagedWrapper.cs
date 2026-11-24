using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using CustomMathEnums = FrameworkLibrary.Helpers.Enums.CustomMathEnums;

namespace FrameworkLibrary.Classes;

internal static partial class UnmanagedWrapper
{
    private const string LibraryName = "ClassicLibrary.dll";

    [LibraryImport(LibraryName, EntryPoint = "new_CustomMathInstance")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial CustomMathHandle Create(int input);

    [LibraryImport(LibraryName, EntryPoint = "delete_CustomMathInstance")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void Delete(IntPtr instance);

    [LibraryImport(LibraryName, EntryPoint = "getValue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetValue(CustomMathHandle instance, CustomMathEnums.MethodType selectedMethod);
}

internal sealed class CustomMathHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    // Required by the generated SafeHandle marshaller.
    public CustomMathHandle() : base(ownsHandle: true) { }

    protected override bool ReleaseHandle()
    {
        UnmanagedWrapper.Delete(handle);
        return true;
    }
}
