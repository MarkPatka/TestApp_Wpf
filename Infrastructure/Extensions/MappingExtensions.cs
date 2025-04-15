using System.IO;
using TestApp_Wpf.Models.ParsedModels;

namespace TestApp_Wpf.Infrastructure.Extensions;

public static class MappingExtensions
{
    public static IEnumerable<ParsedFileResult> MapParsedFile<T>(
        this IEnumerable<string> fullPaths) 
        where T : ParsedFileResult
    {
        foreach (var filePath in fullPaths) 
        {
            FileInfo fileInfo = new(filePath);

            string name = fileInfo.Name;
            double fileSize = fileInfo.Length / 1024.0d;

            ParsedFileResult parsedFileResult = new(
                name, filePath, fileSize);

            yield return parsedFileResult;
        }   
      
    }
}
