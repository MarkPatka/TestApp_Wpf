using System.Collections.Frozen;

namespace TestApp_Wpf.Infrastructure.Helpers;

public static class AllowedMimeTypes
{
    private static readonly FrozenDictionary<string, string> _frozenMimes;

    static AllowedMimeTypes() =>
        _frozenMimes = new Dictionary<string, string>
        {
            ["csv"]  = "text/csv",
            ["txt"]  = "text/plain",
            ["text"] = "text/plain",
            ["json"] = "application/json",
            ["xls"]  = "application/vnd.ms-excel",
            ["xlsx"] = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        }
        .ToFrozenDictionary();

    public static bool Has(string extension, string mimeMessage)
    {
        if (_frozenMimes.TryGetValue(extension, out string? value))
        {
            return string.Equals(value, mimeMessage);
        }
        return false;
    } 
        
}
