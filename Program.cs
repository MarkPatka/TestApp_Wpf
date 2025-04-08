using CommandLine;
using System.Windows;

namespace TestApp_Wpf;

internal static class Program
{
    private const int TimeoutSeconds = 3;
    
    private static readonly Mutex _mutex;
    
    private static bool _initialized;

    static Program() =>
        _mutex = new(false, typeof(Program).FullName);

    [STAThread]
    public static void Main(string[] args)
    {
        Parsed<CommandLineOptions> parserResult = Parser.Default
            .ParseArguments<CommandLineOptions>(args)
            .Cast<Parsed<CommandLineOptions>>();

        /// You can add any logic with parsed cmd arguments. 
        /// I`m just used to do this way.
        
        try
        {
            if (!_mutex.WaitOne(TimeSpan.FromSeconds(TimeoutSeconds), true))
            {
                MessageBox.Show(
                    $"Application is already running",
                    "Warning",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }

            SubscribeToDomainUnhandledExceptions();            
            var app = InitializApplication();
            RunApplication(app);
        }
        finally
        {
            _mutex.ReleaseMutex();
        }
    }

    private static void SubscribeToDomainUnhandledExceptions() =>
        AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
        {
            Exception ex = (Exception)args.ExceptionObject;

            MessageBox.Show(
                $"An unhandled exception occurred: {ex.Message}",
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        };

    private static Application InitializApplication()
    {
        if (_initialized) return App.Current;
        else
        {
            var app = new App();
            _initialized = true;
            return app;
        }
    }

    private static int RunApplication(Application current)
    {
        if (_initialized)
        {
            return current.Run();
        }
        return InitializApplication().Run();
    }
}
