using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Rabe.CPABE;

namespace Rabe;

internal class PublicKeyJsonConverter : JsonConverter<PublicKey>
{
    public override PublicKey Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (JsonDocument document = JsonDocument.ParseValue(ref reader))
        {
            var rawText = document.RootElement.GetRawText();
            var handle = NativeLibCommon.DeserializePubKey(rawText);
            if (handle == IntPtr.Zero)
                throw new Exception("Failed to deserialize publicKey");
            return new PublicKey(handle);
        }
    }

    public override void Write(Utf8JsonWriter writer, PublicKey value, JsonSerializerOptions options)
    {
        //convert a PublicKey to a json object
        var json = NativeLibCommon.PubKeyToJson(value.Handle);
        if (json == IntPtr.Zero)
            throw new Exception("Failed to convert publicKey to json");
        writer.WriteRawValue(Marshal.PtrToStringAnsi(json)!);
        NativeLibCommon.FreeJson(json);
    }
}

internal class MasterKeyJsonConverter : JsonConverter<MasterKey>
{
    public override MasterKey Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (JsonDocument document = JsonDocument.ParseValue(ref reader))
        {
            var rawText = document.RootElement.GetRawText();
            var handle = NativeLibCommon.DeserializeMasterKey(rawText);
            if (handle == IntPtr.Zero)
                throw new Exception("Failed to deserialize masterKey");
            return new MasterKey(handle);
        }
    }

    public override void Write(Utf8JsonWriter writer, MasterKey value, JsonSerializerOptions options)
    {
        //convert a MasterKey to a json object
        var json = NativeLibCommon.MasterKeyToJson(value.Handle);
        if (json == IntPtr.Zero)
            throw new Exception("Failed to convert masterKey to json");
        writer.WriteRawValue(Marshal.PtrToStringAnsi(json)!);
        NativeLibCommon.FreeJson(json);
    }
}

internal class CpSecretKeyJsonConverter : JsonConverter<SecretKey>
{
    public override SecretKey Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (JsonDocument document = JsonDocument.ParseValue(ref reader))
        {
            var rawText = document.RootElement.GetRawText();
            var handle = NativeLibCp.DeserializeSecretKey(rawText);
            if (handle == IntPtr.Zero)
                throw new Exception("Failed to deserialize CpSecretKey");
            return new SecretKey(handle);
        }
    }


    public override void Write(Utf8JsonWriter writer, SecretKey value, JsonSerializerOptions options)
    {
        //convert a SecretKey to a json object
        var json = NativeLibCp.SecKeyToJson(value.Handle);
        if (json == IntPtr.Zero)
            throw new Exception("Failed to convert CpSecretKey to json");
        var jsonString = Marshal.PtrToStringAnsi(json)!;
        writer.WriteRawValue(jsonString);
        NativeLibCommon.FreeJson(json);
    }
}

internal class CpCipherJsonConverter : JsonConverter<Cipher>
{
    public override Cipher Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (JsonDocument document = JsonDocument.ParseValue(ref reader))
        {
            var rawText = document.RootElement.GetRawText();
            var handle = NativeLibCp.DeserializeCipher(rawText);
            if (handle == IntPtr.Zero)
                throw new Exception("Failed to deserialize CpCipher");
            return new Cipher(handle);
        }
    }

    public override void Write(Utf8JsonWriter writer, Cipher value, JsonSerializerOptions options)
    {
        //convert a Cipher to a json object
        var json = NativeLibCp.CipherToJson(value.Handle);
        if (json == IntPtr.Zero)
            throw new Exception("Failed to convert Cipher to json");
        writer.WriteRawValue(Marshal.PtrToStringAnsi(json)!);
        NativeLibCommon.FreeJson(json);
    }
}

internal class KpSecretKeyJsonConverter : JsonConverter<KPABE.SecretKey>
{
    public override KPABE.SecretKey Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (JsonDocument document = JsonDocument.ParseValue(ref reader))
        {
            var rawText = document.RootElement.GetRawText();
            var handle = NativeLibKp.DeserializeSecretKey(rawText);
            if (handle == IntPtr.Zero)
                throw new Exception("Failed to deserialize CpSecretKey");
            return new KPABE.SecretKey(handle);
        }
    }


    public override void Write(Utf8JsonWriter writer, KPABE.SecretKey value, JsonSerializerOptions options)
    {
        //convert a SecretKey to a json object
        var json = NativeLibKp.SecKeyToJson(value.Handle);
        if (json == IntPtr.Zero)
            throw new Exception("Failed to convert SecretKey to json");
        writer.WriteRawValue(Marshal.PtrToStringAnsi(json)!);
        NativeLibCommon.FreeJson(json);
    }
}

internal class KpCipherJsonConverter : JsonConverter<KPABE.Cipher>
{
    public override KPABE.Cipher Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (JsonDocument document = JsonDocument.ParseValue(ref reader))
        {
            var rawText = document.RootElement.GetRawText();
            var handle = NativeLibKp.DeserializeCipher(rawText);
            if (handle == IntPtr.Zero)
                throw new Exception("Failed to deserialize KpCipher");
            return new KPABE.Cipher(handle);
        }
    }

    public override void Write(Utf8JsonWriter writer, KPABE.Cipher value, JsonSerializerOptions options)
    {
        //convert a Cipher to a json object
        var json = NativeLibKp.CipherToJson(value.Handle);
        if (json == IntPtr.Zero)
            throw new Exception("Failed to convert KpCipher to json");
        writer.WriteRawValue(Marshal.PtrToStringAnsi(json)!);
        NativeLibCommon.FreeJson(json);
    }
}