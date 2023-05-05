using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using PasswordManager.MVVM.View;

namespace PasswordManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //MainWindow mw = new MainWindow();
            LoginView lv = new LoginView();
            lv.Show();
            base.OnStartup(e);
        }

    }
}