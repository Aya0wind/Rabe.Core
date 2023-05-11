using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace Rabe.Core.KPABE.AC17;

internal class SecretKeyJsonConverter : JsonConverter<SecretKey>
{
    public override SecretKey Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (JsonDocument document = JsonDocument.ParseValue(ref reader))
        {
            var rawText = document.RootElement.GetRawText();
            var handle = RabeNative.kp_ac17_secret_key_from_json(rawText);
            if (handle == IntPtr.Zero)
                throw Common.GetLastWrappedException();
            return new SecretKey(handle);
        }
    }


    public override void Write(Utf8JsonWriter writer, SecretKey value, JsonSerializerOptions options)
    {   //convert a SecretKey to a json object
        using var json = RabeNative.kp_ac17_secret_key_to_json(value.Handle).ToNativeString();
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
            var handle = RabeNative.kp_ac17_ciphertext_from_json(rawText);
            if (handle == IntPtr.Zero)
                throw Common.GetLastWrappedException();
            return new Cipher(handle);
        }
    }

    public override void Write(Utf8JsonWriter writer, Cipher value, JsonSerializerOptions options)
    {
        //convert a Cipher to a json object
        using var json = RabeNative.kp_ac17_ciphertext_to_json(value.Handle).ToNativeString();
        writer.WriteRawValue(Marshal.PtrToStringAnsi(json.Handle)!);
    }
}

