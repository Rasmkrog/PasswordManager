using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using PasswordManager.Core;
using static PasswordManager.Core.DBConnection;

namespace PasswordManager.MVVM.View
{
    public partial class SignUpView : UserControl
    {
        public SignUpView()
        {
            InitializeComponent();
        }

        private int count = 0;
        private bool usernameExists = false;
        private bool emailExists = false;
        private void OpretBruger_Click(object sender, RoutedEventArgs e)
        {
            count = 0;
            
            try
            {
                string usernameText = Brugernavn.Text;
                string emailText = Email.Text;
                string passwordText = Kodeord.Text;

                // Check if the username or email already exist in the database
                bool usernameExists = false;
                bool emailExists = false;
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();

                    // Tjekker om brugernavnet er i databasen
                    using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM [User] WHERE Username = @Username", con))
                    {
                        command.Parameters.AddWithValue("@Username", usernameText);
                        int count = (int)command.ExecuteScalar();
                        usernameExists = (count > 0);
                    }

                    // Tjekker om mailen er i databasen
                    using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM [User] WHERE Email = @Email", con))
                    {
                        command.Parameters.AddWithValue("@Email", emailText);
                        int count = (int)command.ExecuteScalar();
                        emailExists = (count > 0);
                    }
                }

                // Error message hvis brugernavn eller mail er i databasen
                if (usernameExists)
                {
                    MessageBox.Show("Username already exists. Please choose a different username.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else if (emailExists)
                {
                    MessageBox.Show("Email already exists. Please use a different email address.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Insert the new user data into the database
                string insertQuery = "INSERT INTO [User] (Username, Email, Hashed_Password, DateOfOprettelse) VALUES (@Username, @Email, @HashedPassword, GETDATE())";

                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand(insertQuery, con))
                    {
                        command.Parameters.AddWithValue("@Username", usernameText);
                        command.Parameters.AddWithValue("@Email", emailText);
                        command.Parameters.AddWithValue("@HashedPassword", passwordText);
                        command.ExecuteNonQuery();
                    }

                    HomeView homeView = new HomeView();
                    SignUp_View.Navigate(homeView);
                }
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
    }
}

/*using System;
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
            string usernameText = Brugernavn.Text;
            string emailText = Email.Text;
            string passwordText = Kodeord.Text;

            string insertQuery = "INSERT INTO [User] (Username, Email, Hashed_Password, DateOfOprettelse) VALUES (@Username, @Email, @HashedPassword, GETDATE())";

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                using (SqlCommand command = new SqlCommand(insertQuery, con))
                {
                    command.Parameters.AddWithValue("@Username", usernameText);
                    command.Parameters.AddWithValue("@Email", emailText);
                    command.Parameters.AddWithValue("@HashedPassword", passwordText);
                    command.ExecuteNonQuery();
                }

                HomeView homeView = new HomeView();
                SignUp_View.Navigate(homeView);
            }
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

}*/