using System;
using System.Text;
using System.Text.Json;
using NUnit.Framework;
using Rabe.Core.CPABE.BSW;
namespace Test.CPABE;
public class BSWTest
{
    private MasterKey _masterKey;
    private PublicKey _publicKey;
    [SetUp]
    public void Setup()
    {
        var (masterKey, publicKey) = Context.Init();
        //master key and public key serialize test
        var masterKeyJson = JsonSerializer.Serialize(masterKey);
        Console.WriteLine(masterKeyJson);
        var publicKeyJson = JsonSerializer.Serialize(publicKey);
        Console.WriteLine(publicKeyJson);
        masterKey = JsonSerializer.Deserialize<MasterKey>(masterKeyJson);
        publicKey = JsonSerializer.Deserialize<PublicKey>(publicKeyJson);
        _masterKey = masterKey;
        _publicKey = publicKey;
    }

    [Test]
    public void CPABETest()
    {
        SecretKey secretKey = Extension.KeyGen(_masterKey, _publicKey, new[] { "A", "B" });
        var secretKeyJson = JsonSerializer.Serialize(secretKey);
        Console.WriteLine(secretKeyJson);
        var secretKeyDeserialized = JsonSerializer.Deserialize<SecretKey>(secretKeyJson);
        var message = Encoding.Default.GetBytes("Hello World");
        var cipher = Extension.Encrypt(_publicKey, "\"A\" and \"B\"", message);
        var cipherJson = JsonSerializer.Serialize(cipher);
        Console.WriteLine(cipherJson);
        var cipherDeserialized = JsonSerializer.Deserialize<Cipher>(cipherJson);
        var text = secretKeyDeserialized!.Decrypt(cipherDeserialized!);
        Assert.AreEqual(text, message);
    }
}
