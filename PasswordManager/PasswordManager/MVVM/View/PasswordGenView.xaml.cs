using System;
using System.Security.AccessControl;
using System.Windows.Controls;
using System.Security.Cryptography;
using System.Linq;
using System.Windows;

namespace PasswordManager.MVVM.View;

public partial class PasswordGenView : UserControl
{
    public PasswordGenView()
    {
        InitializeComponent();
        
    }
   public static class PasswordGenerator
{
    // Definer karakterkonstanterne, der vil blive brugt til at generere kodeordet
    private const string LowercaseChars = "abcdefghijklmnopqrstuvwxyz";
    private const string UppercaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string DigitChars = "0123456789";
    private const string SpecialChars = "!@#$%^&*()_+-=[]{}|;':\",./<>?";

    // Funktion til at generere et kodeord
    public static string Generate(int length, bool includeLowercase, bool includeUppercase, bool includeDigits, bool includeSpecialChars)
    {
        // Kontrollér, at længden er større end eller lig med 1
        if (length < 1)
        {
            throw new ArgumentException("Length must be greater than or equal to 1");
        }

        // Opret en streng, der indeholder de karakterer, der er valgt af brugeren
        var allowedChars = "";

        if (includeLowercase)
        {
            allowedChars += LowercaseChars;
        }

        if (includeUppercase)
        {
            allowedChars += UppercaseChars;
        }

        if (includeDigits)
        {
            allowedChars += DigitChars;
        }

        if (includeSpecialChars)
        {
            allowedChars += SpecialChars;
        }

        // Kontrollér, at mindst ét karakter sæt er valgt
        if (allowedChars.Length == 0)
        {
            throw new ArgumentException("At least one character set must be selected");
        }

        // Opret en char array til at holde resultatet
        var result = new char[length];

        // Opret en kryptografisk tilfældighedsgenerator
        using (var rng = new RNGCryptoServiceProvider())
        {
            byte[] bytes = new byte[sizeof(uint)];

            // Loop gennem længden af det ønskede kodeord
            for (int i = 0; i < length; i++)
            {
                // Generer en tilfældig uint-værdi
                rng.GetBytes(bytes);
                uint value = BitConverter.ToUInt32(bytes, 0);

                // Bestem, hvilken karakter der skal bruges baseret på den tilfældige uint-værdi og de tilladte karakterer
                int index = (int)(value % (uint)allowedChars.Length);
                result[i] = allowedChars[index];
            }
        }

        // Konverter resultatet til en streng og returner det
        return new string(result);
    }
}
        
   private void GeneratePasswordButton_Click(object sender, RoutedEventArgs e)
   {
       // Hent brugerens valg
       int length = int.Parse(LengthTextBox.Text);
       bool includeLowercase = LowercaseCheckBox.IsChecked.Value;
       bool includeUppercase = UppercaseCheckBox.IsChecked.Value;
       bool includeDigits = DigitsCheckBox.IsChecked.Value;
       bool includeSpecialChars = SpecialCharsCheckBox.IsChecked.Value;

       // Generer et kodeord baseret på brugerens valg
   }
}