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
        //oprettelse af variabler
        private const string LowercaseChars = "abcdefghijklmnopqrstuvwxyz";
        private const string UppercaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string DigitChars = "0123456789";
        private const string SpecialChars = "!@#$%^&*()_+-=[]{}|;':\",./<>?";

        public static string Generate(int length, bool includeLowercase, bool includeUppercase, bool includeDigits, bool includeSpecialChars)
        {
            
                //en tom variabel som bliver lavet 
            var allowedChars = "";

                // hvis IncludeLowercase bliver valgt så skal den med i allowedchars
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
            
            // Opretter en char array til at holde resultatet
            var result = new char[length];
            
            // Opret en kryptografisk tilfældighes generator. Så den kan vælge forskellige char
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] bytes = new byte[sizeof(uint)];
                
                //loop gennem den givende længde af kodeordet som er blevet indsat 
                for (int i = 0; i < length; i++)
                {
                    // Generer en tilfældig uint-værdi. Grunden til vi bruger ToUInt istedetfor ToInt er fordi at UInt sætter det til unsigned
                    rng.GetBytes(bytes);
                    uint value = BitConverter.ToUInt32(bytes, 0);
                   
                    //bestem af hvilke kaaraktere der skal skal bruges til og lave kode ved hjælp af den tilfældige uint-værdi ved de tilladte karakter 
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
        //henter brugers valg efter om de har checket af ved Checkbox 
        int length = int.Parse(LengthTextBox.Text);
        bool includeLowercase = LowercaseCheckBox.IsChecked.Value;
        bool includeUppercase = UppercaseCheckBox.IsChecked.Value;
        bool includeDigits = DigitsCheckBox.IsChecked.Value;
        bool includeSpecialChars = SpecialCharsCheckBox.IsChecked.Value;

        // Generer et kodeord baseret på brugerens valg
        var password = PasswordGenerator.Generate(length, includeLowercase, includeUppercase, includeDigits, includeSpecialChars);

        PasswordTextBox.Text = password;
    }
}