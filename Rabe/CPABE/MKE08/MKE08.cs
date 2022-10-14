using System.Text.Json.Serialization;

namespace Rabe.CPABE.MKE08;

public static class Context
{
    public static ValueTuple<MasterKey,PublicKey> Init()
    {
        var setupResult = RabeNative.cp_mke08_init();
        return (new MasterKey(setupResult.master_key), new PublicKey(setupResult.public_key));
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
        RabeNative.cp_mke08_free_public_key(handle);
    }
}

[JsonConverter(typeof(PublicAttributeKeyJsonConverter))]
public class PublicAttributeKey : NativeObject
{
    public PublicAttributeKey(IntPtr handle):base(handle)
    {
    }
    
    protected override void FreeHandle(IntPtr handle)
    {
        RabeNative.cp_mke08_free_public_key(handle);
    }
}

[JsonConverter(typeof(PublicUserKeyJsonConverter))]
public class PublicUserKey : NativeObject
{
    public PublicUserKey(IntPtr handle):base(handle)
    {
    }
    
    protected override void FreeHandle(IntPtr handle)
    {
        RabeNative.cp_mke08_free_public_key(handle);
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
        RabeNative.cp_mke08_free_master_key(handle);
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
        RabeNative.cp_mke08_free_ciphertext(handle);
    }
}

[JsonConverter(typeof(SecretAttributeKeyJsonConverter))]
public class SecretAttributeKey : NativeObject
{
    public SecretAttributeKey(IntPtr handle):base(handle)
    {
    }
    
    public byte[] Decrypt(Cipher cipher)
    {
        var result = RabeNative.cp_ac17_decrypt(cipher.Handle, Handle);
        if (result.buffer == IntPtr.Zero)
            throw new Exception("Decryption failed");
        return result.ToByteArrayAndFree();
    }

    protected override void FreeHandle(IntPtr handle)
    {
        RabeNative.cp_mke08_free_secret_attribute_key(handle);
    }
}

[JsonConverter(typeof(SecretUserKeyJsonConverter))]
public class SecretUserKey : NativeObject
{
    public SecretUserKey(IntPtr handle):base(handle)
    {
    }
    protected override void FreeHandle(IntPtr handle)
    {
        RabeNative.cp_mke08_free_secret_user_key(handle);
    }
}

[JsonConverter(typeof(SecretUserKeyJsonConverter))]
public class SecretAuthorityKey : NativeObject
{
    public SecretAuthorityKey(IntPtr handle):base(handle)
    {
    }
    
    protected override void FreeHandle(IntPtr handle)
    {
        RabeNative.cp_mke08_free_secret_authority_key(handle);
    }
}

[JsonConverter(typeof(UserKeyJsonConverter))]
public class UserKey : NativeObject
{
    public UserKey(IntPtr handle):base(handle)
    {
    }

    protected override void FreeHandle(IntPtr handle)
    {
        RabeNative.cp_mke08_free_user_key(handle);
    }
}


public static class Extension
{
    public static Cipher Encrypt(this PublicKey publicKey,IEnumerable<PublicAttributeKey> publicAttributeKeys, string policy, byte[] text)
    {
        var publicAttributeKeysArray = publicAttributeKeys.Select(k => k.Handle).ToArray();
        var cipher = RabeNative.cp_mke08_encrypt(
            publicKey.Handle,
            publicAttributeKeysArray,
            (nuint)publicAttributeKeysArray.Length,
            policy, 
            text, 
            (nuint)text.Length
            );
        if (cipher == IntPtr.Zero)
            throw new Exception("Encryption failed");
        return new Cipher(cipher);
    }
    
    public static byte[] Decrypt(this UserKey userKey,PublicKey publicKey, Cipher cipher)
    {
        var result = RabeNative.cp_bdabe_decrypt(publicKey.Handle, userKey.Handle, cipher.Handle);
        return result.ToByteArrayAndFree();
    }
    
    // public static SecretKey KeyGen(this MasterKey masterKey, string[] attributes)
    // {
    //     var secretKey = RabeNative.cp_ac17_generate_sec_key(
    //         masterKey.Handle, 
    //         attributes, 
    //         (UIntPtr)attributes.Length
    //         );
    //     if (secretKey == IntPtr.Zero)
    //         throw new Exception("KeyGen failed");
    //     return new SecretKey(secretKey);
    // }
}