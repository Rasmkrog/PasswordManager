using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Windows;
using System.Windows.Controls;
using PasswordManager.Core;
using static PasswordManager.Core.DBConnection;

namespace PasswordManager.MVVM.View
{
    public partial class LoginView : Window
    {
        // Initialisering af kode
        public LoginView()
        {
            InitializeComponent();
        }
        
        // Bliver kaldt hvis Login-knappen trykkes
        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            // Indsætter text fra textboxe i variabler i UserInfo
            UserInfo.UserName = brugernavn.Text;
            UserInfo.Password = kodeord.Text;

            // Instans af forbindelse til databasen
            SqlConnection con = new SqlConnection(ConnectionString);
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand(
                    "SELECT UserID, Username, Hashed_Password From [User] WHERE Username = @Username AND Hashed_Password = @Hashed_Password",
                    con);
                command.Parameters.AddWithValue("@Username", UserInfo.UserName);
                command.Parameters.AddWithValue("@Hashed_Password", UserInfo.Password);

                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        UserInfo.UserID = reader.GetInt32(reader.GetOrdinal("UserID"));
                        ToMainWindow();
                    }
                }
                else
                {
                    MessageBox.Show("Username or password invalid or incorrect:" , "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Refresh();
                }
                await reader.CloseAsync();
                
            }   // Fejlbesked
            catch (Exception exception)
            {
                MessageBox.Show("Exception: " + exception.GetType().Name + "\nMessage: " + exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            finally
            {
                con.Close();
            }
        }

        // Sender brugeren til Sign Up siden
        private void SignUpClick(object sender, RoutedEventArgs e)
        {
            SignUpView signUpView = new SignUpView();
            signUpView.Show();
            this.Close();
        }

        // Sender brugeren til index
        private void ToMainWindow()
        {
            MainWindow MV = new MainWindow();
            MV.Show();
            this.Close();
        }

        // Genindlæser siden, så alt bliver genskabt
        private void Refresh()
        {
            LoginView LV = new LoginView();
            LV.Show();
            this.Close();
        }
    }
}
