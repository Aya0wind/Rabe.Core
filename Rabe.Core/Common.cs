using System.Runtime.InteropServices;
using System.Text;

namespace Rabe.Core;

internal static class Common
{
    public static string? ReadAsString(this IntPtr ptr)
    {
        return Marshal.PtrToStringAnsi(ptr!);
    }

    public static byte[] ToByteArrayAndFree(this CBoxedBuffer buffer)
    {
        if (buffer.buffer == IntPtr.Zero)
            throw Common.GetLastWrappedException();
        var managedBuffer = new byte[buffer.len];
        Marshal.Copy(buffer.buffer, managedBuffer, 0, (int)buffer.len);
        RabeNative.free_boxed_buffer(buffer);
        return managedBuffer;
    }

    //free cboxed buffer
    public static void FreeBuffer(this CBoxedBuffer buffer)
    {
        RabeNative.free_boxed_buffer(buffer);
    }

    //conver string to byte array
    public static byte[] ToByteArray(this string str)
    {
        return Encoding.Default.GetBytes(str);
    }

    public static RabeException GetLastWrappedException()
    {
        IntPtr messagePtr = RabeNative.get_thread_last_error();
        string message = Marshal.PtrToStringAnsi(messagePtr)!;
        RabeNative.free_json(messagePtr);
        return new RabeException(message);
    }

}

public class RabeException : Exception
{
    public RabeException(string message) : base(message) { }
}

public class NativeString : NativeObject
{
    public NativeString(IntPtr handle) : base(handle)
    {
    }
    protected override void FreeHandle(IntPtr handle)
    {
        RabeNative.free_json(handle);
    }
}

public static class NativeStringExtensions
{
    public static NativeString ToNativeString(this IntPtr obj)
    {
        if (obj == IntPtr.Zero)
            throw Common.GetLastWrappedException();
        return new NativeString(obj);
    }
}
