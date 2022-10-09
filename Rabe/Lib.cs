using System.Text.Json.Serialization;

namespace Rabe;

[JsonConverter(typeof(PublicKeyJsonConverter))]
public class PublicKey : IDisposable
{
    public PublicKey(IntPtr handle)
    {
        Handle = handle;
    }

    public IntPtr Handle { get; }

    public void Dispose()
    {
        if (Handle != IntPtr.Zero)
            NativeLibCommon.FreePubKey(Handle);
    }
}

[JsonConverter(typeof(MasterKeyJsonConverter))]
public class MasterKey : IDisposable
{
    public MasterKey(IntPtr handle)
    {
        Handle = handle;
    }

    public IntPtr Handle { get; }

    public void Dispose()
    {
        if (Handle != IntPtr.Zero)
            NativeLibCommon.FreeMasterKey(Handle);
    }
}

public static class RabeContext
{
    public static ValueTuple<PublicKey, MasterKey> Init()
    {
        var initResult = NativeLibCommon.Init();
        if (initResult.masterKey == IntPtr.Zero || initResult.pubKey == IntPtr.Zero)
            throw new Exception("Failed to initialize public and master key");

        return (new PublicKey(initResult.pubKey), new MasterKey(initResult.masterKey));
    }
}