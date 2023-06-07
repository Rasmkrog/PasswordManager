using System;
using System.Security.Cryptography;
using System.Text;
namespace PasswordManager.MVVM.Model;

public class AesEncryption
  {
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
    
   
    public static string Decrypt(string encryptedText, string? password, string salt)
    {
      // Convert the encrypted text from Base64 string to bytes
      byte[] bytes = Convert.FromBase64String(encryptedText);
      
      // Convert the password and salt strings to bytes

      byte[] passwordBytes = Encoding.Unicode.GetBytes(password);
      byte[] saltBytes = Encoding.Unicode.GetBytes(salt);
      
      // Variable to hold the decrypted bytes
      byte[]? decryptedBytes = null;

      // Create an instance of AesCryptoServiceProvider
      using (var aes = new AesCryptoServiceProvider())
      {
        // Derive the key and initialization vector (IV) using the password and salt
        var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
        aes.Key = key.GetBytes(aes.KeySize / 8);
        aes.IV = key.GetBytes(aes.BlockSize / 8);
        aes.Mode = CipherMode.CBC;
        
        // Create a decryptor using the derived key and IV
        using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
        {
          // Create a memory stream to hold the encrypted bytes
          using (var memoryStream = new System.IO.MemoryStream(bytes))
          {
            // Create a CryptoStream to perform the decryption
            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
            {
              // Create an array to store the decrypted bytes
              decryptedBytes = new byte[bytes.Length];

              // Read the decrypted bytes from the CryptoStream
              cryptoStream.Read(decryptedBytes, 0, decryptedBytes.Length);
            }
          }
        }
      }
      // Convert the decrypted bytes to a string using Unicode encoding
      return Encoding.Unicode.GetString(decryptedBytes);
    }
  }