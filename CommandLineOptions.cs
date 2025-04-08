using CommandLine;

namespace TestApp_Wpf;

/// <summary>
/// To add any launc logic in further. That is just a demo
/// </summary>
public sealed class CommandLineOptions
{
    [Option('i', "incognito", Required = false, HelpText = "Private mode")]
    public bool IsIncognitoModeEnabled { get; set; }
}
