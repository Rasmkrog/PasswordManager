using System;
using System.Security.Cryptography;
using System.Text;
namespace PasswordManager.Core;

public class Sha256script
{
    public string ShAencryption(string password)
    {
        // Konverterer password-strengen til en byte-array ved hjælp af UTF8-kodning.
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

        // Instantiere et SHA256-objekt til at udføre hash-operationen.
        using (SHA256 sha256 = SHA256.Create())
        {
            // Genererer en hash-værdi fra byte-arrayet af password ved hjælp af SHA256-algoritmen.
            byte[] hashBytes = sha256.ComputeHash(passwordBytes);

            // Konverterer hash-byte-arrayet til en string med hexadecimal representation og fjerner bindestregerne mellem hvert hex-cifre.
            string hashString = BitConverter.ToString(hashBytes).Replace("-", "");
            
            // Returnerer den genererede hash-værdi.
            return hashString;
        }
    }
}


