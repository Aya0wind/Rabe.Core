using System;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Rabe;
using NUnit.Framework;

namespace Test;

public class Tests
{
    private MasterKey _masterKey;
    private PublicKey _publicKey;
    [SetUp]
    public void Setup()
    {
        var (publicKey, masterKey) = RabeContext.Init();
        _masterKey = masterKey;
        _publicKey = publicKey;
    }

    [Test]
    public void CPABETest()
    {
        Rabe.CPABE.SecretKey secretKey = Rabe.CPABE.CPABEExtension.KeyGen(_masterKey,new string[]{"A", "B"});
        var secretKeyJson = JsonSerializer.Serialize(secretKey, new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true,
        });
        Console.WriteLine(secretKeyJson);
        var secretKeyDeserialized = JsonSerializer.Deserialize<Rabe.CPABE.SecretKey>(secretKeyJson);
        var message = Encoding.Default.GetBytes("dsafsadsa");
        var cipher = Rabe.CPABE.CPABEExtension.Encrypt(_publicKey,"\"A\" and \"B\"", message );
        var cipherJson = JsonSerializer.Serialize(cipher, new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true,
        });
        Console.WriteLine(cipherJson);
        var cipherDeserialized = JsonSerializer.Deserialize<Rabe.CPABE.Cipher>(cipherJson);
        var text = secretKeyDeserialized.Decrypt(cipherDeserialized);
        Assert.AreEqual(text,message);
    }
    
    [Test]
    public void KPABETest()
    {
        Rabe.KPABE.SecretKey secretKey = Rabe.KPABE.KPABEExtension.KeyGen(_masterKey,"\"A\" and \"B\"");
        var secretKeyJson = JsonSerializer.Serialize(secretKey, new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true,
        });
        Console.WriteLine(secretKeyJson);
        var secretKeyDeserialized = JsonSerializer.Deserialize<Rabe.KPABE.SecretKey>(secretKeyJson);
        var message = Encoding.Default.GetBytes("dsafsadsa");
        var cipher = Rabe.KPABE.KPABEExtension.Encrypt(_publicKey,new string[]{"A", "B"}, message );
        var cipherJson = JsonSerializer.Serialize(cipher, new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true,
        });
        Console.WriteLine(cipherJson);
        var cipherDeserialized = JsonSerializer.Deserialize<Rabe.KPABE.Cipher>(cipherJson);
        var text = secretKeyDeserialized.Decrypt(cipherDeserialized);
        Assert.AreEqual(text,message);
    }
    
}