using TestApp_Wpf.Infrastructure.Factories.Abstract;
using TestApp_Wpf.Models.DomainModels;
using TestApp_Wpf.Services.Parsing.Interfaces;

namespace TestApp_Wpf.Services.Parsing;

public class ParsingService : IParsingService
{
    private readonly IParserFactory _parsers;

    public ParsingService(IParserFactory parsers) =>
        _parsers = parsers;


    public async Task ParseFileAsync(IParsingFile file) 
    {
        try
        {
            var parser = _parsers.CreateParser(file.Extension);

            await Task.CompletedTask;
        }
        catch (Exception ex) 
        { 
            throw new Exception(ex.Message, ex);
        }
    }
}
