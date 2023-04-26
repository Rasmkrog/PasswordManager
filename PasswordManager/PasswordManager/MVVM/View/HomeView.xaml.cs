using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Data.SqlClient;


namespace PasswordManager.MVVM.View;

public partial class HomeView : UserControl
{
    public HomeView()
    {
        InitializeComponent();
    }

    private void TestConnection_Click(object sender, RoutedEventArgs e)
    {
        string connectionString = "Data Source=70.34.198.110; Initial Catalog=2023PWManager; User ID=PWManager; Password=2023PWManager";
        SqlConnection connection = new SqlConnection(connectionString);
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