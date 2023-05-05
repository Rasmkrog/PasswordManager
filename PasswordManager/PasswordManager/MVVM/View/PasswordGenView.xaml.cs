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
        private const string LowercaseChars = "abcdefghijklmnopqrstuvwxyz";
        private const string UppercaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string DigitChars = "0123456789";
        private const string SpecialChars = "!@#$%^&*()_+-=[]{}|;':\",./<>?";

        public static string Generate(int length, bool includeLowercase, bool includeUppercase, bool includeDigits, bool includeSpecialChars)
        {
            if (length < 1)
            {
                throw new ArgumentException("Length must be greater than or equal to 1");
            }

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

            if (allowedChars.Length == 0)
            {
                throw new ArgumentException("At least one character set must be selected");
            }

            var result = new char[length];

            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] bytes = new byte[sizeof(uint)];

                for (int i = 0; i < length; i++)
                {
                    rng.GetBytes(bytes);
                    uint value = BitConverter.ToUInt32(bytes, 0);
                    int index = (int)(value % (uint)allowedChars.Length);
                    result[i] = allowedChars[index];
                }
            }

            return new string(result);
        }
    }
        
    private void GeneratePasswordButton_Click(object sender, RoutedEventArgs e)
    {
        int length = int.Parse(LengthTextBox.Text);
        bool includeLowercase = LowercaseCheckBox.IsChecked.Value;
        bool includeUppercase = UppercaseCheckBox.IsChecked.Value;
        bool includeDigits = DigitsCheckBox.IsChecked.Value;
        bool includeSpecialChars = SpecialCharsCheckBox.IsChecked.Value;

        var password = PasswordGenerator.Generate(length, includeLowercase, includeUppercase, includeDigits, includeSpecialChars);

        PasswordTextBox.Text = password;
    }
}