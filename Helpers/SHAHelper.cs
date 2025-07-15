using System.Security.Cryptography;
using System.Text;

public static class SHAHelper
{
    public static string ComputeSHA256Hash(string raw)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(raw));
            StringBuilder sb = new StringBuilder();
            foreach (var b in bytes)
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }
    }
}
