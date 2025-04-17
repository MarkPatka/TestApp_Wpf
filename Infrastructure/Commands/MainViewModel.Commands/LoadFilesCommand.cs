using FluentValidation.Results;
using TestApp_Wpf.Infrastructure.Commands.Abstract;
using TestApp_Wpf.Infrastructure.Extensions;
using TestApp_Wpf.Models.ParsedModels;
using TestApp_Wpf.Services.FileDialog.Interfaces;
using TestApp_Wpf.Services.Parsing.Interfaces;
using TestApp_Wpf.Services.Validation.Interfaces;

namespace TestApp_Wpf.Infrastructure.Commands.MainViewModel.Commands;

public class LoadFilesCommand : BaseCommand
{
    private readonly IFileDialogService _dialogService;
    private readonly IValidationService _fileValidator;
    private readonly IParsingService    _fileParser;

    public LoadFilesCommand(
        IValidationService fileValidator,                           
        IFileDialogService dialogService,
        IParsingService parsingService)
    { 
        _fileValidator = fileValidator;
        _dialogService = dialogService;
        _fileParser = parsingService;
    }

    protected override async Task OnExecuteAsync(object? parameter)
    {
        // Get all files (only pathes)
        var files = _dialogService.GetFiles()
            .MapParsedFile<ParsedFileResult>()
            ;

        // Validate files
        List<ParsedFileResult> validParsedFiles = [];

        var iterator = files.GetEnumerator();
        while (iterator.MoveNext())
        {
            ValidationResult validationResult = await _fileValidator
                .ValidateAsync<ParsedFileResult>(iterator.Current);

            if (validationResult.IsValid) 
            {
                validParsedFiles.Add(iterator.Current);
            }
        }

        // Parse valid files
        foreach (var file in validParsedFiles) 
        {
            await _fileParser.ParseFileAsync(file);

        }



        await Task.CompletedTask;
    }
}