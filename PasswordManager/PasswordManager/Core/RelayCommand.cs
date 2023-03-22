using System;
using System.Windows.Input;

namespace PasswordManager.Core;

class RelayCommand : ICommand
{
    
    private Action<Object> execute;
    private Func<object, bool> canExecute;

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    protected RelayCommand(Action<Object> execute, Func<object, bool> canExecute)
    {
        this.execute = execute;
        this.canExecute = canExecute;
    }
    
    public bool CanExecute(object? parameter)
    {
        return parameter != null && this.canExecute(parameter);
    }
    
    public void Execute(object? parameter)
    {
        if (parameter != null) this.execute(parameter);
    }
}