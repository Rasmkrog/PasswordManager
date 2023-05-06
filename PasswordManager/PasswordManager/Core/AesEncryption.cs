using System;
using System.Security.Cryptography;
using System.Text;
namespace PasswordManager.MVVM.Model;

public class AesEncryption
  {
    [Obsolete("Obsolete")]
    public string Encrypt(string text, string password, string salt)
    {
      byte[] bytes = Encoding.Unicode.GetBytes(text);
      byte[] passwordBytes = Encoding.Unicode.GetBytes(password);
      byte[] saltBytes = Encoding.Unicode.GetBytes(salt);
      byte[] encryptedBytes;
      using (var aes = new AesCryptoServiceProvider())
      {
        var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
        aes.Key = key.GetBytes(aes.KeySize / 8);
        aes.IV = key.GetBytes(aes.BlockSize / 8);
        aes.Mode = CipherMode.CBC;
        using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
        {
          using (var memoryStream = new System.IO.MemoryStream())
          {
            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
            {
              cryptoStream.Write(bytes, 0, bytes.Length);
              cryptoStream.Close();
            }
            encryptedBytes = memoryStream.ToArray();
          }
        }
      }
      return Convert.ToBase64String(encryptedBytes);
    }
    
    [Obsolete("Obsolete")]
    public static string Decrypt(string encryptedText, string? password, string salt)
    {
      byte[] bytes = Convert.FromBase64String(encryptedText);
      byte[] passwordBytes = Encoding.Unicode.GetBytes(password);
      byte[] saltBytes = Encoding.Unicode.GetBytes(salt);
      byte[]? decryptedBytes = null;
      using (var aes = new AesCryptoServiceProvider())
      {
        var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
        aes.Key = key.GetBytes(aes.KeySize / 8);
        aes.IV = key.GetBytes(aes.BlockSize / 8);
        aes.Mode = CipherMode.CBC;
        using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
        {
          using (var memoryStream = new System.IO.MemoryStream(bytes))
          {
            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
            {
              decryptedBytes = new byte[bytes.Length];
              cryptoStream.Read(decryptedBytes, 0, decryptedBytes.Length);
            }
          }
        }
      }
      return Encoding.Unicode.GetString(decryptedBytes);
    }
  }