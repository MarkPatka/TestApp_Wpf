using System.IO;
using TestApp_Wpf.Models.DomainModels;

namespace TestApp_Wpf.Services.Parsing.Interfaces;

public interface IFileParser
{
    public bool CanParse<T>(IParsedFile parsedFile) where T : class;
    public Task<T> Parse<T>(Stream fileStream) where T : class;
}
