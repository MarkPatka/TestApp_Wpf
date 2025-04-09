using Microsoft.Extensions.FileProviders;
using System.IO;
using TestApp_Wpf.Models.DomainModels;
using TestApp_Wpf.Services.Parsing.Interfaces;

namespace TestApp_Wpf.Services.Parsing.Parsers;

public class CsvParser : IFileParser
{
    public bool CanParse<T>(IParsedFile parsedFile) 
        where T : class
    {
        throw new NotImplementedException();
    }

    public Task<T> Parse<T>(Stream fileStream) 
        where T : class
    {
        throw new NotImplementedException();
    }
}
