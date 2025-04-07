using Microsoft.Extensions.Hosting;
using System;

namespace TestApp_Wpf;

internal static class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        var App = new App();
        
        App.InitializeComponent();        
        App.Run();
        
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
        .ConfigureServices(App.ConfigureServices);
}
