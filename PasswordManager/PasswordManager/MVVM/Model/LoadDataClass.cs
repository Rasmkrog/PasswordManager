using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Data.SqlClient;
using PasswordManager.Core;
using System.Globalization;
using System.Security.RightsManagement;

namespace PasswordManager.MVVM.Model;

public class LoadDataClass
{
    public string Hashed_Password {get; set;}
    public string Salt {get; set;}
    
    
}