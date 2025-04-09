using Microsoft.Extensions.DependencyInjection;
using TestApp_Wpf.ViewModels.Interfaces;

namespace TestApp_Wpf.ViewModels;

public static class ViewModelLocator
{
    public static IMainViewModel MainViewModel =>
        App.Services.GetRequiredService<IMainViewModel>();
}
