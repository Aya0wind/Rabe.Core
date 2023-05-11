using System;
using System.Text;
using System.Text.Json;
using NUnit.Framework;
using Rabe.Core.CPABE.MKE08;
namespace Test.CPABE;
public class MKE08Test
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
        Assert.NotNull(masterKey);
        Assert.NotNull(publicKey);
        _masterKey = masterKey;
        _publicKey = publicKey;
    }

    [Test]
    public void CPABETest()
    {
        //generate user key
        UserKey userKey = Extension.UserKeyGen(_publicKey, _masterKey, "user1");
        //user key serialize test
        var userKeyJson = JsonSerializer.Serialize(userKey);
        Console.WriteLine(userKeyJson);
        userKey = JsonSerializer.Deserialize<UserKey>(userKeyJson)!;
        //generate secret authority key
        SecretAuthorityKey secretAuthorityKey = Extension.SecretAuthorityKeyGen("aa");
        //secret authority key serialize test
        var secretAuthorityKeyJson = JsonSerializer.Serialize(secretAuthorityKey);
        Console.WriteLine(secretAuthorityKeyJson);
        secretAuthorityKey = JsonSerializer.Deserialize<SecretAuthorityKey>(secretAuthorityKeyJson)!;
        //generate secret authority key1
        SecretAuthorityKey secretAuthorityKey1 = Extension.SecretAuthorityKeyGen("aa1");

        //generate public attribute key
        PublicAttributeKey? publicAttributeKey = Extension.PublicAttributeKeyGen(_publicKey, secretAuthorityKey, "aa::A");
        //public attribute key serialize test
        string publicAttributeKeyJson = JsonSerializer.Serialize(publicAttributeKey);
        Console.WriteLine(publicAttributeKeyJson);
        publicAttributeKey = JsonSerializer.Deserialize<PublicAttributeKey>(publicAttributeKeyJson);
        Assert.NotNull(publicAttributeKey);
        //generate public attribute key1
        PublicAttributeKey publicAttributeKey1 = Extension.PublicAttributeKeyGen(_publicKey, secretAuthorityKey1, "aa1::B");

        //add attribute key to user key
        Extension.AddAttributeToUserKey(userKey, secretAuthorityKey, "aa::A");
        Extension.AddAttributeToUserKey(userKey, secretAuthorityKey1, "aa1::B");
        //encrypt
        var text = Encoding.Default.GetBytes("hello world");
        var cipher = Extension.Encrypt(_publicKey, new[] { publicAttributeKey!, publicAttributeKey1 }, "\"aa::A\" and \"aa1::B\"", text);
        //cipher serialize test
        var cipherJson = JsonSerializer.Serialize(cipher);
        Console.WriteLine(cipherJson);
        cipher = JsonSerializer.Deserialize<Cipher>(cipherJson)!;
        var message = Extension.Decrypt(userKey, _publicKey, cipher);
        Assert.AreEqual(text, message);
    }
}
