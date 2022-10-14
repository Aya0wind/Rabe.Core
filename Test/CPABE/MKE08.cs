using System;
using System.Text;
using System.Text.Json;
using NUnit.Framework;
using Rabe.CPABE.MKE08;
namespace Test.CPABE;
public class MKE08Test
{
    private MasterKey _masterKey;
    private PublicKey _publicKey;
    [SetUp]
    public void Setup()
    {
        var (masterKey,publicKey) = Context.Init();
        _masterKey = masterKey;
        _publicKey = publicKey;
    }

    [Test]
    public void CPABETest()
    {
        SecretKey secretKey = Extension.KeyGen(_masterKey,new[]{"医生", "老师"});
        var secretKeyJson = JsonSerializer.Serialize(secretKey);
        Console.WriteLine(secretKeyJson);
        var secretKeyDeserialized = JsonSerializer.Deserialize<SecretKey>(secretKeyJson);
        var message = Encoding.Default.GetBytes("dsafsadsa");
        var cipher = Extension.Encrypt(_publicKey,"\"医生\" and \"老师\"", message );
        var cipherJson = JsonSerializer.Serialize(cipher);
        Console.WriteLine(cipherJson);
        var cipherDeserialized = JsonSerializer.Deserialize<Cipher>(cipherJson);
        var text = secretKeyDeserialized.Decrypt(cipherDeserialized);
        Assert.AreEqual(text,message);
    }
}