using System;
using NUnit.Framework;

namespace KSTest;

public class SecurityTests
{
    private string? _priKey;
    private string? _pubKey;
    private string _key;
    private string _message;
    private string _md5Hash;
    private string _sha1Hash;

    [SetUp]
    public void Setup()
    {
        _priKey = KS.Core.V1.Security.RsaCrypt.PriKey;
        _pubKey = KS.Core.V1.Security.RsaCrypt.PubKey;
        _key = Guid.NewGuid().ToString();
        _message = "test message";
        _md5Hash = "c72b9698fa1927e1dd12d3cf26ed84b2"; //from _message
        _sha1Hash = "35ee8386410d41d14b3f779fc95f4695f4851682"; //from _message
    }

    [Test]
    public void RsaTest()
    {
        var chyperMessage = KS.Core.V1.Security.RsaCrypt.Encrypt(_message, _pubKey);
        Assert.IsTrue(_message.Equals(KS.Core.V1.Security.RsaCrypt.Decrypt(chyperMessage, _priKey)));
    }

    [Test]
    public void TripeTest()
    {
        var cryptMessage = KS.Core.V1.Security.TripeCrypt.Encrypt(_message, _key);
        Assert.IsTrue(_message.Equals(KS.Core.V1.Security.TripeCrypt.Decrypt(cryptMessage,_message)));
    }

    [Test]
    public void HashTest()
    {
        Assert.IsTrue(_md5Hash.Equals(KS.Core.V1.Security.Hash.MD5(_message)));
    }
}