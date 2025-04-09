using System.IO;
using TestApp_Wpf.Services.Parsing.Interfaces;

namespace TestApp_Wpf.Services.Parsing.Parsers;

public class XlsParser : IFileParser
{
    public bool CanParse<T>(IParsedFile parsedFile) 
        where T : class
    {
        throw new NotImplementedException();
    }

    public Task<T> Parse<T>(Stream fileStream) 
        where T : class
    {

        //using var package = new ExcelPackage(fileStream);
        //var sheet = package.Workbook.Worksheets[0];

        //var data = ParseSheet(sheet);
        //return new ParsedResult();

        throw new NotImplementedException();
    }
}
