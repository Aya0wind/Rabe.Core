using System.Runtime.InteropServices;
using System.Text;

namespace Rabe;

public static class Common
{
    public static string? ReadAsString(this IntPtr ptr)
    {
        return Marshal.PtrToStringAnsi(ptr!);
    }

    public static byte[] ToByteArrayAndFree(this CBoxedBuffer buffer)
    {
        if (buffer.buffer == IntPtr.Zero)
            throw new Exception("Decryption failed");
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
    
}