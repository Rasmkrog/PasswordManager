using System;
using System.Security.Cryptography;
using System.Text;
namespace PasswordManager.Core;

public class Sha256script
{
    public string ShAencryption(string password)
    {
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashBytes = sha256.ComputeHash(passwordBytes);

            string hashString = BitConverter.ToString(hashBytes).Replace("-", "");

            Console.WriteLine("SHA-256 hash: " + hashString);
            return hashString;
        }
    }
}


