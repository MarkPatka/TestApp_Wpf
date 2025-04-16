using TestApp_Wpf.Services.Parsing.Interfaces;

namespace TestApp_Wpf.Models.ParsedModels;

public record ParsedFileResult(
    string FileName, 
    string FullPath,
    string Extension,
    double Length) // KB 
    : IParsingFile;


