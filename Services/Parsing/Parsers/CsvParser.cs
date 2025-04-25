using System.IO;
using System.Reflection;
using TestApp_Wpf.Infrastructure.Converters;
using TestApp_Wpf.Models.Common.Abstract;
using TestApp_Wpf.Services.Parsing.Interfaces;

namespace TestApp_Wpf.Services.Parsing.Parsers;

public class CsvParser : IFileParser
{
    public bool CanParse(IParsingFile file) =>
        file.Extension.Equals(".csv", StringComparison.OrdinalIgnoreCase);

    public async Task<List<T>> Parse<T>(string filepath, Type type, T instance)
        where T : IDomainModel
    {
        using var reader = new StreamReader(filepath,
            new FileStreamOptions { Access = FileAccess.Read, Mode = FileMode.Open });

        var properties = type.GetProperties();

        var lines = await reader.ReadToEndAsync();

        var rows = lines
            .Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries);

        string[] headers = [.. rows[0].Split(';').Select(v => v.Trim())];

        return ConstructModels(instance, properties, rows, headers);
    }
    
    private static List<T> ConstructModels<T>(
        T instance, 
        PropertyInfo[] properties, 
        string[] rows, 
        string[] headers)
        where T : IDomainModel
    {
        List<T> models = [];
        for (int i = 1; i < rows.Length; i++)
        {
            string[] values = [.. rows[i].Split(';').Select(v => v.Trim())];

            T model = instance;

            for (int j = 0; j < headers.Length; j++)
            {
                string header = headers[j];

                PropertyInfo? property = properties
                    .FirstOrDefault(p => p.Name.Equals(header, StringComparison.OrdinalIgnoreCase));

                if (property != null)
                {
                    try
                    {
                        var convertedValue = Convert
                            .ChangeType(values[j], property.PropertyType);

                        property.SetValue(model, convertedValue);
                    }
                    catch
                    {
                        if (property.PropertyType == typeof(bool))
                        {
                            var val = new StringToBooleanConverter()
                                .Convert(values[j], property.PropertyType);

                            if (!val.IsError)
                            {
                                property.SetValue(model, val.Value);
                                continue;
                            }
                        }
                        throw new Exception($"Failed " +
                            $"to set value \"{values[j]}\"" +
                            $"to property \"{property.Name}\" " +
                            $"of type \"{property.PropertyType}\"");
                    }
                    /// TODO: Add maping of complex objects
                }
            }
            models.Add(model);
        }
        return models;
    }
}
