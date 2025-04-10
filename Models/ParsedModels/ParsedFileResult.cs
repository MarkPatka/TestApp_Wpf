using MimeKit;
using System.IO;
using TestApp_Wpf.Models.DomainModels;
using TestApp_Wpf.Services.Parsing.Interfaces;

namespace TestApp_Wpf.Models.ParsedModels;

public record ParsedFileResult(
    ContentType ContentType, 
    string FileName, 
    string Path, 
    long Length) 
    : IParsingFile;


