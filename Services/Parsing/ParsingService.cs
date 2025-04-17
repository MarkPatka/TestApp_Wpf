using CommandLine;
using System.IO;
using TestApp_Wpf.Infrastructure.Factories.Abstract;
using TestApp_Wpf.Infrastructure.Helpers;
using TestApp_Wpf.Models.Common.Abstract;
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

            using var fileStream = new FileStream(
                file.FullPath, FileMode.Open, FileAccess.ReadWrite);
            
            var contentTypeResolver = SupportedParsingTypes
                .GetFileContentResolver(file.Extension);

            Type? contentType = await contentTypeResolver(fileStream);
            

            if (contentType is not null)
            {
                await parser.Parse(fileStream, contentType);
            }

            await Task.CompletedTask;
        }
        catch (Exception ex) 
        { 
            throw new Exception(ex.Message, ex);
        }
    }
}
