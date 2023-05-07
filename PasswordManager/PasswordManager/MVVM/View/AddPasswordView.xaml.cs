using PasswordManager.Core;
using PasswordManager.MVVM.Model;
using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace PasswordManager.MVVM.View;

public partial class AddPasswordView : UserControl
{
    public AddPasswordView()
    {
        InitializeComponent();
        
    }

    private string _title;
    private string _username;
    
    private string _hashedPassword;
    private string _websiteUrl;
    private string _notes;
    private int? _userId = UserInfo.UserID;
    static SaltGen saltGen = new SaltGen();
    private readonly string _Salt = saltGen.GenerateSalt();
    private void SavePassword(object sender, RoutedEventArgs routedEventArgs)
    {
        _title = TitleTextBox.Text;
        _username = UsernameTextBox.Text;
        _websiteUrl = WebsiteUrlTextBox.Text;
        _notes = NotesTextBox.Text;

        if (_title == "" || _title == " ")
        {
            BorderTitle.BorderBrush = Brushes.Red;
            BorderTitle.BorderThickness = new Thickness(1);
            BorderTitle.CornerRadius = new CornerRadius(10);
            MessageBox.Show("Du skal indtaste en titel");
            return;
        }
        BorderTitle.BorderBrush = Brushes.GhostWhite;
        BorderTitle.BorderThickness = new Thickness(1);
        BorderTitle.CornerRadius = new CornerRadius(10);
        
        if(_username =="" || _username == " ")
        {
            BorderUsername.BorderBrush = Brushes.Red;
            BorderUsername.BorderThickness = new Thickness(1);
            BorderUsername.CornerRadius = new CornerRadius(10);
            MessageBox.Show("Du skal indtaste et brugernavn");
            return;
        }
        BorderUsername.BorderBrush = Brushes.GhostWhite;
        BorderUsername.BorderThickness = new Thickness(1);
        BorderUsername.CornerRadius = new CornerRadius(10);
        



        AesEncryption aes = new AesEncryption();
        if (UserInfo.Password != null)
        {
           _hashedPassword =  aes.Encrypt(PasswordTextBox.Password, UserInfo.Password, _Salt);
        }


        string insertQuery = "INSERT INTO [Logins] (UserID, Title, Username, Email, Hashed_Password, URL, Notes, Date_Of_Creation, Salt) " +
                             "VALUES (@UserID, @Title, @Username, @Email, @HashedPassword, @URL, @Notes, GETDATE(), @Salt)";
        // Ny forbindelse til databasen skabes
        using (SqlConnection connection = new SqlConnection(DBConnection.ConnectionString))
        {
            // Forbindelsen åbnes
            connection.Open();
            // Ny SQL command skabes
            SqlCommand command = new SqlCommand(insertQuery, connection);
            
            // Parameter @Username's værdi sættes til usernameText - osv.
            if (_userId != null)
            {
                command.Parameters.AddWithValue("@UserID", _userId);
                command.Parameters.AddWithValue("@Title", _title);   
                command.Parameters.AddWithValue("@Username", _username);
                command.Parameters.AddWithValue("@Email", EmailTextBox.Text);
                command.Parameters.AddWithValue("@HashedPassword", _hashedPassword);
                command.Parameters.AddWithValue("@URL", _websiteUrl);
                command.Parameters.AddWithValue("@Notes", _notes);
                command.Parameters.AddWithValue("@Salt", _Salt);
                command.ExecuteNonQuery();
                
                // Besked om at bruger er oprettet
                MessageBox.Show("Password tilføjet!");
            }
            else
            {
                MessageBox.Show("Der skete en fejl, prøv igen");
            }
        }
    }
}