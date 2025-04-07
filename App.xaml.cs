using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Data;
using System.Windows;
using TestApp_Wpf.Services;
using TestApp_Wpf.ViewModels;

namespace TestApp_Wpf;
public partial class App : Application 
{
    private readonly IHost _host;
    
    public App() => _host ??= Program
        .CreateHostBuilder(Environment.GetCommandLineArgs())
        .Build();

    public static Window? FocusedWindow =>
        Current.Windows.Cast<Window>()
        .FirstOrDefault(w => w.IsFocused);

    public static Window? ActivedWindow =>
        Current.Windows.Cast<Window>()
        .FirstOrDefault(w => w.IsActive);

    public static Window? GetTitledWindow(string title) =>
        Current.Windows.Cast<Window>()
        .FirstOrDefault(w => w.Title.Equals(title));

    public IServiceProvider Services => _host.Services 
        ?? throw new ApplicationException("Application is not initialized");

    internal static void ConfigureServices(HostBuilderContext host, IServiceCollection services) => 
        services
        .AddServices()
        .AddViewModels()
        ;

    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        try
        {
            await _host.StartAsync().ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An error occurred during startup: {ex.Message}");
        }
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        base.OnExit(e);
        using (_host) await _host.StopAsync().ConfigureAwait(false);
    }
}
