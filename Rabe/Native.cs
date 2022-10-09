using System.Runtime.InteropServices;

namespace Rabe;

[StructLayout(LayoutKind.Sequential)]
public struct InitKeyResult
{
    public IntPtr pubKey;
    public IntPtr masterKey;
}

[StructLayout(LayoutKind.Sequential)]
public struct DecryptResult
{
    public IntPtr buffer;
    public UIntPtr len;
}

internal static class NativeLibCommon
{
    public const string LibraryName = "rabe_ffi";

    // Common functions for cp-abe and kp-abe
    [DllImport(LibraryName, EntryPoint = "rabe_init")]
    public static extern InitKeyResult Init();

    [DllImport(LibraryName, EntryPoint = "rabe_master_key_to_json")]
    public static extern IntPtr MasterKeyToJson(
        IntPtr masterKey
    );

    [DllImport(LibraryName, EntryPoint = "rabe_pub_key_to_json")]
    public static extern IntPtr PubKeyToJson(
        IntPtr pubKey
    );

    [DllImport(LibraryName, EntryPoint = "rabe_free_json")]
    public static extern void FreeJson(
        IntPtr json
    );

    [DllImport(LibraryName, EntryPoint = "rabe_free_decrypt_result")]
    public static extern void FreeDecryptResult(
        DecryptResult result
    );

    [DllImport(LibraryName, EntryPoint = "rabe_free_init_result")]
    public static extern void FreeInitResult(
        InitKeyResult result
    );

    [DllImport(LibraryName, EntryPoint = "rabe_free_pub_key")]
    public static extern void FreePubKey(
        IntPtr pubKey
    );

    [DllImport(LibraryName, EntryPoint = "rabe_free_master_key")]
    public static extern void FreeMasterKey(
        IntPtr masterKey
    );

    [DllImport(LibraryName, EntryPoint = "rabe_deserialize_pub_key")]
    public static extern IntPtr DeserializePubKey(
        string json
    );

    [DllImport(LibraryName, EntryPoint = "rabe_deserialize_master_key")]
    public static extern IntPtr DeserializeMasterKey(
        string json
    );
}

internal static class NativeLibCp
{
    private const string LibraryName = NativeLibCommon.LibraryName;

    [DllImport(LibraryName, EntryPoint = "rabe_generate_cp_sec_key")]
    public static extern IntPtr GenerateSecKey(
        IntPtr masterKey,
        string[] attrs,
        UIntPtr attrLen
    );

    [DllImport(LibraryName, EntryPoint = "rabe_cp_encrypt")]
    public static extern IntPtr Encrypt(
        IntPtr pubKey,
        string policy,
        byte[] text,
        UIntPtr textLength
    );

    [DllImport(LibraryName, EntryPoint = "rabe_cp_decrypt")]
    public static extern DecryptResult Decrypt(
        IntPtr cipher,
        IntPtr secKey
    );


    [DllImport(LibraryName, EntryPoint = "rabe_cp_sec_key_to_json")]
    public static extern IntPtr SecKeyToJson(
        IntPtr secKey
    );


    [DllImport(LibraryName, EntryPoint = "rabe_cp_cipher_to_json")]
    public static extern IntPtr CipherToJson(
        IntPtr cipher
    );


    [DllImport(LibraryName, EntryPoint = "rabe_free_cp_cipher")]
    public static extern void FreeCipher(
        IntPtr cipher
    );

    [DllImport(LibraryName, EntryPoint = "rabe_free_cp_sec_key")]
    public static extern void FreeSecKey(
        IntPtr secKey
    );


    [DllImport(LibraryName, EntryPoint = "rabe_deserialize_cp_sec_key")]
    public static extern IntPtr DeserializeSecretKey(
        string json
    );

    [DllImport(LibraryName, EntryPoint = "rabe_deserialize_cp_cipher")]
    public static extern IntPtr DeserializeCipher(
        string json
    );
}

internal static class NativeLibKp
{
    private const string LibraryName = NativeLibCommon.LibraryName;

    [DllImport(LibraryName, EntryPoint = "rabe_generate_kp_sec_key")]
    public static extern IntPtr GenerateSecKey(
        IntPtr masterKey,
        string policy
    );

    [DllImport(LibraryName, EntryPoint = "rabe_kp_encrypt")]
    public static extern IntPtr Encrypt(
        IntPtr pubKey,
        string[] attrs,
        UIntPtr attrLen,
        byte[] text,
        UIntPtr textLength
    );

    [DllImport(LibraryName, EntryPoint = "rabe_kp_decrypt")]
    public static extern DecryptResult Decrypt(
        IntPtr cipher,
        IntPtr secKey
    );


    [DllImport(LibraryName, EntryPoint = "rabe_kp_sec_key_to_json")]
    public static extern IntPtr SecKeyToJson(
        IntPtr secKey
    );


    [DllImport(LibraryName, EntryPoint = "rabe_kp_cipher_to_json")]
    public static extern IntPtr CipherToJson(
        IntPtr cipher
    );


    [DllImport(LibraryName, EntryPoint = "rabe_free_kp_cipher")]
    public static extern void FreeCipher(
        IntPtr cipher
    );

    [DllImport(LibraryName, EntryPoint = "rabe_free_kp_sec_key")]
    public static extern void FreeSecKey(
        IntPtr secKey
    );


    [DllImport(LibraryName, EntryPoint = "rabe_deserialize_kp_sec_key")]
    public static extern IntPtr DeserializeSecretKey(
        string json
    );

    [DllImport(LibraryName, EntryPoint = "rabe_deserialize_kp_cipher")]
    public static extern IntPtr DeserializeCipher(
        string json
    );
}