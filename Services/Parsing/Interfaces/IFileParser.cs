using System.IO;

namespace TestApp_Wpf.Services.Parsing.Interfaces;

public interface IFileParser
{
    public bool CanParse(IParsingFile file);
    public Task Parse(Stream fileStream);
}
