using System;
using CustomMathEnums = FrameworkLibrary.Helpers.Enums.CustomMathEnums;

namespace FrameworkLibrary.Classes
{
    public class CustomMath : IDisposable
    {
        bool disposed;
        private IntPtr native_instance;

        public CustomMath(int input)
        {
            native_instance = UnmanagedWrapper.new_CustomMathInstance(input);
        }

        public int GetValue(CustomMathEnums.MethodType methodName)
        {
            return UnmanagedWrapper.getValue(native_instance, methodName);
        }

        public void Delete()
        {
            UnmanagedWrapper.delete_CustomMathInstance(native_instance);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    Delete();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
