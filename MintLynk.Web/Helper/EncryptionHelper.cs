using System.Security.Cryptography;
using System.Text;

namespace MintLynk.Web.Helper
{
    public static class EncryptionHelper
    {
        public static string Encrypt(string plainText, string key, string iv)
        {           
            using (Aes aes = Aes.Create())
            {
                aes.Key = Convert.FromBase64String(key);
                aes.IV = Convert.FromBase64String(iv);
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using MemoryStream memoryStream = new MemoryStream();
                using CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
                using (StreamWriter writer = new StreamWriter(cryptoStream))
                {
                    writer.Write(plainText);
                }

                return Convert.ToBase64String(memoryStream.ToArray());
            }
        }

        public static string Decrypt(string encryptedText, string key, string iv)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Convert.FromBase64String(key);
                aes.IV = Convert.FromBase64String(iv);
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                byte[] buffer = Convert.FromBase64String(encryptedText);

                using MemoryStream memoryStream = new MemoryStream(buffer);
                using CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Read);
                using StreamReader reader = new StreamReader(cryptoStream);
                return reader.ReadToEnd();
            }
        }

        public static string KeyGenerator()
        {
            var key = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
            var iv = Convert.ToBase64String(RandomNumberGenerator.GetBytes(16));
            return string.Empty;
        }
    }
}
