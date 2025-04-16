using Microsoft.Extensions.DependencyInjection;
using TestApp_Wpf.Infrastructure.Commands.Abstract;
using TestApp_Wpf.Infrastructure.Factories.Abstract;

namespace TestApp_Wpf.Infrastructure.Factories;

public class CommandFactory(IServiceProvider serviceProvider)
    : ICommandFactory
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public T CreateCommand<T>() where T : BaseCommand =>
        _serviceProvider.GetRequiredService<T>();
}

