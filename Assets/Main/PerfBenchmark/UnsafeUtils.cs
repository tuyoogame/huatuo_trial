using System;
using System.Runtime.InteropServices;

namespace Huatuo.Perf
{
    public static class UnsafeUtils
    {
        public static unsafe void* Malloc(int size, int alignment, out void* pointer)
        {
            IntPtr aligned = IntPtr.Zero;

            if (alignment > 8)
            {
                pointer = (void*)Marshal.AllocHGlobal(size + (alignment - 8));
                aligned = new IntPtr(alignment * (((long)pointer + (alignment - 1)) / alignment));
            }
            else
            {
                aligned = Marshal.AllocHGlobal(size);
                pointer = (void*)aligned;
            }

            return (void*)aligned;
        }

        public static unsafe void Free(void* pointer)
        {
            Marshal.FreeHGlobal((IntPtr)pointer);
        }
    }
}
