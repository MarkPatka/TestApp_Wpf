using Microsoft.Extensions.DependencyInjection;

namespace TestApp_Wpf.Views;

public static class DependencyInjection
{
    public static IServiceCollection AddViews(this IServiceCollection services)
    {
        services.AddSingleton<MainWindow>();

        return services;
    }

}
