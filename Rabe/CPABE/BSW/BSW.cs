using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace Rabe.CPABE.BSW;

public static class Context
{
    public static ValueTuple<MasterKey,PublicKey> Init()
    {
        var setupResult = RabeNative.bsw_init();
        return ValueTuple.Create(new MasterKey(setupResult.master_key), new PublicKey(setupResult.public_key));
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
        RabeNative.cp_bsw_free_public_key(handle);
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
        RabeNative.cp_bsw_free_master_key(handle);
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
        RabeNative.cp_bsw_free_ciphertext(handle);
    }
}

[JsonConverter(typeof(SecretKeyJsonConverter))]
public class SecretKey : NativeObject
{
    public SecretKey(IntPtr handle):base(handle)
    {
    }
    protected override void FreeHandle(IntPtr handle)
    {
        RabeNative.cp_bsw_free_secret_key(handle);
    }
}

public static class Extension
{
    public static Cipher Encrypt(this PublicKey publicKey, string policy, byte[] text)
    {
        var cipher = RabeNative.cp_bsw_encrypt(publicKey.Handle, policy, text, (UIntPtr)text.Length);
        if (cipher == IntPtr.Zero)
            throw new Exception("Encryption failed");
        return new Cipher(cipher);
    }

    public static byte[] Decrypt(this SecretKey secretKey, Cipher cipher)
    {
        var result = RabeNative.cp_bsw_decrypt(cipher.Handle,secretKey.Handle);
        return result.ToByteArrayAndFree();
    }
    
    public static SecretKey KeyGen(this MasterKey masterKey,PublicKey publicKey, string[] attributes)
    {
        var secretKey = RabeNative.cp_bsw_generate_secret_key(
            publicKey.Handle, 
            masterKey.Handle,
            attributes, 
            (nuint)attributes.Length
            );
        if (secretKey == IntPtr.Zero)
            throw new Exception("KeyGen failed");
        return new SecretKey(secretKey);
    }
    
}