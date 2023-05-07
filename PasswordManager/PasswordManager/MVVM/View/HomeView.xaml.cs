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



namespace PasswordManager.MVVM.View;


public partial class HomeView : UserControl
{
    [Obsolete("Obsolete")]
    public HomeView()
    {
        
        InitializeComponent();
        
        //initialize the grid
        LoadData(LoginGrid, NumberOfLogins, loginstext, HeaderGrid);
        
    }

    private const string SqlQuery = "SELECT Title, Username, Hashed_Password, Salt, Email, URL, Notes, Date_Of_Creation From Logins WHERE UserID = @UserID";
    private int _rows;
    
    [Obsolete("Obsolete")]
    public async void LoadData(Grid? LoginGrid, TextBlock? NumberOfLogins, TextBlock? loginstext, Grid? HeaderGrid)
    {
        
        _rows = 0;
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
                /*
                //create scrollviewer
                ScrollViewer scrollViewer = new ScrollViewer();
                scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
                scrollViewer.Height = 500;
                scrollViewer.Width = 1000;
                scrollViewer.Margin = new Thickness(0, 0, 0, 0);
                scrollViewer.BorderThickness = new Thickness(1);
                scrollViewer.VerticalAlignment = VerticalAlignment.Top;
                scrollViewer.HorizontalAlignment = HorizontalAlignment.Left;
                scrollViewer.CanContentScroll = true;
                scrollViewer.PanningMode = PanningMode.VerticalOnly;
                scrollViewer.PanningDeceleration = 0.001;
                scrollViewer.PanningRatio = 0.5;
                scrollViewer.IsDeferredScrollingEnabled = true;
                scrollViewer.IsManipulationEnabled = true;
                scrollViewer.IsHitTestVisible = true;
                scrollViewer.IsTabStop = true;
                
                //add the scrollviewer to the grid
                LoginGrid?.Children.Add(scrollViewer);*/
                

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
                titleHeader.FontSize = 18;
                usernameHeader.FontSize = 18;
                passwordHeader.FontSize = 18;
                emailHeader.FontSize = 18;
                dateHeader.FontSize = 18;

                //set the font family of each header
                titleHeader.FontFamily = new FontFamily("Nunito");
                usernameHeader.FontFamily = new FontFamily("Nunito");
                passwordHeader.FontFamily = new FontFamily("Nunito");
                emailHeader.FontFamily = new FontFamily("Nunito");
                dateHeader.FontFamily = new FontFamily("Nunito");
                
                //set font weight of each header
                titleHeader.FontWeight = FontWeights.Bold;
                usernameHeader.FontWeight = FontWeights.Bold;
                passwordHeader.FontWeight = FontWeights.Bold;
                emailHeader.FontWeight = FontWeights.Bold;
                dateHeader.FontWeight = FontWeights.Bold;
                
                
                
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
                this.HeaderGrid.Children.Add(titleHeader);
                
                Grid.SetRow(usernameHeader, 0);
                Grid.SetColumn(usernameHeader, 1);
                this.HeaderGrid.Children.Add(usernameHeader);
                
                Grid.SetRow(passwordHeader, 0);
                Grid.SetColumn(passwordHeader, 2);
                this.HeaderGrid.Children.Add(passwordHeader);
                
                Grid.SetRow(emailHeader, 0);
                Grid.SetColumn(emailHeader, 3);
                this.HeaderGrid.Children.Add(emailHeader);
                
                
                Grid.SetRow(dateHeader, 0);
                Grid.SetColumn(dateHeader, 5);
                this.HeaderGrid.Children.Add(dateHeader);
                
                while (reader.Read())
                {
                    _rows++;
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
                    title.FontSize = 16;
                    username.FontSize = 16;
                    password.FontSize = 16;
                    email.FontSize = 16;
                    date.FontSize = 16;
                    
                    title.FontFamily = new FontFamily("Nunito");
                    username.FontFamily = new FontFamily("Nunito");
                    password.FontFamily = new FontFamily("Nunito");
                    email.FontFamily = new FontFamily("Nunito");
                    date.FontFamily = new FontFamily("Nunito");
                
                    //set font weight of each textblock
                    title.FontWeight = FontWeights.Medium;
                    username.FontWeight = FontWeights.Medium;
                    password.FontWeight = FontWeights.Medium;
                    email.FontWeight = FontWeights.Medium;
                    date.FontWeight = FontWeights.Medium;
                    
                    
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
                    
                    LoginGrid.ShowGridLines= true;
                }
                if (_rows-1 == 1)
                {
                    NumberOfLogins.Text = $"{_rows + 1}"; 
                    loginstext.Text = "login";
                }
                else
                {
                    NumberOfLogins.Text = $"{_rows--}";
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