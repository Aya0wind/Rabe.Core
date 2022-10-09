using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace Rabe.KPABE;

[JsonConverter(typeof(KpSecretKeyJsonConverter))]
public class SecretKey : IDisposable
{
    public SecretKey(IntPtr handle)
    {
        Handle = handle;
    }

    public IntPtr Handle { get; private set; }

    //This is the public method, it will HOPEFULLY but
    //not always be called by users of the class
    public void Dispose()
    {
        //indicate this was NOT called by the Garbage collector
        if (Handle != IntPtr.Zero)
        {
            NativeLibKp.FreeSecKey(Handle);
            //preserve the handle in case of double dispose when gc call finalizer
            Handle = IntPtr.Zero;
            //clear up any unmanaged resources - this is safe to
            //put outside the disposing check because if the user
            //called dispose we want to also clean up unmanaged
            //resources, if the GC called Dispose then we only
            //want to clean up managed resources
        }

        //Now we have disposed of all our resources, the GC does not
        //need to do anything, stop the finalizer being called
        GC.SuppressFinalize(this);
    }

    //Finalize method for the object, will call Dispose for us
    //to clean up the resources if the user has not called it
    ~SecretKey()
    {
        //indicate this was NOT called by the Garbage collector
        if (Handle != IntPtr.Zero)
            //after this call the handle will not be use, so no need to set it to zero
            NativeLibKp.FreeSecKey(Handle);
    }


    public byte[] Decrypt(Cipher cipher)
    {
        var result = NativeLibKp.Decrypt(cipher.Handle, Handle);
        if (result.buffer == IntPtr.Zero)
            throw new Exception("Decryption failed");
        var buffer = new byte[result.len.ToUInt32()];
        Marshal.Copy(result.buffer, buffer, 0, buffer.Length);
        NativeLibCommon.FreeDecryptResult(result);
        return buffer;
    }
}

[JsonConverter(typeof(KpCipherJsonConverter))]
public class Cipher : IDisposable
{
    public Cipher(IntPtr handle)
    {
        Handle = handle;
    }

    public IntPtr Handle { get; private set; }

    //This is the public method, it will HOPEFULLY but
    //not always be called by users of the class
    public void Dispose()
    {
        //indicate this was NOT called by the Garbage collector
        if (Handle != IntPtr.Zero)
        {
            NativeLibKp.FreeCipher(Handle);
            //preserve the handle in case of double dispose when gc call finalizer
            Handle = IntPtr.Zero;
            //clear up any unmanaged resources - this is safe to
            //put outside the disposing check because if the user
            //called dispose we want to also clean up unmanaged
            //resources, if the GC called Dispose then we only
            //want to clean up managed resources
        }

        //Now we have disposed of all our resources, the GC does not
        //need to do anything, stop the finalizer being called
        GC.SuppressFinalize(this);
    }

    //Finalize method for the object, will call Dispose for us
    //to clean up the resources if the user has not called it
    ~Cipher()
    {
        //indicate this was NOT called by the Garbage collector
        if (Handle != IntPtr.Zero)
            //after this call the handle will not be use, so no need to set it to zero
            NativeLibKp.FreeCipher(Handle);
    }
}

public static class Extension
{
    public static Cipher Encrypt(this PublicKey publicKey, string[] attrs, byte[] text)
    {
        var cipher = NativeLibKp.Encrypt(publicKey.Handle, attrs, (UIntPtr)attrs.Length, text, (UIntPtr)text.Length);
        if (cipher == IntPtr.Zero)
            throw new Exception("Encryption failed");
        return new Cipher(cipher);
    }

    public static SecretKey KeyGen(this MasterKey masterKey, string policy)
    {
        var secretKey = NativeLibKp.GenerateSecKey(masterKey.Handle, policy);
        if (secretKey == IntPtr.Zero)
            throw new Exception("KeyGen failed");
        return new SecretKey(secretKey);
    }
}