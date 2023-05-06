using PasswordManager.Core;
using System.Security.Cryptography;
using System.Security.RightsManagement;
namespace PasswordManager.MVVM.ViewModel;

public class MainViewModel : ObservableObject
{
    public RelayCommand HomeViewCommand { get; }
    public RelayCommand AddPasswordViewCommand { get; }
    public RelayCommand PasswordGenViewCommand { get; }
    public RelayCommand SecuritycheckViewCommand { get; }
    
    
    public HomeViewModel HomeVm {get; set;}
    public AddPasswordViewModel AddPasswordVm {get; set;}
    public LoginViewModel LoginVm {get; set;}
    public PasswordGenViewModel PasswordGenVm {get; set;}

    public SecuritycheckViewModel SecuritycheckVm { get; set; }
    
    
    
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
        SecuritycheckVm = new SecuritycheckViewModel();


        CurrentView = HomeVm;
        
        HomeViewCommand = new RelayCommand( o=>
        {
            CurrentView = HomeVm;
        });
        
        AddPasswordViewCommand = new RelayCommand( o=>
        {
            CurrentView = AddPasswordVm;
        });
        
        PasswordGenViewCommand = new RelayCommand( o=>
        {
            CurrentView = PasswordGenVm;
        });
        
        SecuritycheckViewCommand = new RelayCommand( o=>
        {
            CurrentView = SecuritycheckVm;
        });
        
        
    }
}