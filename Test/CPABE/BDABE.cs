using System;
using System.Text;
using System.Text.Json;
using NUnit.Framework;
using Rabe.CPABE.BDABE;
namespace Test.CPABE;
public class BDABETest
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
        var secretAuthorityKey = Extension.AuthorityKeyGen(_publicKey,_masterKey,"aa1");
        var userKey = Extension.UserKeyGen(_publicKey,secretAuthorityKey,"aa1");
        var publicAttributeKey = Extension.PublicAttributeKeyGen(_publicKey,secretAuthorityKey,"aa1::A");
        userKey.AddAttributeToUserKey(secretAuthorityKey,"aa1::A");
        var message = "hello world!";
        var policy = "\"aa1::A\" or \"aa1::B\"";
        var cipherText = Extension.Encrypt(_publicKey,new []{publicAttributeKey},policy,Encoding.UTF8.GetBytes(message));
        var plainText = Extension.Decrypt(userKey,_publicKey,cipherText);
        Assert.AreEqual(message,plainText);
    }
}