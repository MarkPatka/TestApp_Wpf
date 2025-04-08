using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TestApp_Wpf.Models.Common.Enumerations;

namespace TestApp_Wpf.Infrastructure.Commands;

public abstract class BaseCommand 
    : ICommand, INotifyPropertyChanged
{
    private bool _isExecuting;
    private CommandStatus _statusMessage = CommandStatus.DEFAULT;
    private Exception? _lastError;

    public bool IsExecuting
    {
        get => _isExecuting;
        protected set
        {
            if (_isExecuting != value)
            {
                _isExecuting = value;
                OnPropertyChanged();
                OnCanExecuteChanged();
            }
        }
    }

    public CommandStatus Status
    {
        get => _statusMessage;
        protected set
        {
            _statusMessage = value;
            OnPropertyChanged();
        }
    }

    public Exception? LastError
    {
        get => _lastError;
        protected set
        {
            _lastError = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(HasError));
        }
    }

    public bool HasError => LastError != null;

    public event EventHandler? CanExecuteChanged;
    public event PropertyChangedEventHandler? PropertyChanged;

    public virtual bool CanExecute(object? parameter)
    {
        return !IsExecuting;
    }

    public async void Execute(object? parameter)
    {
        if (!CanExecute(parameter)) return;

        try
        {
            IsExecuting = true;
            LastError = null;
            Status = CommandStatus.EXECUTING;

            await ExecuteAsync(parameter).ConfigureAwait(true);

            Status = CommandStatus.SUCCESS;
        }
        catch (Exception ex)
        {
            LastError = ex;
            Status = CommandStatus.ERROR;
            OnError(ex);
        }
        finally
        {
            IsExecuting = false;
        }
    }

    protected abstract Task ExecuteAsync(object? parameter);

    /// <summary>
    /// Provide custom exception handling for derived classes
    /// </summary>
    /// <param name="ex"></param>
    protected virtual void OnError(Exception ex) { }

    protected virtual void OnCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
