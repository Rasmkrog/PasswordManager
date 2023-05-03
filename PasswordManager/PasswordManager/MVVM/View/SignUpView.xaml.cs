using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Controls;

namespace PasswordManager.MVVM.View;

public partial class SignUpView : Page
{
    public SignUpView()
    {
        InitializeComponent();
        binddatagrid();
    }

    private void binddatagrid() {
        //Skaber forbindelse til SQL database gennem "ConnectionString"
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection.ConnectionString"].ConnectionString;
        
        //Indsætter brugernavn i database
        con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "select * from[Username]";    //tjek navn
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
        kodeordText = kodeord.Text;
    }

}