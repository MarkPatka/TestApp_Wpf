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

            string extension = fileInfo.Extension;
            string name = fileInfo.Name;
            double fileSize = fileInfo.Length / 1024.0d;

            ParsedFileResult parsedFileResult = new(
                name, filePath, extension, fileSize);

            yield return parsedFileResult;
        }  
    }
    public static ParsedFileResult MapParsedFile<T>(
        this string fullPath)
        where T : ParsedFileResult
    {
        try
        {
            FileInfo fileInfo = new(fullPath);

            string extension = fileInfo.Extension;
            string name = fileInfo.Name;
            double fileSize = fileInfo.Length / 1024.0d;

            ParsedFileResult parsedFileResult = new(
                name, fullPath, extension, fileSize);

            return parsedFileResult;
        }
        catch (Exception ex) 
        {
            throw new InvalidOperationException(ex.Message, ex);
        }
        
    }
}
