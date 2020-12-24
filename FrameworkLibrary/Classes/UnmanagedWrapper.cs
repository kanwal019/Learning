using System;
using System.Runtime.InteropServices;
using CustomMathEnums = FrameworkLibrary.Helpers.Enums.CustomMathEnums;

namespace FrameworkLibrary.Classes
{
    class UnmanagedWrapper
    {
        [DllImport("ClassicLibrary.dll")]
        public static extern IntPtr new_CustomMathInstance(int input);

        [DllImport("ClassicLibrary.dll")]
        public static extern void delete_CustomMathInstance(IntPtr instance);

        [DllImport("ClassicLibrary.dll")]
        public static extern int getValue(IntPtr instance, CustomMathEnums.MethodType selectedMethod);
    }
}
