using Microsoft.Extensions.DependencyInjection;
using TestApp_Wpf.Infrastructure.Factories.Abstract;
using TestApp_Wpf.Services.Parsing.Interfaces;
using TestApp_Wpf.Services.Parsing.Parsers;

namespace TestApp_Wpf.Infrastructure.Factories;

public class ParserFactory(IEnumerable<IFileParser> serviceProvider) 
    : IParserFactory
{
    private readonly IEnumerable<IFileParser> _serviceProvider = serviceProvider;

    public IFileParser CreateParser(string fileExtension)
    {

        return fileExtension switch
        {
            ".csv"  => GetService(typeof(CsvParser)),
            ".json" => GetService(typeof(JsonParser)),
            ".xls"  => GetService(typeof(XlsxParser)),
            ".xlsx" => GetService(typeof(XlsxParser)),
            _ => throw new InvalidOperationException(
                $"There is no registered parser for type \"{fileExtension}\".")
        };
    }

    public IFileParser GetService(Type type)
    {
        return _serviceProvider.FirstOrDefault(x => x.GetType() == type) ?? 
            throw new InvalidOperationException(
                $"There is no registered parser for type \"{type.Name}\".");
    }
}
