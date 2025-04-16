using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TestApp_Wpf.Models.Common.Abstract;

namespace TestApp_Wpf.Models;

public static class DependencyInjection
{
    public static IServiceCollection AddModels(this IServiceCollection services)
    {
        services
            .RegisterDomainModelsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }

    static void RegisterDomainModelsFromAssembly(this IServiceCollection services, Assembly assembly)
    {
        ArgumentNullException.ThrowIfNull(assembly, nameof(assembly));

        var types = assembly.GetTypes();

        var domainModels = types
            .Where(t => typeof(IDomainModel)
            .IsAssignableFrom(t) 
            && !t.IsAbstract 
            && !t.IsInterface);

        foreach (var model in domainModels)
            services.AddTransient(typeof(IDomainModel), model);
    }
}
