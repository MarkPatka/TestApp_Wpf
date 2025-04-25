using System.Collections.Frozen;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using TestApp_Wpf.Infrastructure.Extensions;
using TestApp_Wpf.Models.Common.Abstract;
using TestApp_Wpf.Services.Parsing.Parsers;

namespace TestApp_Wpf.Infrastructure.Helpers;

public class SupportedParsingTypes
{
    private static readonly FrozenDictionary<Type, string[]> _supportedTypes;

    static SupportedParsingTypes()
    {
        var domainModels = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(t => typeof(IDomainModel)
            .IsAssignableFrom(t)
            && !t.IsAbstract
            && !t.IsInterface);

        Dictionary<Type, string[]> supportedTypes = [];
        foreach (var domainModel in domainModels) 
        {
            var headers = domainModel
                .GetProperties()
                .Select(p => p.Name)
                .ToArray();

            supportedTypes.TryAdd(domainModel, headers);
        }
        _supportedTypes = supportedTypes.ToFrozenDictionary();
    }

    public static Func<FileStream, Task<Type?>> GetFileContentResolver(string fileExtension)
    {
        return fileExtension switch
        {
            ".csv"  => IdentifyCsvContentAsync,
            ".json" => IdentifyJsonContentAsync,
            ".xls"  => IdentifyXlsContentAsync,
            ".xlsx" => IdentifyXlsContentAsync,
            _ => throw new InvalidOperationException(
                $"There is no registered file content type identifier for type \"{fileExtension}\".")
        };
    }


    private static async Task<Type?> IdentifyCsvContentAsync(FileStream fileStream)
    {
        using var reader = new StreamReader(fileStream);
        
        while (!reader.EndOfStream) 
        {
            var headerLine = await reader.ReadLineAsync();
            if (!string.IsNullOrEmpty(headerLine))
            {
                var headers = headerLine
                    .Split(';')
                    .Select(h => h.Trim())
                    .ToArray();

                var objectType = DetermineObjectTypeByFields(headers);

                return objectType;
            }
        }
        throw new InvalidOperationException(
            $"The {Path.GetFileName(fileStream.Name)} is empty - nothing to read");
    }
    private static async Task<Type?> IdentifyJsonContentAsync(FileStream fileStream)
    {
        throw new NotImplementedException();
    }
    private static async Task<Type?> IdentifyXlsContentAsync(FileStream fileStream)
    {
        throw new NotImplementedException();
    }
    private static Type? DetermineObjectTypeByFields(string[] headers)
    {
        foreach (var type in _supportedTypes.Keys)
        {
            var properties = type
                .GetProperties()
                .Select(p => p.Name)
                .ToArray();

            if (headers.SequenceEqual(
                properties, StringComparer.OrdinalIgnoreCase))
            {
                return type;
            }

            if (headers.IsSubsequence(
                properties, StringComparer.OrdinalIgnoreCase))
            {
                return type;
            }
        }
        return null;
    }
}

