using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ProtechGroup.FlightBookingWeb.Helpers
{
    public static class UrlEncryptHelper
    {
        private static readonly string EncryptionKey = "Huy@nMy$0702%T*mAnh1106"; 

        public static string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText)) return string.Empty;

            using (var aes = Aes.Create())
            {
                var key = new Rfc2898DeriveBytes(EncryptionKey, Encoding.UTF8.GetBytes("SaltValue123"));
                aes.Key = key.GetBytes(32);
                aes.IV = key.GetBytes(16);

                using (var ms = new MemoryStream())
                using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    var bytes = Encoding.UTF8.GetBytes(plainText);
                    cs.Write(bytes, 0, bytes.Length);
                    cs.Close();
                    return HttpServerUtility.UrlTokenEncode(ms.ToArray());
                }
            }
        }

        public static string Decrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText)) return string.Empty;

            using (var aes = Aes.Create())
            {
                var key = new Rfc2898DeriveBytes(EncryptionKey, Encoding.UTF8.GetBytes("SaltValue123"));
                aes.Key = key.GetBytes(32);
                aes.IV = key.GetBytes(16);

                var bytes = HttpServerUtility.UrlTokenDecode(cipherText);

                using (var ms = new MemoryStream())
                using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(bytes, 0, bytes.Length);
                    cs.Close();
                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
        }
    }
}