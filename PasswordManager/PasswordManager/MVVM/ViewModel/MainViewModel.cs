using PasswordManager.Core;
using System.Security.Cryptography;
namespace PasswordManager.MVVM.ViewModel;

public class MainViewModel : ObservableObject
{
    public HomeViewModel HomeVm {get; set;}
    
    public AddPasswordViewModel AddPasswordVm {get; set;}
    public LoginViewModel LoginVm {get; set;}
    public PasswordGenViewModel PasswordGenVm {get; set;}
    public UserViewModel UserVm {get; set;}
    
    
    private object _currentView;
    
    public object CurrentView
    {
        get => _currentView;
        set
        {
            _currentView = value;
            OnPropertyChanged();
        }
    }
    

    public MainViewModel()
    {
        HomeVm = new HomeViewModel();
        CurrentView = HomeVm;
    }
}