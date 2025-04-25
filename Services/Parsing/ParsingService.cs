using System.IO;
using TestApp_Wpf.Infrastructure.Factories.Abstract;
using TestApp_Wpf.Infrastructure.Helpers;
using TestApp_Wpf.Models.Common.Abstract;
using TestApp_Wpf.Services.Parsing.Interfaces;

namespace TestApp_Wpf.Services.Parsing;

public class ParsingService(IParserFactory parsers) 
    : IParsingService
{
    private readonly IParserFactory _parsers = parsers;

    public async Task<List<IDomainModel>> ParseFileAsync(IParsingFile file) 
    {
        try
        {
            var parser = _parsers.CreateParser(file.Extension);

            Type type = await SetDomainObjectType(file);

            IDomainModel instance = (dynamic)Activator
                .CreateInstance(type)!;

            var result = await parser
                .Parse(file.FullPath, type, instance);

            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }

    private static async Task<Type> SetDomainObjectType(IParsingFile file)
    {
        using var fileStream = new FileStream(
            file.FullPath, FileMode.Open, FileAccess.Read);

        var contentTypeResolver = SupportedParsingTypes
            .GetFileContentResolver(file.Extension);

        Type contentType = await contentTypeResolver(fileStream)
            ?? throw new Exception($"Unknown file type received ({file.Extension})");

        return contentType;
    }
}
