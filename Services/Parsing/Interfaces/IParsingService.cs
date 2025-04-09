using TestApp_Wpf.Models.DomainModels;

namespace TestApp_Wpf.Services.Parsing.Interfaces;

public interface IParsingService
{
    public Task<T> ParseFileAsync<T>(IParsedFile file) where T : class;
}
