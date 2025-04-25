using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TestApp_Wpf.Models.Common.Abstract;

namespace TestApp_Wpf.Infrastructure.Commands.Abstract;

public class BaseCommand : ICommand
{    
    private readonly Func<object?, Task>? _asyncExecute;
    private readonly Action<object?>?     _syncExecute;
    private readonly Func<object?, bool>? _canExecute;
    private readonly Action<Exception>?   _errorHandler;
    private bool _isExecuting;

    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Occurs when changes the command should execute
    /// </summary>
    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public BaseCommand(
        Func<object?, Task>? asyncExecute = null,
        Action<object?>? syncExecute = null,
        Func<object?, bool>? canExecute = null,
        Action<Exception>? errorHandler = null)
    {
        _asyncExecute = asyncExecute;
        _syncExecute = syncExecute;
        _canExecute = canExecute;
        _errorHandler = errorHandler;
    }

    /// <summary>
    /// Determines whether the command can execute in its current state
    /// </summary>
    public bool CanExecute(object? parameter = null)
    {
        if (_isExecuting) return false;
        return _canExecute?.Invoke(parameter) ?? true;
    }

    /// <summary>
    /// Executes the command
    /// </summary>
    public async void Execute(object? parameter = null)
    {
        if (!CanExecute(parameter)) return;

        try
        {
            _isExecuting = true;
            RaiseCanExecuteChanged();

            if (_asyncExecute != null)
            {
                await _asyncExecute(parameter)
                    .ConfigureAwait(false);
            }
            else
            {
                _syncExecute?
                    .Invoke(parameter);
            }
        }
        catch (Exception ex) when (_errorHandler != null)
        {
            _errorHandler(ex);
        }
        finally
        {
            _isExecuting = false;
            RaiseCanExecuteChanged();
        }
    }

    /// <summary>
    /// Raises the CanExecuteChanged event to force requery of command state
    /// </summary>
    public void RaiseCanExecuteChanged() => CommandManager.InvalidateRequerySuggested();

    protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null!)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
    }
}

