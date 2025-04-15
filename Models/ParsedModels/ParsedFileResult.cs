using MimeKit;
using TestApp_Wpf.Services.Parsing.Interfaces;

namespace TestApp_Wpf.Models.ParsedModels;

public record ParsedFileResult(
    string FileName, 
    string FullPath, 
    double Length) // KB 
    : IParsingFile;


