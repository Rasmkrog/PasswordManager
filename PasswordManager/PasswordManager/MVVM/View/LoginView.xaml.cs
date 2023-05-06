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
            // Initialisering af kode, cursor sat på textbox 'brugernavn'
        public LoginView()
        {
            InitializeComponent();
            brugernavn.Focus();
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
                    // Instans af 'SQL command', læser Username og Hashed_Password
                SqlCommand command = new SqlCommand(
                    "SELECT UserID, Username, Hashed_Password From [User] WHERE Username = @Username AND Hashed_Password = @Hashed_Password",
                    con);
                    // Sætter @Username's værdi til UserInfo.Username
                command.Parameters.AddWithValue("@Username", UserInfo.UserName);
                    // Sætter @Hashed_Password's værdi til Userinfo.Password
                command.Parameters.AddWithValue("@Hashed_Password", UserInfo.Password);

                    // SQL data reader, læser data asynkront fra resten af koden
                SqlDataReader reader = await command.ExecuteReaderAsync();

                    // Finder ud af om der er rækker som udfylder betingelserne
                if (reader.HasRows)
                {
                    // Sikre sig kun at læse en række, når der er rækker at læse
                    if (reader.Read())
                    {
                            // Læser rækkens UserID, laves til integer
                        UserInfo.UserID = reader.GetInt32(reader.GetOrdinal("UserID"));
                            // Funktion Kaldes, bruger flyttes til MainWindow
                        ToMainWindow();
                    }
                }
                else    // Hvis kode eller brugernavn er forkert
                {       // Fejl-besked om forkert login
                    MessageBox.Show("Username or password invalid or incorrect:" , "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        // Nulstil textbox 'kodeord's text
                    kodeord.Text = "";
                        // Nulstil værdien @Username
                    command.Parameters.AddWithValue("@Username", "");
                        // Nulstil værdien @Hashed_Password
                    command.Parameters.AddWithValue("@Hashed_Password", "");
                        // Flyt Cursor til kodeord-textbox
                    kodeord.Focus();
                }
                await reader.CloseAsync();
                
            }
            // Fejlbesked
            catch (Exception exception)
            {
                MessageBox.Show("Exception: " + exception.GetType().Name + "\nMessage: " + exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            //Lukning af instans-connection
            finally
            {
                con.Close();
            }
        }

            // Sender brugeren til Sign Up siden
        private void SignUpClick(object sender, RoutedEventArgs e)
        {
                // Instans af SignUpView
            SignUpView signUpView = new SignUpView();
                // Bruger føres til SignUpView
            signUpView.Show();
            this.Close();
        }

            // Sender brugeren til index
        private void ToMainWindow()
        {
                // Instans af MainWindow
            MainWindow MV = new MainWindow();
                // Bruger føres til MainWindow
            MV.Show();
            this.Close();
        }

            // Genindlæser siden, så alt bliver genskabt
        private void Refresh()
        {
                // Instans af LoginView
            LoginView LV = new LoginView();
                // Bruger føres til LoginView
            LV.Show();
            this.Close();
        }
    }
}
