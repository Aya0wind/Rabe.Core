using System.Text;
using System.Text.Json.Serialization;
using Rabe.Core.CPABE.AC17;

namespace Rabe.Core.KPABE.LSW;

public static class Context
{
    public static ValueTuple<MasterKey, PublicKey> Init()
    {
        var setupResult = RabeNative.kp_lsw_init();
        return ValueTuple.Create(new MasterKey(setupResult.master_key), new PublicKey(setupResult.public_key));
    }
}

[JsonConverter(typeof(PublicKeyJsonConverter))]
public class PublicKey : NativeObject
{
    public PublicKey(IntPtr handle) : base(handle)
    {
    }


    public byte[] Decrypt(Cipher cipher)
    {
        var result = RabeNative.kp_lsw_decrypt(cipher.Handle, Handle);
        return result.ToByteArrayAndFree();
    }

    protected override void FreeHandle(IntPtr handle)
    {
        RabeNative.kp_lsw_free_public_key(handle);
    }
}

[JsonConverter(typeof(MasterKeyJsonConverter))]
public class MasterKey : NativeObject
{
    public MasterKey(IntPtr handle) : base(handle)
    {
    }

    protected override void FreeHandle(IntPtr handle)
    {
        RabeNative.kp_lsw_free_master_key(handle);
    }
}


[JsonConverter(typeof(SecretKeyJsonConverter))]
public class SecretKey : NativeObject
{
    public SecretKey(IntPtr handle) : base(handle)
    {
    }

    protected override void FreeHandle(IntPtr handle)
    {
        RabeNative.kp_lsw_free_secret_key(handle);
    }
}

[JsonConverter(typeof(CipherJsonConverter))]
public class Cipher : NativeObject
{
    public Cipher(IntPtr handle) : base(handle)
    {

    }


    protected override void FreeHandle(IntPtr handle)
    {
        RabeNative.kp_lsw_free_ciphertext(handle);
    }
}

public static class Extension
{
    public static Cipher Encrypt(this PublicKey publicKey, string[] attrs, byte[] text)
    {
        var cipher = RabeNative.kp_lsw_encrypt(publicKey.Handle, attrs, (nuint)attrs.Length, text, (nuint)text.Length);
        if (cipher == IntPtr.Zero)
            throw Common.GetLastWrappedException();
        return new Cipher(cipher);
    }

    public static SecretKey KeyGen(this PublicKey publicKey, MasterKey masterKey, string policy)
    {
        var secretKey = RabeNative.kp_lsw_generate_secret_key(publicKey.Handle, masterKey.Handle, policy);
        if (secretKey == IntPtr.Zero)
            throw Common.GetLastWrappedException();
        return new SecretKey(secretKey);
    }
    //decrypt
    public static byte[] Decrypt(this SecretKey secretKey, Cipher cipher)
    {
        var result = RabeNative.kp_lsw_decrypt(cipher.Handle, secretKey.Handle);
        return result.ToByteArrayAndFree();
    }
}
