using System.Security.RightsManagement;
namespace PasswordManager.Core;

public class UserInfo
{
    public string? UserName { get; set; }

    // ReSharper disable once InconsistentNaming
    public static int? UserID { get; set; } = 5;
    public static string? Password { get; set; } = "password";



}