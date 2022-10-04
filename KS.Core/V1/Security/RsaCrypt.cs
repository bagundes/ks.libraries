namespace KS.Core.V1.Security;
using System.Security.Cryptography;

/// <summary>
/// Performs asymmetric encryption and decryption using the implementation of the RSA algorithm provided by the cryptographic service provider (CSP). This class cannot be inherited.
/// </summary>
public static class RsaCrypt
{
    /// <summary>
    /// The size of the key to use in bits.
    /// </summary>
    private const int KEY_SIZE = 2048;
    
    /// <summary>
    /// Performs asymmetric encryption and decryption using the implementation of the RSA algorithm provided by the cryptographic service provider (CSP). This class cannot be inherited.
    /// </summary>
    private static RSACryptoServiceProvider? _csp;
    
    /// <summary>
    /// Public key
    /// </summary>
    public static string? PubKey => GetKey(false);
    public static string? PriKey => GetKey(true);
    
    private static RSACryptoServiceProvider? RSAProvider()
    {
        return new RSACryptoServiceProvider(KEY_SIZE);
    }
    
    private static string? GetKey(bool privateKey)
    {
        if (_csp is null) _csp = RSAProvider();
            
        var key = _csp.ExportParameters(privateKey);
            
        //we need some buffer
        var sw = new System.IO.StringWriter();
        //we need a serializer
        var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
        //serialize the key into the stream
        xs.Serialize(sw, key);
        //get the string from the stream
        return sw.ToString();
    }

    public static string Decrypt(string message, string? privateKey)
    {
        //first, get our bytes back from the base64 string ...
        var bytesChyperMessage = Convert.FromBase64String(message);
        //get a stream from the string
        var sr = new System.IO.StringReader(privateKey);
        //we need a deserializer
        var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
        //get the object back from the stream
        var priKey = (RSAParameters)xs.Deserialize(sr);
        //we have a public key ... let's get a new csp and load that key
        var csp = new RSACryptoServiceProvider();
        csp.ImportParameters(priKey);
        //decrypt and strip pkcs#1.5 padding
        var bytesMessage = csp.Decrypt(bytesChyperMessage, false);
        //get our original plainText back...
        return System.Text.Encoding.Unicode.GetString(bytesMessage);
    }

    public static string Encrypt(string message, string? publicKey)
    {
        //get a stream from the string
        var sr = new System.IO.StringReader(publicKey);
        //we need a deserializer
        var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
        //get the object back from the stream
        var pubKey = (RSAParameters)xs.Deserialize(sr);
        //we have a public key ... let's get a new csp and load that key
        var csp = new RSACryptoServiceProvider();
        csp.ImportParameters(pubKey);
        //for encryption, always handle bytes...
        var bytesPlainTextData = System.Text.Encoding.Unicode.GetBytes(message);
        //apply pkcs#1.5 padding and encrypt our data 
        var bytesCypherText = csp.Encrypt(bytesPlainTextData, false);
        //we might want a string representation of our cypher text... base64 will do
        return Convert.ToBase64String(bytesCypherText);
    }
}