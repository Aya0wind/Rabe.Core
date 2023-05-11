using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Rabe.Core.CPABE.AW11;

internal class PublicKeyJsonConverter : JsonConverter<PublicKey>
{
    public override PublicKey Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (JsonDocument document = JsonDocument.ParseValue(ref reader))
        {
            var rawText = document.RootElement.GetRawText();
            var handle = RabeNative.cp_aw11_public_key_from_json(rawText);
            if (handle == IntPtr.Zero)
                throw Common.GetLastWrappedException();
            return new PublicKey(handle);
        }
    }

    public override void Write(Utf8JsonWriter writer, PublicKey value, JsonSerializerOptions options)
    {
        //convert a PublicKey to a json object
        using var json = RabeNative.cp_aw11_public_key_to_json(value.Handle).ToNativeString();
        writer.WriteRawValue(Marshal.PtrToStringAnsi(json.Handle)!);
    }
}

internal class MasterKeyJsonConverter : JsonConverter<MasterKey>
{
    public override MasterKey Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (JsonDocument document = JsonDocument.ParseValue(ref reader))
        {
            var rawText = document.RootElement.GetRawText();
            var handle = RabeNative.cp_aw11_master_key_from_json(rawText);
            if (handle == IntPtr.Zero)
                throw Common.GetLastWrappedException();
            return new MasterKey(handle);
        }
    }

    public override void Write(Utf8JsonWriter writer, MasterKey value, JsonSerializerOptions options)
    {
        //convert a MasterKey to a json object
        using var json = RabeNative.cp_aw11_master_key_to_json(value.Handle).ToNativeString();
        writer.WriteRawValue(Marshal.PtrToStringAnsi(json.Handle)!);
    }
}

internal class SecretKeyJsonConverter : JsonConverter<SecretKey>
{
    public override SecretKey Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (JsonDocument document = JsonDocument.ParseValue(ref reader))
        {
            var rawText = document.RootElement.GetRawText();
            var handle = RabeNative.cp_aw11_secret_key_from_json(rawText);
            if (handle == IntPtr.Zero)
                throw Common.GetLastWrappedException();
            return new SecretKey(handle);
        }
    }


    public override void Write(Utf8JsonWriter writer, SecretKey value, JsonSerializerOptions options)
    {   //convert a SecretKey to a json object
        using var json = RabeNative.cp_aw11_secret_key_to_json(value.Handle).ToNativeString();
        writer.WriteRawValue(Marshal.PtrToStringAnsi(json.Handle)!);
    }
}

internal class CipherJsonConverter : JsonConverter<Cipher>
{
    public override Cipher Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (JsonDocument document = JsonDocument.ParseValue(ref reader))
        {
            var rawText = document.RootElement.GetRawText();
            var handle = RabeNative.cp_aw11_ciphertext_from_json(rawText);
            if (handle == IntPtr.Zero)
                throw Common.GetLastWrappedException();
            return new Cipher(handle);
        }
    }

    public override void Write(Utf8JsonWriter writer, Cipher value, JsonSerializerOptions options)
    {
        //convert a Cipher to a json object
        using var json = RabeNative.cp_aw11_ciphertext_to_json(value.Handle).ToNativeString();
        writer.WriteRawValue(Marshal.PtrToStringAnsi(json.Handle)!);
    }
}


internal class GlobalKeyJsonConverter : JsonConverter<GlobalKey>
{
    public override GlobalKey Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (JsonDocument document = JsonDocument.ParseValue(ref reader))
        {
            var rawText = document.RootElement.GetRawText();
            var handle = RabeNative.cp_aw11_global_key_from_json(rawText);
            if (handle == IntPtr.Zero)
                throw Common.GetLastWrappedException();
            return new GlobalKey(handle);
        }
    }

    public override void Write(Utf8JsonWriter writer, GlobalKey value, JsonSerializerOptions options)
    {
        //convert a Cipher to a json object
        using var json = RabeNative.cp_aw11_global_key_to_json(value.Handle).ToNativeString();
        writer.WriteRawValue(Marshal.PtrToStringAnsi(json.Handle)!);
    }
}
