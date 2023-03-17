using System.Windows;
using System.Windows.Controls;

namespace PasswordManager.Components;

public partial class PageLayout : UserControl
{
    public PageLayout(string title, int maxLength)
    {
        InitializeComponent();
        this.DataContext = this;
    }

    public string Title { get; set; }

    public int MaxLength { get; set; }
}