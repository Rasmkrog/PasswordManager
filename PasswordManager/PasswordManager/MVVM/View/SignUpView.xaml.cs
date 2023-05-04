using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Controls;
using PasswordManager.Core;
using static PasswordManager.Core.DBConnection;

namespace PasswordManager.MVVM.View;

public partial class SignUpView : Page
{
    public SignUpView()
    {
        InitializeComponent();
        binddatagrid();
    }

    private void binddatagrid()
    {
        //Henter text og indsætter i variabler
        string username = brugernavn.Text;
        string email = Email.Text;
        string Hashed_Password = UserInfo.Password;

        //Skaber forbindelse til SQL database gennem "ConnectionString"
        SqlConnection con = new SqlConnection(ConnectionString);

        //Indsætter brugernavn i database
        con.Open();
        SqlCommand cmd = new SqlCommand();
        string query = "INSERT INTO User(Username, Email, Hashed_Password) Values (@Username, @Email, @Hashed_Password)";
        SqlCommand command = new SqlCommand(query, con);
        command.Parameters.AddWithValue("@Username", username);
        command.Parameters.AddWithValue("@Email", email);
        command.Parameters.AddWithValue("Hashed_Password", UserInfo.Password);

        command.ExecuteNonQuery();
        con.Close();
        
        //cmd.CommandText = "select * from[Username]";    //tjek navn
        
        cmd.Connection = con;
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable("Username");       //samme her, samme navn tho
        da.Fill(dt);
    }

    private string brugernavnText;
    private string kodeordText;

    
    public void SignUp()
    {
        brugernavnText = brugernavn.Text;
        kodeordText = Kodeord.Text;
    }

}