﻿using Microsoft.Extensions.DependencyInjection;
using System.Windows.Input;
using TestApp_Wpf.Infrastructure.Commands.Abstract;
using TestApp_Wpf.Infrastructure.Factories.Abstract;

namespace TestApp_Wpf.Infrastructure.Factories;

public class CommandFactory(IServiceProvider serviceProvider) : ICommandFactory
{
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
        => new BaseCommand(
            asyncExecute: execute,
            canExecute: canExecute,
            errorHandler: errorHandler);

    public ICommand CreateAsyncCommand(
        Func<Task> execute, 
        Func<bool>? canExecute = null, 
        Action<Exception>? errorHandler = null)
        => new BaseCommand(
            asyncExecute: _ => execute(),
            canExecute: _ => canExecute?.Invoke() ?? true,
            errorHandler: errorHandler);

    public ICommand CreateParameterizedCommand(
        Action<object?> execute, 
        Func<object?, bool>? canExecute = null, 
        Action<Exception>? errorHandler = null)
        => new BaseCommand(
            syncExecute: execute, 
            canExecute: canExecute,
            errorHandler: errorHandler);

    public ICommand CreateCommand(
        Action execute,
        Func<bool>? canExecute = null, 
        Action<Exception>? errorHandler = null)
        => new BaseCommand(
            syncExecute: _ => execute(),
            canExecute: _ => canExecute?.Invoke() ?? true,
            errorHandler: errorHandler);
}

