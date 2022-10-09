using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using Rabe.CPABE;

namespace Rabe;

internal class PublicKeyJsonConverter : JsonConverter<PublicKey>
{
    public override PublicKey Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        //read a json object and convert it to a PublicKey
        var json = reader.GetString();
        var handle = NativeLibCommon.DeserializePubKey(json!);
        return new PublicKey(handle);
    }

    public override void Write(Utf8JsonWriter writer, PublicKey value, JsonSerializerOptions options)
    {
        //convert a PublicKey to a json object
        var json = NativeLibCommon.PubKeyToJson(value.Handle);
        if (json == IntPtr.Zero)
            throw new Exception("Failed to convert PublicKey to json");
        writer.WriteStringValue(Marshal.PtrToStringAnsi(json));
        NativeLibCommon.FreeJson(json);
    }
}

internal class MasterKeyJsonConverter : JsonConverter<MasterKey>
{
    public override MasterKey Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        //read a json object and convert it to a MasterKey
        var json = reader.GetString();
        var handle = NativeLibCommon.DeserializeMasterKey(json!);
        if (handle == IntPtr.Zero)
            throw new Exception("Failed to deserialize MasterKey");
        return new MasterKey(handle);
    }

    public override void Write(Utf8JsonWriter writer, MasterKey value, JsonSerializerOptions options)
    {
        //convert a MasterKey to a json object
        var json = NativeLibCommon.MasterKeyToJson(value.Handle);
        if (json == IntPtr.Zero)
            throw new Exception("Failed to convert MasterKey to json");
        writer.WriteStringValue(Marshal.PtrToStringAnsi(json));
        NativeLibCommon.FreeJson(json);
    }
}

internal class CpSecretKeyJsonConverter : JsonConverter<SecretKey>
{
    public override SecretKey Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        //read a json object and convert it to a SecretKey
        var json = reader.GetString();
        var handle = NativeLibCp.DeserializeSecretKey(json!);
        if (handle == IntPtr.Zero)
            throw new Exception("Failed to deserialize SecretKey");
        return new SecretKey(handle);
    }


    public override void Write(Utf8JsonWriter writer, SecretKey value, JsonSerializerOptions options)
    {
        //convert a SecretKey to a json object
        var json = NativeLibCp.SecKeyToJson(value.Handle);
        if (json == IntPtr.Zero)
            throw new Exception("Failed to convert SecretKey to json");
        writer.WriteStringValue(Marshal.PtrToStringAnsi(json));
        NativeLibCommon.FreeJson(json);
    }
}

internal class CpCipherJsonConverter : JsonConverter<Cipher>
{
    public override Cipher Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        //read a json object and convert it to a Cipher
        var json = reader.GetString();
        var handle = NativeLibCp.DeserializeCipher(json!);
        if (handle == IntPtr.Zero)
            throw new Exception("Failed to deserialize Cipher");
        return new Cipher(handle);
    }

    public override void Write(Utf8JsonWriter writer, Cipher value, JsonSerializerOptions options)
    {
        //convert a Cipher to a json object
        var json = NativeLibCp.CipherToJson(value.Handle);
        if (json == IntPtr.Zero)
            throw new Exception("Failed to convert Cipher to json");
        writer.WriteStringValue(Marshal.PtrToStringAnsi(json));
        NativeLibCommon.FreeJson(json);
    }
}

internal class KpSecretKeyJsonConverter : JsonConverter<KPABE.SecretKey>
{
    public override KPABE.SecretKey Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        //read a json object and convert it to a SecretKey
        var json = reader.GetString();
        var handle = NativeLibKp.DeserializeSecretKey(json!);
        if (handle == IntPtr.Zero)
            throw new Exception("Failed to deserialize SecretKey");
        return new KPABE.SecretKey(handle);
    }


    public override void Write(Utf8JsonWriter writer, KPABE.SecretKey value, JsonSerializerOptions options)
    {
        //convert a SecretKey to a json object
        var json = NativeLibKp.SecKeyToJson(value.Handle);
        if (json == IntPtr.Zero)
            throw new Exception("Failed to convert SecretKey to json");
        writer.WriteStringValue(Marshal.PtrToStringAnsi(json));
        NativeLibCommon.FreeJson(json);
    }
}

internal class KpCipherJsonConverter : JsonConverter<KPABE.Cipher>
{
    public override KPABE.Cipher Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        //read a json object and convert it to a Cipher
        var json = reader.GetString();
        var handle = NativeLibKp.DeserializeCipher(json!);
        if (handle == IntPtr.Zero)
            throw new Exception("Failed to deserialize Cipher");
        return new KPABE.Cipher(handle);
    }

    public override void Write(Utf8JsonWriter writer, KPABE.Cipher value, JsonSerializerOptions options)
    {
        //convert a Cipher to a json object
        var json = NativeLibKp.CipherToJson(value.Handle);
        if (json == IntPtr.Zero)
            throw new Exception("Failed to convert Cipher to json");
        writer.WriteStringValue(Marshal.PtrToStringAnsi(json));
        NativeLibCommon.FreeJson(json);
    }
}