﻿using System.Security.RightsManagement;
namespace PasswordManager.Core;

public class UserInfo
{
    public static string Username { get; set; }

    // ReSharper disable once InconsistentNaming
    public static int? UserID { get; set; } = 4;
    public static string? Password { get; set; } = "password";



}