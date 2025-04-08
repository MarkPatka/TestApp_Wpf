using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using System.Configuration;
using System.Data;
using System.Windows;
using TestApp_Wpf.Services;
using TestApp_Wpf.ViewModels;

namespace TestApp_Wpf;

public partial class App : Application 
{
    private static readonly IHost _host;

    static App() => _host = Host
        .CreateDefaultBuilder(
            Environment.GetCommandLineArgs())
        .ConfigureServices(RegisterDependencies)
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
        ?? throw new ApplicationException("Application services are not initialized");

    internal static void RegisterDependencies(HostBuilderContext host, IServiceCollection services) => 
        services
        .AddServices()
        .AddViewModels()
        ;

    protected override async void OnStartup(StartupEventArgs e)
    {
        try
        {
            await _host.StartAsync().ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An error occurred during startup: {ex.Message}");
        }
        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        using (_host) await _host.StopAsync().ConfigureAwait(false);
        base.OnExit(e);

    }
}
