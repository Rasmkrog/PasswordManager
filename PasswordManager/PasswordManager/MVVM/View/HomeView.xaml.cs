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
    public HomeView()
    {
        
        InitializeComponent();
        
        //initialize the grid
        LoadData(LoginGrid);
    }

    private const string SqlQuery = "SELECT Title, Username, Hashed_Password, Salt, Email, URL, Notes, Date_Of_Creation From Logins WHERE UserID = @UserID";
    
    public async static void LoadData(Grid LoginGrid)
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
            if (reader.HasRows)
            {
                while (reader.Read())
                {
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
                    TextBlock url = new TextBlock();
                    TextBlock notes = new TextBlock();
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
                    url.Text = url.Text != null ? reader.GetString("URL") : "No url found";
                    notes.Text = notes.Text != null ? reader.GetString("Notes") : "";
                    date.Text = reader.GetDateTime("Date_Of_Creation").ToString(CultureInfo.CurrentCulture);
                    
                    //Set the font size of each TextBlock
                    title.FontSize = 14;
                    username.FontSize = 14;
                    password.FontSize = 14;
                    email.FontSize = 14;
                    url.FontSize = 14;
                    notes.FontSize = 14;
                    date.FontSize = 14;
                    
                    //Set the font color of each TextBlock
                    title.Foreground = Brushes.White;
                    username.Foreground = Brushes.White;
                    password.Foreground = Brushes.White;
                    email.Foreground = Brushes.White;
                    url.Foreground = Brushes.White;
                    notes.Foreground = Brushes.White;
                    date.Foreground = Brushes.White;
                    
                    //Set the margin of each TextBlock

                    title.Margin = new Thickness(10,0,10,0);
                    username.Margin = new Thickness(10,0,10,0);
                    password.Margin = new Thickness(10, 0, 10, 0);
                    email.Margin = new Thickness(10, 0, 10, 0);
                    url.Margin = new Thickness(10, 0, 10, 0);
                    notes.Margin = new Thickness(10, 0, 10, 0);
                    date.Margin = new Thickness(10, 0, 10, 0);
                    
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

                    Grid.SetRow(url, LoginGrid.RowDefinitions.Count - 1);
                    Grid.SetColumn(url, 4);
                    LoginGrid.Children.Add(url);

                    Grid.SetRow(notes, LoginGrid.RowDefinitions.Count - 1);
                    Grid.SetColumn(notes, 5);
                    LoginGrid.Children.Add(notes);

                    Grid.SetRow(date, LoginGrid.RowDefinitions.Count - 1);
                    Grid.SetColumn(date, 6);
                    LoginGrid.Children.Add(date);
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