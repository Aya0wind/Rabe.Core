using System;
using System.Text;
using System.Text.Json;
using NUnit.Framework;
using Rabe.Core.KPABE.AC17;
using MasterKey = Rabe.Core.CPABE.AC17.MasterKey;
using PublicKey = Rabe.Core.CPABE.AC17.PublicKey;
namespace Test.KPABE;
public class AC17Test
{
    private MasterKey _masterKey;
    private PublicKey _publicKey;
    [SetUp]
    public void Setup()
    {
        var (masterKey, publicKey) = Context.Init();
        Console.WriteLine("Master and Public Key serialize test start");
        var masterKeyJson = JsonSerializer.Serialize(masterKey);
        var publicKeyJson = JsonSerializer.Serialize(publicKey);
        Console.WriteLine(masterKeyJson);
        Console.WriteLine(publicKeyJson);
        _masterKey = JsonSerializer.Deserialize<MasterKey>(masterKeyJson)!;
        _publicKey = JsonSerializer.Deserialize<PublicKey>(publicKeyJson)!;
        Console.WriteLine("Master Key serialize test end");
    }

    [Test]
    public void KPABETest()
    {
        SecretKey secretKey = Extension.KeyGen(_masterKey, "\"A\" and \"B\"");
        var secretKeyJson = JsonSerializer.Serialize(secretKey);
        secretKey = JsonSerializer.Deserialize<SecretKey>(secretKeyJson)!;
        var message = Encoding.Default.GetBytes("Hello World!");
        var cipher = Extension.Encrypt(_publicKey, new[] { "A", "B" }, message);
        var cipherJson = JsonSerializer.Serialize(cipher);
        cipher = JsonSerializer.Deserialize<Cipher>(cipherJson)!;
        var text = Extension.Decrypt(secretKey, cipher);
        Assert.AreEqual(text, message);
    }

}
