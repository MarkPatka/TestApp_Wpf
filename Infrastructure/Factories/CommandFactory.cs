using Microsoft.Extensions.DependencyInjection;
using TestApp_Wpf.Infrastructure.Commands.Abstract;

namespace TestApp_Wpf.Infrastructure.Factories;

public class CommandFactory : ICommandFactory
{
    private readonly IServiceProvider _serviceProvider;

    public CommandFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public T CreateCommand<T>() where T : BaseCommand =>
        _serviceProvider.GetRequiredService<T>();
}

