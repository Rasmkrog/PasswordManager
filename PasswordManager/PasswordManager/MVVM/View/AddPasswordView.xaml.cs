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
    private void SavePassword(object sender, MouseButtonEventArgs e)
    {
        _title = TitleTextBox.Text;
        _username = UsernameTextBox.Text;
        _websiteUrl = WebsiteUrlTextBox.Text;
        _notes = NotesTextBox.Text;

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
            if (_userId != null && _title != null && _username != null)
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