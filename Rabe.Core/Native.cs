using System.Diagnostics;
using System.Runtime.InteropServices;
#pragma warning disable 0649
namespace Rabe.Core;

internal struct CBoxedBuffer
{
    [NativeTypeName("const unsigned char *")]
    public IntPtr buffer;

    [NativeTypeName("uintptr_t")] public nuint len;
}

internal struct Ac17SetupResult
{
    [NativeTypeName("const void *")] public IntPtr master_key;

    [NativeTypeName("const void *")] public IntPtr public_key;
}

internal struct Aw11AuthGenResult
{
    [NativeTypeName("const void *")] public IntPtr master_key;

    [NativeTypeName("const void *")] public IntPtr public_key;
}

internal struct BdabeSetupResult
{
    [NativeTypeName("const void *")] public IntPtr master_key;

    [NativeTypeName("const void *")] public IntPtr public_key;
}

internal struct BswSetupResult
{
    [NativeTypeName("const void *")] public IntPtr master_key;

    [NativeTypeName("const void *")] public IntPtr public_key;
}

internal struct Mke08SetupResult
{
    [NativeTypeName("const void *")] public IntPtr master_key;

    [NativeTypeName("const void *")] public IntPtr public_key;
}

internal struct Yct14AbeSetupResult
{
    [NativeTypeName("const void *")] public IntPtr master_key;

    [NativeTypeName("const void *")] public IntPtr public_key;
}

internal struct LswSetupResult
{
    [NativeTypeName("const void *")] public IntPtr master_key;

    [NativeTypeName("const void *")] public IntPtr public_key;
}

/// <summary>Defines the type of a member as it was used in the native signature.</summary>
[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Property | AttributeTargets.Field |
                AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
[Conditional("DEBUG")]
internal sealed class NativeTypeNameAttribute : Attribute
{
    /// <summary>Initializes a new instance of the <see cref="NativeTypeNameAttribute" /> class.</summary>
    /// <param name="name">The name of the type that was used in the native signature.</param>
    public NativeTypeNameAttribute(string name)
    {
        Name = name;
    }

    /// <summary>Gets the name of the type that was used in the native signature.</summary>
    public string Name { get; }
}

internal static class RabeNative
{
#if OS_WINDOWS
    const string DllName = "rabe_ffi";
#elif OS_LINUX
    const string DllName = "librabe_ffi";
#elif OS_MACOS
    const string DllName = "librabe_ffi";
#endif
    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_free_json",
        ExactSpelling = true)]
    public static extern void free_json([NativeTypeName("char *")] IntPtr json);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_get_thread_last_error",
        ExactSpelling = true)]
    public static extern string get_thread_last_error();


    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_free_boxed_buffer",
        ExactSpelling = true)]
    public static extern void free_boxed_buffer([NativeTypeName("struct CBoxedBuffer")] CBoxedBuffer result);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_ac17_init",
        ExactSpelling = true)]
    [return: NativeTypeName("struct Ac17SetupResult")]
    public static extern Ac17SetupResult ac17_init();

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_ac17_generate_secret_key",
        ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr cp_ac17_generate_secret_key([NativeTypeName("const void *")] IntPtr master_key,
        [NativeTypeName("const char *const *")] string[] attr, [NativeTypeName("uintptr_t")] nuint attr_len);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_ac17_encrypt",
        ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr cp_ac17_encrypt([NativeTypeName("const void *")] IntPtr public_key,
        [NativeTypeName("const char *")] string policy, [NativeTypeName("const char *")] byte[] text,
        [NativeTypeName("uintptr_t")] nuint text_length);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_ac17_decrypt",
        ExactSpelling = true)]
    [return: NativeTypeName("struct CBoxedBuffer")]
    public static extern CBoxedBuffer cp_ac17_decrypt([NativeTypeName("const void *")] IntPtr cipher,
        [NativeTypeName("const void *")] IntPtr secret_key);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_ac17_master_key_to_json",
        ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern IntPtr ac17_master_key_to_json([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_ac17_public_key_to_json",
        ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern IntPtr ac17_public_key_to_json([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_ac17_secret_key_to_json",
        ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern IntPtr cp_ac17_secret_key_to_json([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_ac17_cipher_to_json",
        ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern IntPtr cp_ac17_cipher_to_json([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_ac17_master_key_from_json",
        ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr ac17_master_key_from_json([NativeTypeName("const char *")] string json);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_ac17_public_key_from_json",
        ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr ac17_public_key_from_json([NativeTypeName("const char *")] string json);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_cp_ac17_secret_key_from_json", ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr cp_ac17_secret_key_from_json([NativeTypeName("const char *")] string json);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_ac17_cipher_from_json",
        ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr cp_ac17_cipher_from_json([NativeTypeName("const char *")] string json);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_ac17_free_master_key",
        ExactSpelling = true)]
    public static extern void ac17_free_master_key([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_ac17_free_public_key",
        ExactSpelling = true)]
    public static extern void ac17_free_public_key([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_ac17_free_secret_key",
        ExactSpelling = true)]
    public static extern void cp_ac17_free_secret_key([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_ac17_free_cipher",
        ExactSpelling = true)]
    public static extern void cp_ac17_free_cipher([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_aw11_init",
        ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr aw11_init();

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_aw11_generate_auth",
        ExactSpelling = true)]
    [return: NativeTypeName("struct Aw11AuthGenResult")]
    public static extern Aw11AuthGenResult cp_aw11_generate_auth([NativeTypeName("const void *")] IntPtr global_key,
        [NativeTypeName("const char *const *")] string[] attrs, [NativeTypeName("uintptr_t")] nuint attr_len);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_aw11_generate_secret_key",
        ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr cp_aw11_generate_secret_key([NativeTypeName("const void *")] IntPtr global_key,
        [NativeTypeName("const void *")] IntPtr master_key, [NativeTypeName("const char *")] string name,
        [NativeTypeName("const char *const *")] string[] attrs, [NativeTypeName("uintptr_t")] nuint attr_len);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_aw11_encrypt",
        ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr cp_aw11_encrypt([NativeTypeName("const void *")] IntPtr global_key,
        [NativeTypeName("const void *const *")] IntPtr[] public_keys, [NativeTypeName("uintptr_t")] nuint public_keys_len,
        [NativeTypeName("const char *")] string policy, [NativeTypeName("const char *")] byte[] text,
        [NativeTypeName("uintptr_t")] nuint text_length);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_aw11_decrypt",
        ExactSpelling = true)]
    [return: NativeTypeName("struct CBoxedBuffer")]
    public static extern CBoxedBuffer cp_aw11_decrypt([NativeTypeName("const void *")] IntPtr global_key,
        [NativeTypeName("const void *")] IntPtr secret_key, [NativeTypeName("const void *")] IntPtr cipher);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_cp_aw11_master_key_from_json", ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr cp_aw11_master_key_from_json([NativeTypeName("const char *")] string json);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_cp_aw11_public_key_from_json", ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr cp_aw11_public_key_from_json([NativeTypeName("const char *")] string json);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_cp_aw11_secret_key_from_json", ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr cp_aw11_secret_key_from_json([NativeTypeName("const char *")] string json);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_cp_aw11_ciphertext_from_json", ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr cp_aw11_ciphertext_from_json([NativeTypeName("const char *")] string json);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_cp_aw11_global_key_from_json", ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr cp_aw11_global_key_from_json([NativeTypeName("const char *")] string json);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_aw11_master_key_to_json",
        ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern IntPtr cp_aw11_master_key_to_json([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_aw11_public_key_to_json",
        ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern IntPtr cp_aw11_public_key_to_json([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_aw11_secret_key_to_json",
        ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern IntPtr cp_aw11_secret_key_to_json([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_aw11_ciphertext_to_json",
        ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern IntPtr cp_aw11_ciphertext_to_json([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_aw11_global_key_to_json",
        ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern IntPtr cp_aw11_global_key_to_json([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_aw11_free_master_key",
        ExactSpelling = true)]
    public static extern void cp_aw11_free_master_key([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_aw11_free_public_key",
        ExactSpelling = true)]
    public static extern void cp_aw11_free_public_key([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_aw11_free_secret_key",
        ExactSpelling = true)]
    public static extern void cp_aw11_free_secret_key([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_aw11_free_ciphertext",
        ExactSpelling = true)]
    public static extern void cp_aw11_free_ciphertext([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_aw11_free_global_key",
        ExactSpelling = true)]
    public static extern void cp_aw11_free_global_key([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_bdabe_init",
        ExactSpelling = true)]
    [return: NativeTypeName("struct BdabeSetupResult")]
    public static extern BdabeSetupResult cp_bdabe_init();

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_cp_bdabe_generate_secret_authority_key", ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr cp_bdabe_generate_secret_authority_key([NativeTypeName("const void *")] IntPtr public_key,
        [NativeTypeName("const void *")] IntPtr master_key, [NativeTypeName("const char *")] string name);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_cp_bdabe_generate_secret_attribute_key", ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr cp_bdabe_generate_secret_attribute_key(
        [NativeTypeName("const void *")] IntPtr public_user_key,
        [NativeTypeName("const void *")] IntPtr secret_authority_key, [NativeTypeName("const char *")] string attr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_bdabe_generate_user_key",
        ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr cp_bdabe_generate_user_key([NativeTypeName("const void *")] IntPtr public_key,
        [NativeTypeName("const void *")] IntPtr secret_authority_key, [NativeTypeName("const char *")] string name);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_cp_bdabe_generate_public_attribute_key", ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr cp_bdabe_generate_public_attribute_key([NativeTypeName("const void *")] IntPtr public_key,
        [NativeTypeName("const void *")] IntPtr secret_authority_key, [NativeTypeName("const char *")] string name);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_cp_bdabe_add_attribute_to_user_key", ExactSpelling = true)]
    public static extern int cp_bdabe_add_attribute_to_user_key(
        [NativeTypeName("const void *")] IntPtr secret_authority_key, [NativeTypeName("const void *")] IntPtr user_key,
        [NativeTypeName("const char *")] string attr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_bdabe_encrypt",
        ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr cp_bdabe_encrypt([NativeTypeName("const void *")] IntPtr public_key,
        [NativeTypeName("const void *const *")] IntPtr[] public_attribute_keys,
        [NativeTypeName("uintptr_t")] nuint public_attribute_keys_len, [NativeTypeName("const char *")] string policy,
        [NativeTypeName("const char *")] byte[] text, [NativeTypeName("uintptr_t")] nuint text_length);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_bdabe_decrypt",
        ExactSpelling = true)]
    [return: NativeTypeName("struct CBoxedBuffer")]
    public static extern CBoxedBuffer cp_bdabe_decrypt([NativeTypeName("const void *")] IntPtr public_key,
        [NativeTypeName("const void *")] IntPtr user_key, [NativeTypeName("const void *")] IntPtr cipher);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_cp_bdabe_public_user_key_to_json", ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern IntPtr cp_bdabe_public_user_key_to_json([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_cp_bdabe_secret_user_key_to_json", ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern IntPtr cp_bdabe_secret_user_key_to_json([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_bdabe_master_key_to_json",
        ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern IntPtr cp_bdabe_master_key_to_json([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_bdabe_public_key_to_json",
        ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern IntPtr cp_bdabe_public_key_to_json([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_cp_bdabe_secret_authority_key_to_json", ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern IntPtr cp_bdabe_secret_authority_key_to_json([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_cp_bdabe_secret_attribute_key_to_json", ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern IntPtr cp_bdabe_secret_attribute_key_to_json([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_cp_bdabe_public_attribute_key_to_json", ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern IntPtr cp_bdabe_public_attribute_key_to_json([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_bdabe_user_key_to_json",
        ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern IntPtr cp_bdabe_user_key_to_json([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_bdabe_ciphertext_to_json",
        ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern IntPtr cp_bdabe_ciphertext_to_json([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_cp_bdabe_public_user_key_from_json", ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr cp_bdabe_public_user_key_from_json([NativeTypeName("const char *")] string json);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_cp_bdabe_secret_user_key_from_json", ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr cp_bdabe_secret_user_key_from_json([NativeTypeName("const char *")] string json);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_cp_bdabe_master_key_from_json", ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr cp_bdabe_master_key_from_json([NativeTypeName("const char *")] string json);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_cp_bdabe_public_key_from_json", ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr cp_bdabe_public_key_from_json([NativeTypeName("const char *")] string json);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_cp_bdabe_secret_authority_key_from_json", ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr cp_bdabe_secret_authority_key_from_json([NativeTypeName("const char *")] string json);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_cp_bdabe_secret_attribute_key_from_json", ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr cp_bdabe_secret_attribute_key_from_json([NativeTypeName("const char *")] string json);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_cp_bdabe_public_attribute_key_from_json", ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr cp_bdabe_public_attribute_key_from_json([NativeTypeName("const char *")] string json);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_bdabe_user_key_from_json",
        ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr cp_bdabe_user_key_from_json([NativeTypeName("const char *")] string json);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_cp_bdabe_ciphertext_from_json", ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr cp_bdabe_ciphertext_from_json([NativeTypeName("const char *")] string json);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_cp_bdabe_free_public_user_key", ExactSpelling = true)]
    public static extern void cp_bdabe_free_public_user_key([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_cp_bdabe_free_secret_user_key", ExactSpelling = true)]
    public static extern void cp_bdabe_free_secret_user_key([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_bdabe_free_master_key",
        ExactSpelling = true)]
    public static extern void cp_bdabe_free_master_key([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_bdabe_free_public_key",
        ExactSpelling = true)]
    public static extern void cp_bdabe_free_public_key([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_cp_bdabe_free_secret_authority_key", ExactSpelling = true)]
    public static extern void cp_bdabe_free_secret_authority_key([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_cp_bdabe_free_secret_attribute_key", ExactSpelling = true)]
    public static extern void cp_bdabe_free_secret_attribute_key([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_cp_bdabe_free_public_attribute_key", ExactSpelling = true)]
    public static extern void cp_bdabe_free_public_attribute_key([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_bdabe_free_user_key",
        ExactSpelling = true)]
    public static extern void cp_bdabe_free_user_key([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_bdabe_free_ciphertext",
        ExactSpelling = true)]
    public static extern void cp_bdabe_free_ciphertext([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_bsw_init",
        ExactSpelling = true)]
    [return: NativeTypeName("struct BswSetupResult")]
    public static extern BswSetupResult bsw_init();

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_bsw_generate_secret_key",
        ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr cp_bsw_generate_secret_key([NativeTypeName("const void *")] IntPtr public_key,
        [NativeTypeName("const void *")] IntPtr master_key, [NativeTypeName("const char *const *")] string[] attr,
        [NativeTypeName("uintptr_t")] nuint attr_len);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_bsw_encrypt",
        ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr cp_bsw_encrypt([NativeTypeName("const void *")] IntPtr public_key,
        [NativeTypeName("const char *")] string policy, [NativeTypeName("const char *")] byte[] text,
        [NativeTypeName("uintptr_t")] nuint text_length);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_bsw_decrypt",
        ExactSpelling = true)]
    [return: NativeTypeName("struct CBoxedBuffer")]
    public static extern CBoxedBuffer cp_bsw_decrypt([NativeTypeName("const void *")] IntPtr cipher,
        [NativeTypeName("const void *")] IntPtr secret_key);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_bsw_secret_key_from_json",
        ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr cp_bsw_secret_key_from_json([NativeTypeName("const char *")] string json);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_bsw_public_key_from_json",
        ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr cp_bsw_public_key_from_json([NativeTypeName("const char *")] string json);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_bsw_ciphertext_from_json",
        ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr cp_bsw_ciphertext_from_json([NativeTypeName("const char *")] string json);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_bsw_master_key_from_json",
        ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr cp_bsw_master_key_from_json([NativeTypeName("const char *")] string json);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_bsw_secret_key_to_json",
        ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern IntPtr cp_bsw_secret_key_to_json([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_bsw_public_key_to_json",
        ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern IntPtr cp_bsw_public_key_to_json([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_bsw_ciphertext_to_json",
        ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern IntPtr cp_bsw_ciphertext_to_json([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_bsw_master_key_to_json",
        ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern IntPtr cp_bsw_master_key_to_json([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_bsw_free_secret_key",
        ExactSpelling = true)]
    public static extern void cp_bsw_free_secret_key([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_bsw_free_public_key",
        ExactSpelling = true)]
    public static extern void cp_bsw_free_public_key([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_bsw_free_ciphertext",
        ExactSpelling = true)]
    public static extern void cp_bsw_free_ciphertext([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_bsw_free_master_key",
        ExactSpelling = true)]
    public static extern void cp_bsw_free_master_key([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_mke08_init",
        ExactSpelling = true)]
    [return: NativeTypeName("struct Mke08SetupResult")]
    public static extern Mke08SetupResult cp_mke08_init();

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_cp_mke08_generate_secret_authority_key", ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr cp_mke08_generate_secret_authority_key([NativeTypeName("const char *")] string name);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_mke08_generate_user_key",
        ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr cp_mke08_generate_user_key([NativeTypeName("const void *")] IntPtr public_key,
        [NativeTypeName("const void *")] IntPtr master_key, [NativeTypeName("const char *")] string name);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_cp_mke08_add_attribute_to_user_key", ExactSpelling = true)]
    public static extern int cp_mke08_add_attribute_to_user_key(
        [NativeTypeName("const void *")] IntPtr secret_authority_key, [NativeTypeName("const void *")] IntPtr user_key,
        [NativeTypeName("const char *")] string attr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_cp_mke08_generate_public_attribute_key", ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr cp_mke08_generate_public_attribute_key([NativeTypeName("const void *")] IntPtr public_key,
        [NativeTypeName("const char *")] string attr, [NativeTypeName("const void *")] IntPtr secret_authority_key);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_mke08_encrypt",
        ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr cp_mke08_encrypt([NativeTypeName("const void *")] IntPtr public_key,
        [NativeTypeName("const void *const *")] IntPtr[] public_attribute_keys,
        [NativeTypeName("uintptr_t")] nuint public_attribute_keys_len, [NativeTypeName("const char *")] string policy,
        [NativeTypeName("const char *")] byte[] text, [NativeTypeName("uintptr_t")] nuint text_length);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_mke08_decrypt",
        ExactSpelling = true)]
    [return: NativeTypeName("struct CBoxedBuffer")]
    public static extern CBoxedBuffer cp_mke08_decrypt([NativeTypeName("const void *")] IntPtr public_key,
        [NativeTypeName("const void *")] IntPtr user_key, [NativeTypeName("const void *")] IntPtr cipher);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_mke08_master_key_to_json",
        ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern IntPtr cp_mke08_master_key_to_json([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_mke08_public_key_to_json",
        ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern IntPtr cp_mke08_public_key_to_json([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_cp_mke08_public_attribute_key_to_json", ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern IntPtr cp_mke08_public_attribute_key_to_json([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_cp_mke08_public_user_key_to_json", ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern IntPtr cp_mke08_public_user_key_to_json([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_cp_mke08_secret_attribute_key_to_json", ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern IntPtr cp_mke08_secret_attribute_key_to_json([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_cp_mke08_secret_authority_key_to_json", ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern IntPtr cp_mke08_secret_authority_key_to_json([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_cp_mke08_secret_user_key_to_json", ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern IntPtr cp_mke08_secret_user_key_to_json([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_mke08_user_key_to_json",
        ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern IntPtr cp_mke08_user_key_to_json([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_mke08_ciphertext_to_json",
        ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern IntPtr cp_mke08_ciphertext_to_json([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_cp_mke08_master_key_from_json", ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr cp_mke08_master_key_from_json([NativeTypeName("const char *")] string json);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_cp_mke08_public_key_from_json", ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr cp_mke08_public_key_from_json([NativeTypeName("const char *")] string json);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_cp_mke08_public_attribute_key_from_json", ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr cp_mke08_public_attribute_key_from_json([NativeTypeName("const char *")] string json);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_cp_mke08_public_user_key_from_json", ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr cp_mke08_public_user_key_from_json([NativeTypeName("const char *")] string json);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_cp_mke08_secret_attribute_key_from_json", ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr cp_mke08_secret_attribute_key_from_json([NativeTypeName("const char *")] string json);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_cp_mke08_secret_authority_key_from_json", ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr cp_mke08_secret_authority_key_from_json([NativeTypeName("const char *")] string json);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_cp_mke08_secret_user_key_from_json", ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr cp_mke08_secret_user_key_from_json([NativeTypeName("const char *")] string json);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_mke08_user_key_from_json",
        ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr cp_mke08_user_key_from_json([NativeTypeName("const char *")] string json);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_cp_mke08_ciphertext_from_json", ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr cp_mke08_ciphertext_from_json([NativeTypeName("const char *")] string json);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_mke08_free_master_key",
        ExactSpelling = true)]
    public static extern void cp_mke08_free_master_key([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_mke08_free_public_key",
        ExactSpelling = true)]
    public static extern void cp_mke08_free_public_key([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_cp_mke08_free_public_attribute_key", ExactSpelling = true)]
    public static extern void cp_mke08_free_public_attribute_key([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_cp_mke08_free_public_user_key", ExactSpelling = true)]
    public static extern void cp_mke08_free_public_user_key([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_cp_mke08_free_secret_attribute_key", ExactSpelling = true)]
    public static extern void cp_mke08_free_secret_attribute_key([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_cp_mke08_free_secret_authority_key", ExactSpelling = true)]
    public static extern void cp_mke08_free_secret_authority_key([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_cp_mke08_free_secret_user_key", ExactSpelling = true)]
    public static extern void cp_mke08_free_secret_user_key([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_mke08_free_user_key",
        ExactSpelling = true)]
    public static extern void cp_mke08_free_user_key([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_cp_mke08_free_ciphertext",
        ExactSpelling = true)]
    public static extern void cp_mke08_free_ciphertext([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_kp_ac17_generate_secret_key",
        ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr kp_ac17_generate_secret_key([NativeTypeName("const void *")] IntPtr master_key,
        [NativeTypeName("const char *")] string policy);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_kp_ac17_encrypt",
        ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr kp_ac17_encrypt([NativeTypeName("const void *")] IntPtr public_key,
        [NativeTypeName("const char *const *")] string[] attr, [NativeTypeName("uintptr_t")] nuint attr_len,
        [NativeTypeName("const char *")] byte[] text, [NativeTypeName("uintptr_t")] nuint text_length);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_kp_ac17_decrypt",
        ExactSpelling = true)]
    [return: NativeTypeName("struct CBoxedBuffer")]
    public static extern CBoxedBuffer kp_ac17_decrypt([NativeTypeName("const void *")] IntPtr cipher,
        [NativeTypeName("const void *")] IntPtr secret_key);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_kp_ac17_master_key_to_json",
        ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern IntPtr kp_ac17_master_key_to_json([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_kp_ac17_public_key_to_json",
        ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern IntPtr kp_ac17_public_key_to_json([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_kp_ac17_secret_key_to_json",
        ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern IntPtr kp_ac17_secret_key_to_json([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_kp_ac17_ciphertext_to_json",
        ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern IntPtr kp_ac17_ciphertext_to_json([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_kp_ac17_master_key_from_json", ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr kp_ac17_master_key_from_json([NativeTypeName("const char *")] string json);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_kp_ac17_public_key_from_json", ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr kp_ac17_public_key_from_json([NativeTypeName("const char *")] string json);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_kp_ac17_secret_key_from_json", ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr kp_ac17_secret_key_from_json([NativeTypeName("const char *")] string json);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_kp_ac17_ciphertext_from_json", ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr kp_ac17_ciphertext_from_json([NativeTypeName("const char *")] string json);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_kp_ac17_free_master_key",
        ExactSpelling = true)]
    public static extern void kp_ac17_free_master_key([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_kp_ac17_free_public_key",
        ExactSpelling = true)]
    public static extern void kp_ac17_free_public_key([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_kp_ac17_free_secret_key",
        ExactSpelling = true)]
    public static extern void kp_ac17_free_secret_key([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_kp_ac17_free_ciphertext",
        ExactSpelling = true)]
    public static extern void kp_ac17_free_ciphertext([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_kp_yct14_init",
        ExactSpelling = true)]
    [return: NativeTypeName("struct Yct14AbeSetupResult")]
    public static extern Yct14AbeSetupResult kp_yct14_init([NativeTypeName("const char *const *")] string[] attrs,
        [NativeTypeName("uintptr_t")] nuint attr_len);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_kp_yct14_generate_secret_key", ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr kp_yct14_generate_secret_key([NativeTypeName("const void *")] IntPtr public_key,
        [NativeTypeName("const void *")] IntPtr master_key, [NativeTypeName("const char *")] string policy);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_kp_yct14_encrypt",
        ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr kp_yct14_encrypt([NativeTypeName("const void *")] IntPtr public_key,
        [NativeTypeName("const char *const *")] string[] attrs, [NativeTypeName("uintptr_t")] nuint attr_len,
        [NativeTypeName("const char *")] byte[] text, [NativeTypeName("uintptr_t")] nuint text_length);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_kp_yct14_decrypt",
        ExactSpelling = true)]
    [return: NativeTypeName("struct CBoxedBuffer")]
    public static extern CBoxedBuffer kp_yct14_decrypt([NativeTypeName("const void *")] IntPtr cipher,
        [NativeTypeName("const void *")] IntPtr secret_key);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_kp_yct14_ciphertext_to_json",
        ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern IntPtr kp_yct14_ciphertext_to_json([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_kp_yct14_master_key_to_json",
        ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern IntPtr kp_yct14_master_key_to_json([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_kp_yct14_public_key_to_json",
        ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern IntPtr kp_yct14_public_key_to_json([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_kp_yct14_secret_key_to_json",
        ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern IntPtr kp_yct14_secret_key_to_json([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_kp_yct14_ciphertext_from_json", ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr kp_yct14_ciphertext_from_json([NativeTypeName("const char *")] string json);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_kp_yct14_master_key_from_json", ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr kp_yct14_master_key_from_json([NativeTypeName("const char *")] string json);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_kp_yct14_public_key_from_json", ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr kp_yct14_public_key_from_json([NativeTypeName("const char *")] string json);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "rabe_kp_yct14_secret_key_from_json", ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr kp_yct14_secret_key_from_json([NativeTypeName("const char *")] string json);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_kp_yct14_free_ciphertext",
        ExactSpelling = true)]
    public static extern void kp_yct14_free_ciphertext([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_kp_yct14_free_master_key",
        ExactSpelling = true)]
    public static extern void kp_yct14_free_master_key([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_kp_yct14_free_public_key",
        ExactSpelling = true)]
    public static extern void kp_yct14_free_public_key([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_kp_yct14_free_secret_key",
        ExactSpelling = true)]
    public static extern void kp_yct14_free_secret_key([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_kp_lsw_init",
        ExactSpelling = true)]
    [return: NativeTypeName("struct LswSetupResult")]
    public static extern LswSetupResult kp_lsw_init();

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_kp_lsw_generate_secret_key",
        ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr kp_lsw_generate_secret_key([NativeTypeName("const void *")] IntPtr public_key,
        [NativeTypeName("const void *")] IntPtr master_key, [NativeTypeName("const char *")] string policy);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_kp_lsw_encrypt",
        ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr kp_lsw_encrypt([NativeTypeName("const void *")] IntPtr public_key,
        [NativeTypeName("const char *const *")] string[] attrs, [NativeTypeName("uintptr_t")] nuint attr_len,
        [NativeTypeName("const char *")] byte[] text, [NativeTypeName("uintptr_t")] nuint text_length);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_kp_lsw_decrypt",
        ExactSpelling = true)]
    [return: NativeTypeName("struct CBoxedBuffer")]
    public static extern CBoxedBuffer kp_lsw_decrypt([NativeTypeName("const void *")] IntPtr cipher,
        [NativeTypeName("const void *")] IntPtr secret_key);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_kp_lsw_master_key_to_json",
        ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern IntPtr kp_lsw_master_key_to_json([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_kp_lsw_public_key_to_json",
        ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern IntPtr kp_lsw_public_key_to_json([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_kp_lsw_secret_key_to_json",
        ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern IntPtr kp_lsw_secret_key_to_json([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_kp_lsw_ciphertext_to_json",
        ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern IntPtr kp_lsw_ciphertext_to_json([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_kp_lsw_master_key_from_json",
        ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr kp_lsw_master_key_from_json([NativeTypeName("const char *")] string json);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_kp_lsw_public_key_from_json",
        ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr kp_lsw_public_key_from_json([NativeTypeName("const char *")] string json);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_kp_lsw_secret_key_from_json",
        ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr kp_lsw_secret_key_from_json([NativeTypeName("const char *")] string json);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_kp_lsw_ciphertext_from_json",
        ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern IntPtr kp_lsw_ciphertext_from_json([NativeTypeName("const char *")] string json);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_kp_lsw_free_master_key",
        ExactSpelling = true)]
    public static extern void kp_lsw_free_master_key([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_kp_lsw_free_public_key",
        ExactSpelling = true)]
    public static extern void kp_lsw_free_public_key([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_kp_lsw_free_secret_key",
        ExactSpelling = true)]
    public static extern void kp_lsw_free_secret_key([NativeTypeName("const void *")] IntPtr ptr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rabe_kp_lsw_free_ciphertext",
        ExactSpelling = true)]
    public static extern void kp_lsw_free_ciphertext([NativeTypeName("const void *")] IntPtr ptr);
}

