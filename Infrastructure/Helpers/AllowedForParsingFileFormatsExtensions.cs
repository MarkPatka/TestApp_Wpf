namespace TestApp_Wpf.Infrastructure.Helpers;

public static class AllowedForParsingFileFormatsExtensions
{
    private static readonly string[] _extensions;

    static AllowedForParsingFileFormatsExtensions() =>
        _extensions = ["csv", "xlsx", "json", ".csv", ".xlsx", ".json"];

    public static bool Contains(string extension) => 
        _extensions.FirstOrDefault(
            x => string.Equals(x, extension, StringComparison.OrdinalIgnoreCase)) != null;
}
