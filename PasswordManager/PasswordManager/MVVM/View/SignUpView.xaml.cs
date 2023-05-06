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
    public partial class SignUpView : Window
    {
        // Initialisering af side
        public SignUpView()
        {
            InitializeComponent();
        }

        // Oprettelse af variabler
        private int count = 0;
        private bool usernameExists = false;
        private bool emailExists = false;

        // Kaldes når "Opret Bruger" knappen trykkes
        private void OpretBruger_Click(object sender, RoutedEventArgs e)
        {
            // Default værdier til variabler
            count = 0;
            emailExists = false;
            usernameExists = false;

            try
            {
                // Indsætter text fra textboxe i variabler
                string usernameText = Brugernavn.Text;
                string emailText = Email.Text;
                string passwordText = Kodeord.Text;

                // Tjekker om brugernavn eller mail allerede er i databasen
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();

                    // Tjekker om brugernavnet er i databasen
                    using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM [User] WHERE Username = @Username", con))
                    {
                        command.Parameters.AddWithValue("@Username", usernameText);
                        count = (int)command.ExecuteScalar();
                        usernameExists = (count > 0);
                    }

                    // Tjekker om mailen er i databasen
                    using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM [User] WHERE Email = @Email", con))
                    {
                        command.Parameters.AddWithValue("@Email", emailText);
                        count = (int)command.ExecuteScalar();
                        emailExists = (count > 0);
                    }
                }

                // Error message hvis brugernavn eller mail er i databasen
                if (usernameExists)
                {
                    MessageBox.Show("Username already exists. Please choose a different username.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Refresh();
                }
                else if (emailExists)
                {
                    MessageBox.Show("Email already exists. Please use a different email address.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Refresh();
                }

                // Indsætter ny brugers data i databasen
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
                    ToHomeView();
                }
            }
            // Fejl-beskeder
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "SQL Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // genindlæser siden, så alt bliver genskabt
        private void Refresh()
        {
            SignUpView signUpView = new SignUpView();
            this.Content = signUpView;
        }
        
        // Sender brugeren til index
        private void ToHomeView()
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }
        
        private void LoginClick(object sender, RoutedEventArgs e)
        {
            LoginView LW = new LoginView();
            LW.Show();
            this.Close();
        }
    }
}
