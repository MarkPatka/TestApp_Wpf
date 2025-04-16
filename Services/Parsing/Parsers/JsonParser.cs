using System.IO;
using TestApp_Wpf.Models.DomainModels;
using TestApp_Wpf.Services.Parsing.Interfaces;

namespace TestApp_Wpf.Services.Parsing.Parsers;

public class JsonParser : IFileParser
{
    public bool CanParse(IParsingFile file)
    {
        throw new NotImplementedException();
    }

    public Task Parse(Stream fileStream)
    {
        throw new NotImplementedException();
    }
}
