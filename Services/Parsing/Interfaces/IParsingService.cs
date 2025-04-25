using TestApp_Wpf.Models.Common.Abstract;
using TestApp_Wpf.Models.DomainModels;

namespace TestApp_Wpf.Services.Parsing.Interfaces;

public interface IParsingService
{
    public Task<List<IDomainModel>> ParseFileAsync(IParsingFile file);
}
