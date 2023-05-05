using PasswordManager.Core;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Media;
using static PasswordManager.Core.DBConnection;
using PasswordManager.MVVM.Model;
using System.Windows.Markup;
using static PasswordManager.MVVM.Model.LoadDataClass;


namespace PasswordManager.MVVM.View;


public partial class HomeView : UserControl
{
    [Obsolete("Obsolete")]
    public HomeView()
    {
        
        InitializeComponent();
        
        //initialize the grid
        LoadData(LoginGrid, NumberOfLogins, loginstext);
        
    }

    private const string SqlQuery = "SELECT Title, Username, Hashed_Password, Salt, Email, URL, Notes, Date_Of_Creation From Logins WHERE UserID = @UserID";
    private static int rows = 0;
    
    [Obsolete("Obsolete")]
    public async static void LoadData(Grid LoginGrid, TextBlock NumberOfLogins, TextBlock loginstext)
    {
        SqlConnection connection = new SqlConnection(ConnectionString);
        try
        {
            connection.Open();
            
            //Select all logins from the database containing Title, Username of login, Password, Email, and Website url if present. Also check for userid match with class Userinfo.cs
            SqlCommand command = new SqlCommand(SqlQuery, connection);
            
            //test if UserID is not null
            if (UserInfo.UserID == null)
            {
                MessageBox.Show("UserID is null", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                //ReSharper disable once HeapView.BoxingAllocation
                //Parameterize the UserID
                command.Parameters.AddWithValue("@UserID", UserInfo.UserID);
                
            }
            
            //Execute the command
            SqlDataReader reader = await command.ExecuteReaderAsync();
            //get number of rows
            
            
            if (reader.HasRows)
            {
                //create headers for each column
                TextBlock titleHeader = new TextBlock();
                TextBlock usernameHeader = new TextBlock();
                TextBlock passwordHeader = new TextBlock();
                TextBlock emailHeader = new TextBlock();
                TextBlock dateHeader = new TextBlock();
                
                //Set the text of each header
                titleHeader.Text = "Title";
                usernameHeader.Text = "Username";
                passwordHeader.Text = "Password";
                emailHeader.Text = "Email";
                dateHeader.Text = "Date Created";
                
                //Set the font size of each header
                titleHeader.FontSize = 16;
                usernameHeader.FontSize = 16;
                passwordHeader.FontSize = 16;
                emailHeader.FontSize = 16;
                dateHeader.FontSize = 16;
                
                titleHeader.HorizontalAlignment = HorizontalAlignment.Center;
                usernameHeader.HorizontalAlignment = HorizontalAlignment.Center;
                passwordHeader.HorizontalAlignment = HorizontalAlignment.Center;
                emailHeader.HorizontalAlignment = HorizontalAlignment.Center;
                dateHeader.HorizontalAlignment = HorizontalAlignment.Center;
                

                //Set the font color of each header
                titleHeader.Foreground = Brushes.White;
                usernameHeader.Foreground = Brushes.White;
                passwordHeader.Foreground = Brushes.White;
                emailHeader.Foreground = Brushes.White;
                dateHeader.Foreground = Brushes.White;
                
                titleHeader.FontWeight = FontWeights.Bold;
                usernameHeader.FontWeight = FontWeights.Bold;
                passwordHeader.FontWeight = FontWeights.Bold;
                emailHeader.FontWeight = FontWeights.Bold;
                dateHeader.FontWeight = FontWeights.Bold;
                
                
                //Set the margin of each header
                titleHeader.Margin = new Thickness(20,0,20,0);
                usernameHeader.Margin = new Thickness(20,0,20,0);
                passwordHeader.Margin = new Thickness(10, 0, 10, 0);
                emailHeader.Margin = new Thickness(10, 0, 10, 0);
                dateHeader.Margin = new Thickness(10, 0, 10, 0);

                titleHeader.Padding = new Thickness(0, 0, 0, 5);
                usernameHeader.Padding = new Thickness(0, 0, 0, 5);
                passwordHeader.Padding = new Thickness(0, 0, 0, 5);
                emailHeader.Padding = new Thickness(0, 0, 0, 5);
                dateHeader.Padding = new Thickness(0, 0, 0, 5);
                
                
                //Add the headers to the grid
                Grid.SetRow(titleHeader, 0);
                Grid.SetColumn(titleHeader, 0);
                LoginGrid.Children.Add(titleHeader);
                
                Grid.SetRow(usernameHeader, 0);
                Grid.SetColumn(usernameHeader, 1);
                LoginGrid.Children.Add(usernameHeader);
                
                Grid.SetRow(passwordHeader, 0);
                Grid.SetColumn(passwordHeader, 2);
                LoginGrid.Children.Add(passwordHeader);
                
                Grid.SetRow(emailHeader, 0);
                Grid.SetColumn(emailHeader, 3);
                LoginGrid.Children.Add(emailHeader);
                
                
                Grid.SetRow(dateHeader, 0);
                Grid.SetColumn(dateHeader, 5);
                LoginGrid.Children.Add(dateHeader);
                


                while (reader.Read())
                {
                    rows++;
                    //Create a new row for each login
                    RowDefinition row = new RowDefinition();
                    row.Height = new GridLength(50);
                    
                    //Add the row to the grid
                    LoginGrid.RowDefinitions.Add(row);
                    
                    //Create a new TextBlock for each column
                    TextBlock title = new TextBlock();
                    TextBlock username = new TextBlock();
                    TextBlock password = new TextBlock();
                    TextBlock email = new TextBlock();
                    TextBlock date = new TextBlock();
                    
                    //Set the text of each TextBlock to the corresponding column in the database
                    title.Text = reader.GetString("Title");
                    username.Text = reader.GetString("Username");
                    
                    if (password.Text != null)
                    {
                        password.Text = AesEncryption.Decrypt(reader.GetString("Hashed_Password"), UserInfo.Password,
                            reader.GetString("Salt"));
                    }
                    else
                    {
                        password.Text = "No password found";
                    }
                    
                    email.Text = email.Text != null ? reader.GetString("Email") : "No email found";
                    date.Text = reader.GetDateTime("Date_Of_Creation").ToShortDateString().ToString(CultureInfo.CurrentCulture);
                    
                    //Set the font size of each TextBlock
                    title.FontSize = 14;
                    username.FontSize = 14;
                    password.FontSize = 14;
                    email.FontSize = 14;
                    date.FontSize = 14;
                    
                    
                    //Set the font color of each TextBlock
                    title.Foreground = Brushes.White;
                    username.Foreground = Brushes.White;
                    password.Foreground = Brushes.White;
                    email.Foreground = Brushes.White;
                    date.Foreground = Brushes.White;
                    
                    //Set the margin of each TextBlock

                    title.Margin = new Thickness(10,0,10,0);
                    username.Margin = new Thickness(10,0,10,0);
                    password.Margin = new Thickness(10, 0, 10, 0);
                    email.Margin = new Thickness(10, 0, 10, 0);
                    date.Margin = new Thickness(10, 0, 10, 0);
                    
                    title.HorizontalAlignment = HorizontalAlignment.Center;
                    username.HorizontalAlignment = HorizontalAlignment.Center;
                    password.HorizontalAlignment = HorizontalAlignment.Center;
                    email.HorizontalAlignment = HorizontalAlignment.Center;
                    date.HorizontalAlignment = HorizontalAlignment.Center;
                    
                    
                    
                    
                    //Add the TextBlocks to the grid
                    Grid.SetRow(title, LoginGrid.RowDefinitions.Count - 1);
                    Grid.SetColumn(title, 0);
                    LoginGrid.Children.Add(title);

                    Grid.SetRow(username, LoginGrid.RowDefinitions.Count - 1);
                    Grid.SetColumn(username, 1);
                    LoginGrid.Children.Add(username);

                    Grid.SetRow(password, LoginGrid.RowDefinitions.Count - 1);
                    Grid.SetColumn(password, 2);
                    LoginGrid.Children.Add(password);

                    Grid.SetRow(email, LoginGrid.RowDefinitions.Count - 1);
                    Grid.SetColumn(email, 3);
                    LoginGrid.Children.Add(email);
                    
                    Grid.SetRow(date, LoginGrid.RowDefinitions.Count - 1);
                    Grid.SetColumn(date, 5);
                    LoginGrid.Children.Add(date);
                }
                if (rows-1 == 1)
                {
                    NumberOfLogins.Text = $"{rows + 1}"; 
                    loginstext.Text = "login";
                }
                else
                {
                    NumberOfLogins.Text = $"{rows--}";
                    loginstext.Text = "logins";
                }
                
                
            }
            else
            {
                MessageBox.Show("No rows found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            await reader.CloseAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Connection failed: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            connection.Close();
        }
    }
    
    private void TestConnection_Click(object sender, RoutedEventArgs e)
    {
        SqlConnection connection = new SqlConnection(ConnectionString);
        try
        {
            connection.Open();
            MessageBox.Show("Connection successful", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Connection failed: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            connection.Close();
        }
    }
}