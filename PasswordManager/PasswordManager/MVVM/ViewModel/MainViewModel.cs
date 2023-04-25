using PasswordManager.Core;
using System.Security.Cryptography;
namespace PasswordManager.MVVM.ViewModel;

public class MainViewModel : ObservableObject
{
    public RelayCommand HomeViewCommand { get; set; }
    public RelayCommand AddPasswordViewCommand { get; set; }
    public RelayCommand LoginViewCommand { get; set; }
    public RelayCommand PasswordGenViewCommand { get; set; }
    public RelayCommand UserViewCommand { get; set; }
    
    
    
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
        AddPasswordVm = new AddPasswordViewModel();
        LoginVm = new LoginViewModel();
        PasswordGenVm = new PasswordGenViewModel();
        UserVm = new UserViewModel();
        
        CurrentView = HomeVm;
        
        HomeViewCommand = new RelayCommand( o=>
        {
            CurrentView = HomeVm;
        });
        
        AddPasswordViewCommand = new RelayCommand( o=>
        {
            CurrentView = AddPasswordVm;
        });
        
        LoginViewCommand = new RelayCommand( o=>
        {
            CurrentView = LoginVm;
        });
        
        PasswordGenViewCommand = new RelayCommand( o=>
        {
            CurrentView = PasswordGenVm;
        });
        
        UserViewCommand = new RelayCommand( o=>
        {
            CurrentView = UserVm;
        });
    }
}