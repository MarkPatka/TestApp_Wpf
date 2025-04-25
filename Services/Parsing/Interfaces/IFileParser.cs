using System.IO;
using TestApp_Wpf.Models.Common.Abstract;

namespace TestApp_Wpf.Services.Parsing.Interfaces;

public interface IFileParser
{
    public bool CanParse(IParsingFile file);
    public Task<List<T>> Parse<T>(string filepath, Type contentType, T instance) where T : IDomainModel;
}
