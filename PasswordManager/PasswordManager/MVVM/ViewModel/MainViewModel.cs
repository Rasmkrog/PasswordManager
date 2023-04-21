using PasswordManager.Core;
using System.Security.Cryptography;
namespace PasswordManager.MVVM.ViewModel;

public class MainViewModel : ObservableObject
{
    public HomeViewModel HomeVM {get; set;}
    
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
    PbeEncryptionAlgorithm pbe = new PbeEncryptionAlgorithm();
    
    
    public MainViewModel()
    {
        HomeVM = new HomeViewModel();
        CurrentView = HomeVM;
    }
}