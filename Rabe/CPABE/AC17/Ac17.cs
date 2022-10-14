using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace Rabe.CPABE.AC17;
public static class Context
{
    public static ValueTuple<MasterKey, PublicKey> Init()
    {
        var ac17SetupResult = RabeNative.ac17_init();
        return ValueTuple.Create(new MasterKey(ac17SetupResult.master_key), new PublicKey(ac17SetupResult.public_key));
    }
}
[JsonConverter(typeof(PublicKeyJsonConverter))]
public class PublicKey : NativeObject
{
    public PublicKey(IntPtr handle):base(handle)
    {
    }
    
    protected override void FreeHandle(IntPtr handle)
    {
        RabeNative.ac17_free_public_key(handle);
    }
}

[JsonConverter(typeof(MasterKeyJsonConverter))]
public class MasterKey : NativeObject
{
    public MasterKey(IntPtr handle):base(handle)
    {
    }
    protected override void FreeHandle(IntPtr handle)
    {
        RabeNative.ac17_free_master_key(handle);
    }
}


[JsonConverter(typeof(CipherJsonConverter))]
public class Cipher : NativeObject
{
    public Cipher(IntPtr handle):base(handle)
    {
    }
    protected override void FreeHandle(IntPtr handle)
    {
        RabeNative.cp_ac17_free_cipher(handle);
    }
}

[JsonConverter(typeof(SecretKeyJsonConverter))]
public class SecretKey : NativeObject
{
    public SecretKey(IntPtr handle):base(handle)
    {
    }
    
    public byte[] Decrypt(Cipher cipher)
    {
        var result = RabeNative.cp_ac17_decrypt(cipher.Handle, Handle);
        if (result.buffer == IntPtr.Zero)
            throw new Exception("Decryption failed");
        var buffer = new byte[result.len];
        Marshal.Copy(result.buffer, buffer, 0, buffer.Length);
        RabeNative.free_boxed_buffer(result);
        return buffer;
    }

    protected override void FreeHandle(IntPtr handle)
    {
        RabeNative.cp_ac17_free_secret_key(handle);
    }
}

public static class Extension
{
    public static Cipher Encrypt(this PublicKey publicKey, string policy, byte[] text)
    {
        var cipher = RabeNative.cp_ac17_encrypt(publicKey.Handle, policy, text, (UIntPtr)text.Length);
        if (cipher == IntPtr.Zero)
            throw new Exception("Encryption failed");
        return new Cipher(cipher);
    }

    public static SecretKey KeyGen(this MasterKey masterKey, IEnumerable<string> attributes)
    {
        var attributeArray = attributes as string[] ?? attributes.ToArray();
        var secretKey = RabeNative.cp_ac17_generate_secret_key(
            masterKey.Handle, 
            attributeArray, 
            (UIntPtr)attributeArray.Length
            );
        if (secretKey == IntPtr.Zero)
            throw new Exception("KeyGen failed");
        return new SecretKey(secretKey);
    }
}