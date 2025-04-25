using System.Windows.Input;
using TestApp_Wpf.Infrastructure.Commands.Abstract;
using TestApp_Wpf.Infrastructure.Commands.MainViewModel.Commands;

namespace TestApp_Wpf.Infrastructure.Factories.Abstract;

public interface ICommandFactory
{
    public T GetCommand<T>() where T : BaseCommand;



    /// <summary>
    /// Creates a command from an asynchronous operation with a parameter.
    /// </summary>
    ICommand CreateParameterizedAsyncCommand(
        Func<object?, Task> execute,
        Func<object?, bool>? canExecute = null,
        Action<Exception>? errorHandler = null);

    /// <summary>
    /// Creates a command from an asynchronous operation without a parameter.
    /// </summary>
    ICommand CreateAsyncCommand(
        Func<Task> execute,
        Func<bool>? canExecute = null,
        Action<Exception>? errorHandler = default);

    /// <summary>
    /// Creates a command from a synchronous operation with a parameter.
    /// </summary>
    ICommand CreateParameterizedCommand(
        Action<object?> syncExecute,
        Func<object?, bool>? canExecute = null,
        Action<Exception>? errorHandler = null);


    /// <summary>
    /// Creates a command from a synchronous operation without a parameter.
    /// </summary>
    ICommand CreateCommand(
        Action execute,
        Func<bool>? canExecute = null,
        Action<Exception>? errorHandler = null);
}

public interface IScopedCommandFactory 
    : ICommandFactory, IDisposable 
{
    event EventHandler Disposing;
}