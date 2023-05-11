using System;
using System.Text;
using System.Text.Json;
using NUnit.Framework;
using Rabe.Core.CPABE.AC17;

namespace Test.CPABE;

public class AC17Test
{
    private MasterKey _masterKey;
    private PublicKey _publicKey;

    [SetUp]
    public void Setup()
    {
        var (masterKey, publicKey) = Context.Init();
        _masterKey = masterKey;
        _publicKey = publicKey;
    }

    [Test]
    public void CPABETest()
    {
        var secretKey = _masterKey.KeyGen(new[] { "Doctor", "Teacher" });
        var secretKeyJson = JsonSerializer.Serialize(secretKey);

        Console.WriteLine(secretKeyJson);
        var secretKeyDeserialized = JsonSerializer.Deserialize<SecretKey>(secretKeyJson);
        Assert.AreNotEqual(secretKeyDeserialized, null);
        var message = Encoding.Default.GetBytes("dsafsadsa");
        var cipher = _publicKey.Encrypt("\"Doctor\" and \"Teacher\"", message);
        Assert.AreNotEqual(cipher, null);
        var cipherJson = JsonSerializer.Serialize(cipher);
        Console.WriteLine(cipherJson);
        var cipherDeserialized = JsonSerializer.Deserialize<Cipher>(cipherJson);
        var text = secretKeyDeserialized!.Decrypt(cipherDeserialized!);
        Assert.AreEqual(text, message);
    }
}
