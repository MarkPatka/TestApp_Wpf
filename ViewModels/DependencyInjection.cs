using Microsoft.Extensions.DependencyInjection;
using TestApp_Wpf.ViewModels.Abstract;
using TestApp_Wpf.ViewModels.Implementations;

namespace TestApp_Wpf.ViewModels;

public static class DependencyInjection
{
    public static IServiceCollection AddViewModels(this IServiceCollection services)
    {
        services
            .AddTransient<IMainViewModel, MainViewModel>();

        return services;
    }

}
