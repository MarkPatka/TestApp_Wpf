using Microsoft.Extensions.DependencyInjection;
using System.Windows.Input;
using TestApp_Wpf.Infrastructure.Commands.Abstract;
using TestApp_Wpf.Infrastructure.Factories.Abstract;

namespace TestApp_Wpf.Infrastructure.Factories;

public class ScopedCommandFactory(IServiceProvider serviceProvider) : IScopedCommandFactory
{
    private readonly List<IDisposable> _disposables = [];
    public event EventHandler? Disposing;

    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public T GetCommand<T>() where T : notnull, BaseCommand
    {
        var service = _serviceProvider.GetRequiredService<T>();
        return service;
    }

    public ICommand CreateParameterizedAsyncCommand(
        Func<object?, Task> execute, 
        Func<object?, bool>? canExecute = null, 
        Action<Exception>? errorHandler = null)
    {
        var command = new BaseCommand(asyncExecute: execute, canExecute: canExecute, errorHandler: errorHandler);
        TrackDisposable(command);
        return command;
    }
    public ICommand CreateAsyncCommand(
        Func<Task> execute, 
        Func<bool>? canExecute = null,
        Action<Exception>? errorHandler = null)
    {
        var command = new BaseCommand(
            asyncExecute: _ => execute(),
            canExecute: _ => canExecute?.Invoke() ?? true,
            errorHandler: errorHandler);

        TrackDisposable(command);
        return command;
    }
    public ICommand CreateParameterizedCommand(
        Action<object?> syncExecute, 
        Func<object?, bool>? canExecute = null, 
        Action<Exception>? errorHandler = null)
    {
        var command = new BaseCommand(
            syncExecute: syncExecute, 
            canExecute: canExecute, 
            errorHandler: errorHandler);

        TrackDisposable(command);
        return command;
    }
    public ICommand CreateCommand(
        Action execute, 
        Func<bool>? canExecute = null, 
        Action<Exception>? errorHandler = null)
    {
        var command = new BaseCommand(
            syncExecute: _ => execute(),
            canExecute: _ => canExecute?.Invoke() ?? true,
            errorHandler: errorHandler);

        TrackDisposable(command);
        return command;
    }


    private void TrackDisposable(ICommand command)
    {
        if (command is IDisposable disposable)
        {
            _disposables.Add(disposable);
        }
    }
    public void Dispose()
    {
        Disposing?.Invoke(this, EventArgs.Empty);

        foreach (var disposable in _disposables)
        {
            disposable.Dispose();
        }
        _disposables.Clear();
    }
}
