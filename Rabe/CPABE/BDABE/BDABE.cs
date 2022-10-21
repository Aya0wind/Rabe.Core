using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace Rabe.CPABE.BDABE;

public static class Context
{
    public static ValueTuple<MasterKey,PublicKey> Init()
    {
        var setupResult = RabeNative.cp_bdabe_init();
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
        RabeNative.cp_bdabe_free_public_key(handle);
    }
}

[JsonConverter(typeof(PublicKeyJsonConverter))]
public class PublicAttributeKey : NativeObject
{
    public PublicAttributeKey(IntPtr handle):base(handle)
    {
    }
    
    protected override void FreeHandle(IntPtr handle)
    {
        RabeNative.cp_bdabe_free_public_attribute_key(handle);
    }
}

[JsonConverter(typeof(PublicKeyJsonConverter))]
public class PublicUserKey : NativeObject
{
    public PublicUserKey(IntPtr handle):base(handle)
    {
    }
    
    protected override void FreeHandle(IntPtr handle)
    {
        RabeNative.cp_bdabe_free_public_user_key(handle);
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
        RabeNative.cp_bdabe_free_master_key(handle);
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
        RabeNative.cp_bdabe_free_ciphertext(handle);
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
        RabeNative.cp_bdabe_free_secret_user_key(handle);
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
        RabeNative.cp_bdabe_free_secret_authority_key(handle);
    }
}

[JsonConverter(typeof(SecretAttributeKeyJsonConverter))]
public class SecretAttributeKey : NativeObject
{
    public SecretAttributeKey(IntPtr handle):base(handle)
    {
    }
    
    protected override void FreeHandle(IntPtr handle)
    {
        RabeNative.cp_bdabe_free_secret_attribute_key(handle);
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
        RabeNative.cp_bdabe_free_user_key(handle);
    }
}


public static class Extension
{
    public static SecretAuthorityKey AuthorityKeyGen(this PublicKey publicKey, MasterKey masterKey, string name)
    {
        var secretAuthorityKey = RabeNative.cp_bdabe_generate_secret_authority_key(publicKey.Handle, masterKey.Handle, name);
        if (secretAuthorityKey == IntPtr.Zero)
            throw Common.GetLastWrappedException();
        return new SecretAuthorityKey(secretAuthorityKey);
    }
    
    public static UserKey UserKeyGen(this PublicKey publicKey,SecretAuthorityKey authorityKey, string name)
    {
        var userKey = RabeNative.cp_bdabe_generate_user_key(
            publicKey.Handle, 
            authorityKey.Handle, 
            name);
        if (userKey == IntPtr.Zero)
            throw Common.GetLastWrappedException();
        return new UserKey(userKey);
    }
    
    public static PublicAttributeKey PublicAttributeKeyGen(this PublicKey publicKey,SecretAuthorityKey authorityKey, string name)
    {
        var userKey = RabeNative.cp_bdabe_generate_public_attribute_key(
            publicKey.Handle, 
            authorityKey.Handle, 
            name
        );
        if (userKey == IntPtr.Zero)
            throw Common.GetLastWrappedException();
        return new PublicAttributeKey(userKey);
    }

    public static void AddAttributeToUserKey(this UserKey userKey,SecretAuthorityKey authorityKey, string attr)
    {
        var result = RabeNative.cp_bdabe_add_attribute_to_user_key(
            authorityKey.Handle, 
            userKey.Handle,
            attr
        );
        if (result != 0)
            throw Common.GetLastWrappedException();
    }
    
    public static Cipher Encrypt(
        this PublicKey publicKey, 
        IEnumerable<PublicAttributeKey> publicAttributeKeys,
        string policy,
        byte[] text)
    {
        var publicAttributeKeysArray = publicAttributeKeys.Select(k => k.Handle).ToArray();
        var cipher = RabeNative.cp_bdabe_encrypt(
            publicKey.Handle,
            publicAttributeKeysArray, 
            (nuint)publicAttributeKeysArray.Length, 
            policy,
            text,
            (UIntPtr)text.Length);
        if (cipher == IntPtr.Zero)
            throw Common.GetLastWrappedException();
        return new Cipher(cipher);
    }
    
    public static byte[] Decrypt(this UserKey userKey,PublicKey publicKey, Cipher cipher)
    {
        var result = RabeNative.cp_bdabe_decrypt(publicKey.Handle, userKey.Handle, cipher.Handle);
        return result.ToByteArrayAndFree();
    }
    
}