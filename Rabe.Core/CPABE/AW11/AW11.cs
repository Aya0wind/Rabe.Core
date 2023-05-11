using System.Text.Json.Serialization;

namespace Rabe.Core.CPABE.AW11;


public static class Context
{
    public static GlobalKey Init()
    {
        var globalKey = RabeNative.aw11_init();
        if (globalKey == IntPtr.Zero)
            throw Common.GetLastWrappedException();
        return new GlobalKey(globalKey);
    }
}


[JsonConverter(typeof(PublicKeyJsonConverter))]
public class PublicKey : NativeObject
{
    public PublicKey(IntPtr handle) : base(handle)
    {
    }

    protected override void FreeHandle(IntPtr handle)
    {
        RabeNative.cp_aw11_free_public_key(handle);
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
        RabeNative.cp_aw11_free_master_key(handle);
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
        RabeNative.cp_aw11_free_ciphertext(handle);
    }
}

[JsonConverter(typeof(SecretKeyJsonConverter))]
public class SecretKey : NativeObject
{
    public SecretKey(IntPtr handle) : base(handle)
    {
    }

    public byte[] Decrypt(Cipher cipher)
    {
        var result = RabeNative.cp_ac17_decrypt(cipher.Handle, Handle);
        if (result.buffer == IntPtr.Zero)
            throw Common.GetLastWrappedException();
        return result.ToByteArrayAndFree();
    }

    protected override void FreeHandle(IntPtr handle)
    {
        RabeNative.cp_aw11_free_secret_key(handle);
    }
}

[JsonConverter(typeof(GlobalKeyJsonConverter))]
public class GlobalKey : NativeObject
{
    public GlobalKey(IntPtr handle) : base(handle)
    {
    }

    public byte[] Decrypt(Cipher cipher)
    {
        var result = RabeNative.cp_ac17_decrypt(cipher.Handle, Handle);
        if (result.buffer == IntPtr.Zero)
            throw Common.GetLastWrappedException();
        return result.ToByteArrayAndFree();
    }

    protected override void FreeHandle(IntPtr handle)
    {
        RabeNative.cp_aw11_free_global_key(handle);
    }
}


public static class Extension
{
    public static Cipher Encrypt(this PublicKey publicKey, string policy, byte[] text)
    {
        var cipher = RabeNative.cp_ac17_encrypt(publicKey.Handle, policy, text, (UIntPtr)text.Length);
        if (cipher == IntPtr.Zero)
            throw Common.GetLastWrappedException();
        return new Cipher(cipher);
    }


    public static ValueTuple<MasterKey, PublicKey> AuthorityGen(this GlobalKey globalKey, string[] attributes)
    {
        var authGenResult = RabeNative.cp_aw11_generate_auth(
            globalKey.Handle,
            attributes,
            (UIntPtr)attributes.Length
        );
        if (authGenResult.master_key == IntPtr.Zero || authGenResult.public_key == IntPtr.Zero)
            throw Common.GetLastWrappedException();
        return ValueTuple.Create(new MasterKey(authGenResult.master_key), new PublicKey(authGenResult.public_key));
    }

    public static Cipher Encrypt(this GlobalKey globalKey, IEnumerable<PublicKey> publicKeys, string policy, byte[] text)
    {
        var publicKeysArray = publicKeys.Select(x => x.Handle).ToArray();
        var cipher = RabeNative.cp_aw11_encrypt(
            globalKey.Handle,
            publicKeysArray,
            (nuint)publicKeysArray.Length,
            policy,
            text,
            (nuint)text.Length
            );
        if (cipher == IntPtr.Zero)
            throw Common.GetLastWrappedException();
        return new Cipher(cipher);
    }

    public static byte[] Decrypt(this GlobalKey globalKey, SecretKey secretKey, Cipher cipher)
    {
        var result = RabeNative.cp_aw11_decrypt(globalKey.Handle, secretKey.Handle, cipher.Handle);
        if (result.buffer == IntPtr.Zero)
            throw Common.GetLastWrappedException();
        return result.ToByteArrayAndFree();
    }

    public static SecretKey KeyGen(this GlobalKey globalKey, MasterKey masterKey, string name, IEnumerable<string> attributes)
    {
        var attributeArray = attributes as string[] ?? attributes.ToArray();
        var secretKey = RabeNative.cp_aw11_generate_secret_key(
            globalKey.Handle,
            masterKey.Handle,
            name,
            attributeArray,
            (nuint)attributeArray.Length
            );
        if (secretKey == IntPtr.Zero)
            throw Common.GetLastWrappedException();
        return new SecretKey(secretKey);
    }
}
