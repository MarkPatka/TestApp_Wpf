using TestApp_Wpf.Models.DomainModels;
using TestApp_Wpf.Services.Parsing.Interfaces;

namespace TestApp_Wpf.Services.Parsing;

public class ParsingService : IParsingService
{
    private readonly IEnumerable<IFileParser> _parsers;

    public ParsingService(IEnumerable<IFileParser> parsers) =>
        _parsers = parsers;


    public Task<T> ParseFileAsync<T>(IParsingFile file) 
        where T : class
    {
        throw new NotImplementedException();
    }
}
