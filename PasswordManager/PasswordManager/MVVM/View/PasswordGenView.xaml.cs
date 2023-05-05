using System;
using System.Windows.Controls;

namespace PasswordManager.MVVM.View;

public partial class PasswordGenView : UserControl
{
    public PasswordGenView()
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

        public void GetPass()
        {
            Random res = new Random();
                //denne skal laves om så den kan bruge det over
               String add = "abcdefghijklmnopqrstuvwxyz1234567890?!@#$%^&*";

               //Den string den der viser kode når det er sat ind 
            String ran = "";
            
            int size = int.Parse(PasswordLenght.Text);

            for (int i = 0; i < size; i++)
            {
                //så kan den vælge all char 
                int x = res.Next(26);

                ran = ran + add[x];
            }



        }
    
}