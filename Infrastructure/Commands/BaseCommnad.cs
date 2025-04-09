using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TestApp_Wpf.Models.Common.Enumerations;

namespace TestApp_Wpf.Infrastructure.Commands;

public abstract class BaseCommand 
    : ICommand, INotifyPropertyChanged
{
    private bool _isEnabled = true;
    private CommandStatus _status = CommandStatus.DEFAULT;
    private Exception? _lastError;
    private readonly SemaphoreSlim _executionLock = new(1, 1);
    
    public event PropertyChangedEventHandler? PropertyChanged;
    public event EventHandler<Exception>? CommandFailed;
    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
   
    public bool IsEnabled
    {
        get => _isEnabled;
        set
        {
            if (_isEnabled != value)
            {
                _isEnabled = value;
                RaiseCanExecuteChanged();
                OnPropertyChanged();
            }
        }
    }
    public bool IsExecuting => 
        _status == CommandStatus.EXECUTING;
    public bool HasError => 
        LastError != null;
    public CommandStatus Status
    {
        get => _status;
        protected set
        {
            if (_status != value)
            {
                _status = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsExecuting));
            }
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

    public virtual bool CanExecute(object? parameter) =>
        IsEnabled && !IsExecuting;
    
    //There is "void" here as it`s ICommand interface method
    public void Execute(object? parameter)
    {
        try
        {
            ExecuteAsync(parameter)
                .GetAwaiter()
                .GetResult();
        }
        catch (AggregateException ex)
        {
            string errorMsg = ex.Flatten()
                .InnerException?.Message ?? ex.Message;

            LastError = new Exception(errorMsg);
            Status = CommandStatus.ERROR;
            CommandFailed?.Invoke(this, ex);
            OnError(ex);
        }
    }

    private async Task ExecuteAsync(object? parameter)
    {
        if (!CanExecute(parameter)) return;

        try
        {
            await _executionLock.WaitAsync();
            LastError = null;
            Status = CommandStatus.EXECUTING;

            var execution = OnExecuteAsync(parameter);
            await execution;

            if (execution.Status == TaskStatus.RanToCompletion)
            {
                Status = CommandStatus.SUCCESS;
            }
            else if (execution.Status == TaskStatus.Canceled) 
            {
                Status = CommandStatus.CANCELED;
            }
            else
            {
                Status = CommandStatus.ERROR;
            }
        }
        finally
        {
            _executionLock.Release();
            RaiseCanExecuteChanged();
        }
    }

    protected abstract Task OnExecuteAsync(object? parameter);

    /// <summary>
    /// Provide custom exception handling for derived classes
    /// </summary>
    /// <param name="ex"></param>
    protected virtual void OnError(Exception ex) { }

    private static void RaiseCanExecuteChanged() =>
        CommandManager.InvalidateRequerySuggested();

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}

