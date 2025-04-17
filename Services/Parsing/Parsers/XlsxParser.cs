using System.IO;
using TestApp_Wpf.Models.Common.Abstract;
using TestApp_Wpf.Services.Parsing.Interfaces;

namespace TestApp_Wpf.Services.Parsing.Parsers;

public class XlsxParser : IFileParser
{
    public bool CanParse(IParsingFile file)
    {
        throw new NotImplementedException();
    }

    public Task<List<T>> Parse<T>(FileStream fileStream, T contentType)
    {
        //using var package = new ExcelPackage(fileStream);
        //var sheet = package.Workbook.Worksheets[0];

        //var data = ParseSheet(sheet);
        //return new ParsedResult();
        throw new NotImplementedException();
    }
}
