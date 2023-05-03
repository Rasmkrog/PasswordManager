using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace PasswordManager;

public partial class Kode_Generering : Page
{
    public Kode_Generering()
    {
        InitializeComponent();
    }

    private static bool addUpperCase;
        private static bool addNumbers;
        private static bool addSymbols;
        private static string validChars;

        
        static string generatePassword(int length)
        {
            // Check what checkboxes are ticked
            if (addUpperCase && addNumbers && addSymbols)
            {
                validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890?!@#$%^&*";
            }
            else if (addUpperCase && addNumbers)
            {
                validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            }
            else if (addUpperCase && addSymbols)
            {
                validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ?!@#$%^&*";
            }
            else if (addNumbers && addSymbols)
            {
                validChars = "abcdefghijklmnopqrstuvwxyz1234567890?!@#$%^&*";
            }
            else if (addUpperCase)
            {
                validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            }
            else if (addNumbers)
            {
                validChars = "abcdefghijklmnopqrstuvwxyz1234567890";
            }
            else if (addSymbols)
            {
                validChars = "abcdefghijklmnopqrstuvwxyz?!@#$%^&*";
            }
            else
            {
                validChars = "abcdefghijklmnopqrstuvwxyz";
            }


            return "sda";
        }

        void GetPass()
        {
            
        }
}