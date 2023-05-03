using System.Windows.Controls;

namespace PasswordManager.MVVM.View;

public partial class SignUpView : Page
{
    public SignUpView()
    {
        InitializeComponent();
    }

    private string brugernavnText;
    private string kodeordText;

    public void SignUp()
    {
        brugernavnText = brugernavn.Text;
        kodeordText = kodeord.Text;
    }
}