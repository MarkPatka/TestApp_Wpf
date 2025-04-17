using System.IO;
using TestApp_Wpf.Models.Common.Abstract;
using TestApp_Wpf.Services.Parsing.Interfaces;

namespace TestApp_Wpf.Services.Parsing.Parsers;

public class CsvParser : IFileParser
{
    public bool CanParse(IParsingFile file) =>
        file.Extension.Equals(".csv", StringComparison.OrdinalIgnoreCase);

    public async Task<List<T>> Parse<T>(FileStream fileStream, T contentType)
    {
        //var properties = typeof(T).GetFields();
        // OR
        var properties = typeof(T).GetProperties();

        using var reader = new StreamReader(fileStream);

        var lines = await reader.ReadToEndAsync();

        var rows = lines
            .Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries);

        string[] headers = [.. rows[0].Split(';').Select(v => v.Trim())];

        List<T> models = [];

        for (int i = 1; i < rows.Length - 1; i++)
        {
            var values = rows[i]
                .Split(';')
                .Select(v => v.Trim())
                .ToArray();

            var model = Activator.CreateInstance<T>();

            for (int j = 0; i < headers.Length; i++)
            {
                var property = properties
                    .FirstOrDefault(p => p.Name.Equals(headers[j], StringComparison.OrdinalIgnoreCase));

                if (property != null)
                {
                    if (property.PropertyType.IsValueType || property.PropertyType == typeof(string))
                    {
                        var convertedValue = Convert
                            .ChangeType(values[j], property.PropertyType);

                        property.SetValue(model, convertedValue);
                    }

                    /// TODO: Add maping of complex objects
                }
            }
            models.Add(model);
        }
        return models;
    } 
}
