using System.Windows.Controls;

namespace PasswordManager.Components;

public partial class PageLayout : UserControl
{
    public PageLayout()
    {
        
        InitializeComponent();
    }

    
    public object Header { get; set; }
}