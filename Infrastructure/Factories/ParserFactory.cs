using Microsoft.Extensions.DependencyInjection;
using TestApp_Wpf.Infrastructure.Factories.Abstract;
using TestApp_Wpf.Services.Parsing.Interfaces;
using TestApp_Wpf.Services.Parsing.Parsers;

namespace TestApp_Wpf.Infrastructure.Factories;

public class ParserFactory(IServiceProvider serviceProvider) 
    : IParserFactory
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public IFileParser CreateParser(string fileExtension)
    {
        return fileExtension switch
        {
            ".csv"  => _serviceProvider.GetRequiredService<CsvParser>(),
            ".json" => _serviceProvider.GetRequiredService<JsonParser>(),
            ".xls"  => _serviceProvider.GetRequiredService<XlsxParser>(),
            ".xlsx" => _serviceProvider.GetRequiredService<XlsxParser>(),
            _ => throw new InvalidOperationException(
                $"There is no registered parser for type \"{fileExtension}\".")
        };
    }
}
