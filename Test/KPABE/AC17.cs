using System;
using System.Text;
using System.Text.Json;
using NUnit.Framework;
using Rabe.KPABE.AC17;
using Context = Rabe.KPABE.AC17.Context;

namespace Test.KPABE;
public class AC17Test
{
    private Rabe.CPABE.AC17.MasterKey _masterKey;
    private Rabe.CPABE.AC17.PublicKey _publicKey;
    [SetUp]
    public void Setup()
    {
        var (masterKey,publicKey) = Context.Init();
        Console.WriteLine("Master and Public Key serialize test start");
        var masterKeyJson = JsonSerializer.Serialize(masterKey);
        var publicKeyJson = JsonSerializer.Serialize(publicKey);
        Console.WriteLine(masterKeyJson);
        Console.WriteLine(publicKeyJson);
        _masterKey = JsonSerializer.Deserialize<Rabe.CPABE.AC17.MasterKey>(masterKeyJson)!;
        _publicKey = JsonSerializer.Deserialize<Rabe.CPABE.AC17.PublicKey>(publicKeyJson)!;
        Console.WriteLine("Master Key serialize test end");
    }
    
    [Test]
    public void KPABETest()
    {
        Console.WriteLine("KPABE test start");
        SecretKey secretKey = Extension.KeyGen(_masterKey,"\"A\" and \"B\"");
        Console.WriteLine("Secret Key serialize test start");
        var secretKeyJson = JsonSerializer.Serialize(secretKey);
        Console.WriteLine(secretKeyJson);
        var secretKeyDeserialized = JsonSerializer.Deserialize<SecretKey>(secretKeyJson);
        Console.WriteLine("Secret Key serialize test end");
        var message = Encoding.Default.GetBytes("dsafsadsa");
        Console.WriteLine("Encrypt test start");
        var cipher = Extension.Encrypt(_publicKey,new[]{"A", "B"}, message );
        Console.WriteLine("Encrypt test end");
        Console.WriteLine("Cipher serialize test start");
        var cipherJson = JsonSerializer.Serialize(cipher);
        Console.WriteLine(cipherJson);
        var cipherDeserialized = JsonSerializer.Deserialize<Cipher>(cipherJson);
        Console.WriteLine("Decrypt test start");
        var text = secretKeyDeserialized.Decrypt(cipherDeserialized);
        Console.WriteLine("Decrypt test end");
        Assert.AreEqual(text,message);
    }
    
}