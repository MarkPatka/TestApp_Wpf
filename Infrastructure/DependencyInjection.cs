using Microsoft.Extensions.DependencyInjection;
using TestApp_Wpf.Infrastructure.Commands.MainViewModel.Commands;
using TestApp_Wpf.Infrastructure.Factories;
using TestApp_Wpf.Infrastructure.Factories.Abstract;

namespace TestApp_Wpf.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services
            .RegisterCommands()
            .RegisterScopedCommands()
            .RegisterFactories();

        return services;
    }

    private static IServiceCollection RegisterFactories(this IServiceCollection services)
    {
        services
            .AddTransient<ICommandFactory, CommandFactory>()
            .AddScoped<IScopedCommandFactory, ScopedCommandFactory>()
            .AddScoped<IParserFactory, ParserFactory>();

        return services;
    }

    private static IServiceCollection RegisterCommands(this IServiceCollection services)
    {
        return services;
    }
    private static IServiceCollection RegisterScopedCommands(this IServiceCollection services)
    {
        services
            .AddScoped<LoadTestObjectsCommand>();

        return services;
    }
}
