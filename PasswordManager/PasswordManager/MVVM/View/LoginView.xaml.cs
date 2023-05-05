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

        private int count = 0;
        private bool usernameExists = false;
        private bool LoggedIn = false;

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            count = 0;
            LoggedIn = false;
            usernameExists = false;

            SqlConnection con = new SqlConnection(ConnectionString);
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand(
                    "SELECT COUNT(*) FROM [User] WHERE Username = @Username AND Hashed_Password = @Hashed_Password",
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
                }
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
                
            try
            {
                string usernameText = brugernavn.Text;
                string passwordText = kodeord.Text;

                
                // Check if the username or email already exist in the database
                /*using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    // Tjekker om kodeord passer med brugernavn

                    SqlCommand command2 =
                        new SqlCommand(
                            "SELECT COUNT(*) FROM [User] WHERE Username = @Username AND Hashed_Password = @Hashed_Password",
                            con);
                    
                        //command.Parameters.AddWithValue("@UserID", UserInfo.UserID);
                            SqlDataReader reader = command2.ExecuteReaderAsync();
                            UserInfo.UserID =
                                LoggedIn = true;

                }*/
                
                // Sender brugeren til HomeView hvis LoggedIn == true
                if (LoggedIn)
                {
                    ToHomeView();
                }
                // Fejlbesked hvis kode eller brugernavn er forkert
                else if (LoggedIn == false)
                {
                    MessageBox.Show("Forkerte login informationer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    kodeord.Text = "";
                    brugernavn.Text = "";
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
