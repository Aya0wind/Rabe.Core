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

[JsonConverter(typeof(SecretAuthorityKeyJsonConverter))]
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
        var result = RabeNative.cp_mke08_decrypt(publicKey.Handle, userKey.Handle, cipher.Handle);
        return result.ToByteArrayAndFree();
    }
    
    public static UserKey UserKeyGen(this PublicKey publicKey,MasterKey masterKey, string name)
    {
        var userKey = RabeNative.cp_mke08_generate_user_key(
            publicKey.Handle, 
            masterKey.Handle, 
            name
            );
        if (userKey == IntPtr.Zero)
            throw new Exception("UserKeyGen failed");
        return new UserKey(userKey);
    }
    
    public static SecretAuthorityKey SecretAuthorityKeyGen(string name)
    {
        var secretAuthorityKey = RabeNative.cp_mke08_generate_secret_authority_key(name);
        if (secretAuthorityKey == IntPtr.Zero)
            throw new Exception("SecretAuthorityKeyGen failed");
        return new SecretAuthorityKey(secretAuthorityKey);
    }
    
    public static PublicAttributeKey PublicAttributeKeyGen(this PublicKey publicKey,SecretAuthorityKey secretAuthorityKey,string attribute)
    {
        var publicAttributeKey = RabeNative.cp_mke08_generate_public_attribute_key(
            publicKey.Handle, 
            attribute, 
            secretAuthorityKey.Handle
            );
        if (publicAttributeKey == IntPtr.Zero)
            throw new Exception("PublicAttributeKeyGen failed");
        return new PublicAttributeKey(publicAttributeKey);
    }
    
    public static void AddAttributeToUserKey(this UserKey userKey,SecretAuthorityKey secretAuthorityKey, string attribute)
    {
        var result = RabeNative.cp_mke08_add_attribute_to_user_key(
            secretAuthorityKey.Handle, 
            userKey.Handle, 
            attribute
            );
        if (result != 0)
            throw new Exception("AddAttribute failed");
    }
}