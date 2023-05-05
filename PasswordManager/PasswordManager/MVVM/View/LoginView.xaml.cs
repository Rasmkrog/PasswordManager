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
        public LoginView()
        {
            InitializeComponent();
        }
        
        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            UserInfo.UserName = brugernavn.Text;
            UserInfo.Password = kodeord.Text;

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
                    UserInfo.UserID = int.Parse(reader.GetString("UserID"));
                }
                else
                {
                    MessageBox.Show("Username or password invalid or incorrect:" , "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Refresh();
                }
                await reader.CloseAsync();
                
            }
            catch (Exception exception)
            {
                MessageBox.Show("Connection failed: " + exception.Message, "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            finally
            {
                con.Close();
            }
        }
        
        
        // Sender brugeren til index
        private void ToHomeView()
        {
            MainWindow MV = new MainWindow();
            MV.Show();
            this.Close();
        }

        // genindlæser siden, så alt bliver genskabt
        private void Refresh()
        {
            SignUpView signUpView = new SignUpView();
            //signUpView.Show();
            this.Close();
        }
    }
}
