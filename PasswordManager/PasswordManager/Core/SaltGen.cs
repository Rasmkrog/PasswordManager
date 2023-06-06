using System;
namespace PasswordManager.MVVM.Model;

public class SaltGen
{
    public string GenerateSalt()
    {
        //generate a random string with a specified size of 16
        var random = new Random();
        
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+=-";
        //generate a random string with a specified size of 16
        var salt = new char[16];
        for (var i = 0; i < salt.Length; i++)
        {
            salt[i] = chars[random.Next(chars.Length)];
        }
        return new(salt);
    }
}