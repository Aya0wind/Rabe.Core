using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
namespace Rabe.CPABE.MKE08;

internal class PublicKeyJsonConverter : JsonConverter<PublicKey>
{
    public override PublicKey Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (JsonDocument document = JsonDocument.ParseValue(ref reader))
        {
            var rawText = document.RootElement.GetRawText();
            var handle = RabeNative.cp_mke08_public_key_from_json(rawText);
            if (handle == IntPtr.Zero)
                throw Common.GetLastWrappedException();
            return new PublicKey(handle);
        }
    }

    public override void Write(Utf8JsonWriter writer, PublicKey value, JsonSerializerOptions options)
    {
        //convert a PublicKey to a json object
        var json = RabeNative.cp_mke08_public_key_to_json(value.Handle);
        if (json == IntPtr.Zero)
            throw Common.GetLastWrappedException();
        try
        {
            writer.WriteRawValue(Marshal.PtrToStringAnsi(json)!);
        }
        finally
        {
            RabeNative.free_json(json);
        }
    }
}

internal class PublicAttributeKeyJsonConverter : JsonConverter<PublicAttributeKey>
{
    public override PublicAttributeKey Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (JsonDocument document = JsonDocument.ParseValue(ref reader))
        {
            var rawText = document.RootElement.GetRawText();
            var handle = RabeNative.cp_mke08_public_attribute_key_from_json(rawText);
            if (handle == IntPtr.Zero)
                throw Common.GetLastWrappedException();
            return new PublicAttributeKey(handle);
        }
    }

    public override void Write(Utf8JsonWriter writer, PublicAttributeKey value, JsonSerializerOptions options)
    {
        //convert a PublicKey to a json object
        var json = RabeNative.cp_mke08_public_attribute_key_to_json(value.Handle);
        if (json == IntPtr.Zero)
            throw Common.GetLastWrappedException();
        try
        {
            writer.WriteRawValue(Marshal.PtrToStringAnsi(json)!);
        }
        finally
        {
            RabeNative.free_json(json);
        }
    }
}
internal class PublicUserKeyJsonConverter : JsonConverter<PublicUserKey>
{
    public override PublicUserKey Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (JsonDocument document = JsonDocument.ParseValue(ref reader))
        {
            var rawText = document.RootElement.GetRawText();
            var handle = RabeNative.cp_mke08_public_user_key_from_json(rawText);
            if (handle == IntPtr.Zero)
                throw Common.GetLastWrappedException();
            return new PublicUserKey(handle);
        }
    }

    public override void Write(Utf8JsonWriter writer, PublicUserKey value, JsonSerializerOptions options)
    {
        //convert a PublicKey to a json object
        var json = RabeNative.cp_mke08_public_attribute_key_to_json(value.Handle);
        if (json == IntPtr.Zero)
            throw Common.GetLastWrappedException();
        try
        {
            writer.WriteRawValue(Marshal.PtrToStringAnsi(json)!);
        }
        finally
        {
            RabeNative.free_json(json);
        }
    }
}

internal class MasterKeyJsonConverter : JsonConverter<MasterKey>
{
    public override MasterKey Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (JsonDocument document = JsonDocument.ParseValue(ref reader))
        {
            var rawText = document.RootElement.GetRawText();
            var handle = RabeNative.cp_mke08_master_key_from_json(rawText);
            if (handle == IntPtr.Zero)
                throw Common.GetLastWrappedException();
            return new MasterKey(handle);
        }
    }

    public override void Write(Utf8JsonWriter writer, MasterKey value, JsonSerializerOptions options)
    {
        //convert a MasterKey to a json object
        var json = RabeNative.cp_mke08_master_key_to_json(value.Handle);
        if (json == IntPtr.Zero)
            throw Common.GetLastWrappedException();
        try
        {
            writer.WriteRawValue(Marshal.PtrToStringAnsi(json)!);
        }
        finally
        {
            RabeNative.free_json(json);
        }
    }
}

internal class SecretAttributeKeyJsonConverter : JsonConverter<SecretAttributeKey>
{
    public override SecretAttributeKey Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (JsonDocument document = JsonDocument.ParseValue(ref reader))
        {
            var rawText = document.RootElement.GetRawText();
            var handle = RabeNative.cp_mke08_secret_attribute_key_from_json(rawText);
            if (handle == IntPtr.Zero)
                throw Common.GetLastWrappedException();
            return new SecretAttributeKey(handle);
        }
    }


    public override void Write(Utf8JsonWriter writer, SecretAttributeKey value, JsonSerializerOptions options)
    {   //convert a SecretKey to a json object
        var json = RabeNative.cp_mke08_secret_attribute_key_to_json(value.Handle);
        if (json == IntPtr.Zero)
            throw Common.GetLastWrappedException();
        try
        {
            writer.WriteRawValue(Marshal.PtrToStringAnsi(json)!);
        }
        finally
        {
            RabeNative.free_json(json);
        }
    }
}

internal class SecretAuthorityKeyJsonConverter : JsonConverter<SecretAuthorityKey>
{
    public override SecretAuthorityKey Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (JsonDocument document = JsonDocument.ParseValue(ref reader))
        {
            var rawText = document.RootElement.GetRawText();
            var handle = RabeNative.cp_mke08_secret_attribute_key_from_json(rawText);
            if (handle == IntPtr.Zero)
                throw Common.GetLastWrappedException();
            return new SecretAuthorityKey(handle);
        }
    }


    public override void Write(Utf8JsonWriter writer, SecretAuthorityKey value, JsonSerializerOptions options)
    {   //convert a SecretKey to a json object
        var json = RabeNative.cp_mke08_secret_attribute_key_to_json(value.Handle);
        if (json == IntPtr.Zero)
            throw Common.GetLastWrappedException();
        try
        {
            writer.WriteRawValue(Marshal.PtrToStringAnsi(json)!);
        }
        finally
        {
            RabeNative.free_json(json);
        }
    }
}
internal class SecretUserKeyJsonConverter : JsonConverter<SecretUserKey>
{
    public override SecretUserKey Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (JsonDocument document = JsonDocument.ParseValue(ref reader))
        {
            var rawText = document.RootElement.GetRawText();
            var handle = RabeNative.cp_mke08_secret_attribute_key_from_json(rawText);
            if (handle == IntPtr.Zero)
                throw Common.GetLastWrappedException();
            return new SecretUserKey(handle);
        }
    }


    public override void Write(Utf8JsonWriter writer, SecretUserKey value, JsonSerializerOptions options)
    {   //convert a SecretKey to a json object
        var json = RabeNative.cp_mke08_secret_attribute_key_to_json(value.Handle);
        if (json == IntPtr.Zero)
            throw Common.GetLastWrappedException();
        try
        {
            writer.WriteRawValue(Marshal.PtrToStringAnsi(json)!);
        }
        finally
        {
            RabeNative.free_json(json);
        }
    }
}

internal class CipherJsonConverter : JsonConverter<Cipher>
{
    public override Cipher Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (JsonDocument document = JsonDocument.ParseValue(ref reader))
        {
            var rawText = document.RootElement.GetRawText();
            var handle = RabeNative.cp_mke08_ciphertext_from_json(rawText);
            if (handle == IntPtr.Zero)
                throw Common.GetLastWrappedException();
            return new Cipher(handle);
        }
    }

    public override void Write(Utf8JsonWriter writer, Cipher value, JsonSerializerOptions options)
    {
        //convert a Cipher to a json object
        var json = RabeNative.cp_mke08_ciphertext_to_json(value.Handle);
        if (json == IntPtr.Zero)
            throw Common.GetLastWrappedException();
        try
        {
            writer.WriteRawValue(Marshal.PtrToStringAnsi(json)!);
        }
        finally
        {
            RabeNative.free_json(json);
        }
    }
}

internal class UserKeyJsonConverter : JsonConverter<UserKey>
{
    public override UserKey Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (JsonDocument document = JsonDocument.ParseValue(ref reader))
        {
            var rawText = document.RootElement.GetRawText();
            var handle = RabeNative.cp_mke08_user_key_from_json(rawText);
            if (handle == IntPtr.Zero)
                throw Common.GetLastWrappedException();
            return new UserKey(handle);
        }
    }

    public override void Write(Utf8JsonWriter writer, UserKey value, JsonSerializerOptions options)
    {
        //convert a Cipher to a json object
        var json = RabeNative.cp_mke08_user_key_to_json(value.Handle);
        if (json == IntPtr.Zero)
            throw Common.GetLastWrappedException();
        try
        {
            writer.WriteRawValue(Marshal.PtrToStringAnsi(json)!);
        }
        finally
        {
            RabeNative.free_json(json);
        }
    }
}
