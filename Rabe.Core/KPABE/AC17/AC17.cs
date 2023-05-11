using System.Text;
using System.Text.Json.Serialization;
using Rabe.Core.CPABE.AC17;

namespace Rabe.Core.KPABE.AC17;

public static class Context
{
    public static ValueTuple<MasterKey, PublicKey> Init()
    {
        var ac17SetupResult = RabeNative.ac17_init();
        return ValueTuple.Create(new MasterKey(ac17SetupResult.master_key), new PublicKey(ac17SetupResult.public_key));
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
        RabeNative.kp_ac17_free_secret_key(handle);
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
        RabeNative.kp_ac17_free_ciphertext(handle);
    }
}

public static class Extension
{
    public static Cipher Encrypt(this PublicKey publicKey, string[] attrs, byte[] text)
    {
        var cipher = RabeNative.kp_ac17_encrypt(publicKey.Handle, attrs, (nuint)attrs.Length, text, (nuint)text.Length);
        if (cipher == IntPtr.Zero)
            throw Common.GetLastWrappedException();
        return new Cipher(cipher);
    }

    public static SecretKey KeyGen(this MasterKey masterKey, string policy)
    {
        var secretKey = RabeNative.kp_ac17_generate_secret_key(masterKey.Handle, policy);
        if (secretKey == IntPtr.Zero)
            throw Common.GetLastWrappedException();
        return new SecretKey(secretKey);
    }

    public static byte[] Decrypt(this SecretKey secretKey, Cipher cipher)
    {
        var result = RabeNative.kp_ac17_decrypt(cipher.Handle, secretKey.Handle);
        return result.ToByteArrayAndFree();
    }
}
