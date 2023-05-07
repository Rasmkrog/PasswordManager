using PasswordManager.MVVM.ViewModel;
using System;
using System.Windows.Input;
namespace PasswordManager.Core;

public class UpdataViewCommand : ICommand
{
    private MainViewModel viewModel;  
    
    public UpdataViewCommand(MainViewModel viewModel)
    {
        this.viewModel = viewModel;
    }
    
    public event EventHandler? CanExecuteChanged;
    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        Console.WriteLine("test");
        if (parameter!.ToString() == "Home")
        {
            viewModel.CurrentView = viewModel;
        }
    }

   
}