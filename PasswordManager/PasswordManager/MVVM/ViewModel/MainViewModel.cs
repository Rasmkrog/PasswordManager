using PasswordManager.Core;
using System.Security.Cryptography;
using System.Security.RightsManagement;
namespace PasswordManager.MVVM.ViewModel;

public class MainViewModel : ObservableObject
{
    public RelayCommand HomeViewCommand { get; }
    public RelayCommand AddPasswordViewCommand { get; }
    public RelayCommand LoginViewCommand { get; }
    public RelayCommand PasswordGenViewCommand { get; }
    public RelayCommand UserViewCommand { get;}
    public RelayCommand SignUpViewCommand { get; }
    
    
    public HomeViewModel HomeVm {get; set;}
    public AddPasswordViewModel AddPasswordVm {get; set;}
    public LoginViewModel LoginVm {get; set;}
    public PasswordGenViewModel PasswordGenVm {get; set;}
    public UserViewModel UserVm {get; set;}
    public SignUpViewModel SignUpVm {get; set;}
    
    
    
    private object _currentView = null!;
    
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
        SignUpVm = new SignUpViewModel();
  
        
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
        
        SignUpViewCommand = new RelayCommand( o=>
        {
            CurrentView = SignUpVm;
        });
        
    }
}