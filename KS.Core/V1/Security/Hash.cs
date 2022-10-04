using System.Security.Cryptography;
using System.Text;

namespace KS.Core.V1.Security;

public static class Hash
{
    public static string MD5(string text)
    {
        using MD5 md5 = new MD5CryptoServiceProvider();
        var retVal = md5.ComputeHash(Encoding.UTF8.GetBytes(text));

        var sb = new StringBuilder();
        for (var i = 0; i < retVal.Length; i++) sb.Append(retVal[i].ToString("x2"));
        return sb.ToString();
    }

    public static string MD5(FileInfo fileInfo)
    {
        using var file = new FileStream(fileInfo.FullName, FileMode.Open);
        using MD5 md5 = new MD5CryptoServiceProvider();
        var retVal = md5.ComputeHash(file);
        file.Close();

        var sb = new StringBuilder();
        for (var i = 0; i < retVal.Length; i++) sb.Append(retVal[i].ToString("x2"));
        return sb.ToString();
    }
}