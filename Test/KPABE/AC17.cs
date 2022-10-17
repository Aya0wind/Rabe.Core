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
        SecretKey secretKey = Extension.KeyGen(_masterKey,"\"A\" and \"B\"");
        var secretKeyJson = JsonSerializer.Serialize(secretKey);
        secretKey = JsonSerializer.Deserialize<SecretKey>(secretKeyJson)!;
        var message = Encoding.Default.GetBytes("dsafsadsa");
        var cipher = Extension.Encrypt(_publicKey,new[]{"A", "B"}, message );
        var cipherJson = JsonSerializer.Serialize(cipher);
        cipher = JsonSerializer.Deserialize<Cipher>(cipherJson)!;
        var text = Extension.Decrypt(secretKey, cipher);
        Assert.AreEqual(text,message);
    }
    
}