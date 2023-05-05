using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using PasswordManager.Core;
using static PasswordManager.Core.DBConnection;

namespace PasswordManager.MVVM.View;

public partial class SignUpView : UserControl
{
    public SignUpView()
    {
        InitializeComponent();
    }
    private void OpretBruger_Click(object sender, EventArgs e)
    {
        try
        {
            // Henter text fra TextBoxes
            string username = brugernavn.Text;
            string email = Email.Text;
            string hashedPassword = Kodeord.Text; // Det er uklart, hvor UserInfo.Password kommer fra.

            string query = "INSERT INTO [User] (Username, Email, Hashed_Password, DateOfOprettelse) VALUES (@Username, @Email, @HashedPassword, GETDATE())";
            
            // Skaber forbindelse til SQL database gennem "ConnectionString"
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                // Indsætter brugernavn, e-mail og krypteret kodeord i database
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@HashedPassword", hashedPassword);
                    command.ExecuteNonQuery();
                }
                HomeView homeView = new HomeView();
                SignUp_View.Navigate(homeView);
            }
            //Trouble shoot
        }
        catch (SqlException ex)
        {
            MessageBox.Show(ex.Message, "SQL Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }


    private string brugernavnText;
    private string kodeordText;

    
    public void Sign_Up()
    {
        brugernavnText = brugernavn.Text;
        kodeordText = Kodeord.Text;
    }

}