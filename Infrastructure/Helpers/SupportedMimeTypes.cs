using System.Collections.Frozen;

namespace TestApp_Wpf.Infrastructure.Helpers;

public static class SupportedMimeTypes
{
    private static readonly List<string> _textTypeExtensions;
    private static readonly FrozenDictionary<string, string> _frozenMimeExtensions;
    private static readonly FrozenDictionary<string, string> _frozenMimeCodes;

    public static FrozenDictionary<string, string> MimeTypes => _frozenMimeExtensions;
    public static FrozenDictionary<string, string> MimeCodes => _frozenMimeCodes;

    static SupportedMimeTypes()
    {
        _textTypeExtensions = [".csv", ".txt", ".text", ".json"];

        _frozenMimeExtensions = new Dictionary<string, string>
        {
            [".csv"]  = "text/csv",
            [".txt"]  = "text/plain",
            [".text"] = "text/plain",
            [".json"] = "application/json",
            [".xls"]  = "application/vnd.ms-excel",
            [".xlsx"] = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        }
        .ToFrozenDictionary();

        _frozenMimeCodes = new Dictionary<string, string>
        {
            ["D0CF11E0A1B11AE1"] = "application/vnd.ms-excel", 
            ["504B0304"]         = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
        }
        .ToFrozenDictionary();
    }

    public static bool IsTextType(string typeExtension) =>
        !string.IsNullOrEmpty(_textTypeExtensions.Find(ext => ext.Equals(typeExtension)));
    public static bool Contains(string extension, string mimeMessage)
    {
        if (_frozenMimeExtensions.TryGetValue(extension, out string? value))
        {
            return string.Equals(value, mimeMessage);
        }
        return false;
    }
    public static bool TryGetMimeMessage(string hexCode, out string message)
    {
        if (_frozenMimeCodes.TryGetValue(hexCode, out string? value))
        {
            message = value;
            return true;
        }
        message = "application/octet-stream";
        return false;
    }
        

}
