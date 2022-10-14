using System;
using System.Text;
using System.Text.Json;
using NUnit.Framework;
using Rabe.CPABE.AW11;
namespace Test.CPABE;
public class AW11Test
{
    private GlobalKey _globalKey;
    [SetUp]
    public void Setup()
    {
        var globalKey = Context.Init();
        //globalKey Serialize test
        var globalKeyJson = JsonSerializer.Serialize(globalKey);
        Console.WriteLine(globalKeyJson);
        _globalKey = JsonSerializer.Deserialize<GlobalKey>(globalKeyJson)!;
    }

    [Test]
    public void CPABETest()
    {
        var (masterKey,publicKey) = Extension.AuthorityGen(_globalKey,new[]{"A","B"});
        //masterKey Serialize test
        var masterKeyJson = JsonSerializer.Serialize(masterKey);
        Console.WriteLine(masterKeyJson);
        masterKey = JsonSerializer.Deserialize<MasterKey>(masterKeyJson)!;
        //publicKey Serialize test
        var publicKeyJson = JsonSerializer.Serialize(publicKey);
        Console.WriteLine(publicKeyJson);
        publicKey = JsonSerializer.Deserialize<PublicKey>(publicKeyJson)!;

        var secretKey = Extension.KeyGen(_globalKey,masterKey,"A",new []{"A","B"});
        //secretKey Serialize test
        var secretKeyJson = JsonSerializer.Serialize(secretKey);
        Console.WriteLine(secretKeyJson);
        secretKey = JsonSerializer.Deserialize<SecretKey>(secretKeyJson)!;
        //encrypt test
        var message = Encoding.Default.GetBytes("Hello world");
        var cipher = Extension.Encrypt(_globalKey,new []{publicKey},"\"A\" and \"B\"", message );
        //cipher Serialize test
        var cipherJson = JsonSerializer.Serialize(cipher);
        Console.WriteLine(cipherJson);
        cipher = JsonSerializer.Deserialize<Cipher>(cipherJson)!;
        //decrypt test
        var text = Extension.Decrypt(_globalKey, secretKey, cipher);
        Assert.AreEqual(text,message);
    }
}