using PasswordManager.Core;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Media;
using static PasswordManager.Core.DBConnection;
using static PasswordManager.MVVM.Model.LoadDataClass;


namespace PasswordManager.MVVM.View;


public partial class HomeView : UserControl
{
    public HomeView()
    {
        //initialize the grid
        LoadData(LoginGrid);
        InitializeComponent();
    }

    private const string SqlQuery = "SELECT Title, Username, Hashed_Password, Email, URL, Notes, Date_Of_Creation From Logins WHERE UserID = @UserID";
    
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
                    title.Text = reader.GetString(0);
                    username.Text = reader.GetString(1);
                    password.Text = password.Text != null ? reader.GetString(2) : "No password found";
                    email.Text = email.Text != null ? reader.GetString(3) : "No email found";
                    url.Text = url.Text != null ? reader.GetString(4) : "No url found";
                    notes.Text = notes.Text != null ? reader.GetString(5) : "";
                    date.Text = reader.GetDateTime(6).ToString(CultureInfo.CurrentCulture);
                    
                    //Set the font size of each TextBlock
                    title.FontSize = 20;
                    username.FontSize = 20;
                    password.FontSize = 20;
                    email.FontSize = 20;
                    url.FontSize = 20;
                    notes.FontSize = 20;
                    date.FontSize = 20;
                    
                    //Set the font color of each TextBlock
                    title.Foreground = Brushes.White;
                    username.Foreground = Brushes.White;
                    password.Foreground = Brushes.White;
                    email.Foreground = Brushes.White;
                    url.Foreground = Brushes.White;
                    notes.Foreground = Brushes.White;
                    date.Foreground = Brushes.White;
                    
                    //Set the margin of each TextBlock
                    title.Margin = new Thickness(10, 0, 0, 0);
                    username.Margin = new Thickness(10, 0, 0, 0);
                    password.Margin = new Thickness(10, 0, 0, 0);
                    email.Margin = new Thickness(10, 0, 0, 0);
                    url.Margin = new Thickness(10, 0, 0, 0);
                    notes.Margin = new Thickness(10, 0, 0, 0);
                    date.Margin = new Thickness(10, 0, 0, 0);
                    
                    //Add the TextBlocks to the grid
                    Grid.SetRow(title, LoginGrid.RowDefinitions.Count - 1);
                    Grid.SetColumn(title, 1);
                    LoginGrid.Children.Add(title);

                    Grid.SetRow(username, LoginGrid.RowDefinitions.Count - 1);
                    Grid.SetColumn(username, 2);
                    LoginGrid.Children.Add(username);

                    Grid.SetRow(password, LoginGrid.RowDefinitions.Count - 1);
                    Grid.SetColumn(password, 3);
                    LoginGrid.Children.Add(password);

                    Grid.SetRow(email, LoginGrid.RowDefinitions.Count - 1);
                    Grid.SetColumn(email, 4);
                    LoginGrid.Children.Add(email);

                    Grid.SetRow(url, LoginGrid.RowDefinitions.Count - 1);
                    Grid.SetColumn(url, 5);
                    LoginGrid.Children.Add(url);

                    Grid.SetRow(notes, LoginGrid.RowDefinitions.Count - 1);
                    Grid.SetColumn(notes, 6);
                    LoginGrid.Children.Add(notes);

                    Grid.SetRow(date, LoginGrid.RowDefinitions.Count - 1);
                    Grid.SetColumn(date, 7);
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