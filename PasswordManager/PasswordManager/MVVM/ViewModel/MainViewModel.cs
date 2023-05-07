using PasswordManager.Core;
using System.Security.Cryptography;
using System.Security.RightsManagement;
using System.Threading;
using System.Windows.Input;
namespace PasswordManager.MVVM.ViewModel;

public class MainViewModel : ObservableObject
{
    //laver Relaycommands til at skifte view
    public RelayCommand HomeViewCommand { get; }
    public RelayCommand AddPasswordViewCommand { get; }
    public RelayCommand PasswordGenViewCommand { get; }
    public RelayCommand SecuritycheckViewCommand { get; }
    
    public RelayCommand NavigateToPass { get;  }
    
    //laver viewmodels til de forskellige views
    public HomeViewModel HomeVm {get; set;}
    public AddPasswordViewModel AddPasswordVm {get; set;}
    public LoginViewModel LoginVm {get; set;}
    public PasswordGenViewModel PasswordGenVm {get; set;}

    public SecuritycheckViewModel SecuritycheckVm { get; set; }
    
    
    //initialisere objektet _currentView
    private object _currentView = null!;
    
    //laver en property til _currentView
    public object CurrentView
    {
        get => _currentView;
        set
        {
            _currentView = value;
            OnPropertyChanged();
        }
    }

    //laver en constructor til MainViewModel
    public MainViewModel()
    {
        //instantiere  de forskellige viewmodels
        HomeVm = new HomeViewModel();
        AddPasswordVm = new AddPasswordViewModel();
        LoginVm = new LoginViewModel();
        PasswordGenVm = new PasswordGenViewModel();
        SecuritycheckVm = new SecuritycheckViewModel();

        //sætter CurrentView til at være HomeView
        CurrentView = HomeVm;
        
        //laver relaycommands til at skifte view
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