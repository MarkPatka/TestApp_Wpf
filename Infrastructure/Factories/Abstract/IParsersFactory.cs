using TestApp_Wpf.Services.Parsing.Interfaces;

namespace TestApp_Wpf.Infrastructure.Factories.Abstract;

public interface IParserFactory
{
    IFileParser CreateParser(string fileExtension);
}
