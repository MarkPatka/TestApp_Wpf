using FluentValidation;
using Microsoft.Win32;
using System.IO;
using TestApp_Wpf.Infrastructure.Commands.Abstract;
using TestApp_Wpf.Models.ParsedModels;
using TestApp_Wpf.Services.FileDialog.Interfaces;
using TestApp_Wpf.Services.Validation.Interfaces;

namespace TestApp_Wpf.Infrastructure.Commands.MainViewModel.Commands;

public class LoadFilesCommand : BaseCommand
{
    private readonly IValidationService _fileValidator;
    private readonly IFileDialogService _dialogService;

    public LoadFilesCommand(
        IValidationService fileValidator,                           
        IFileDialogService dialogService)
    { 
        _fileValidator = fileValidator;
        _dialogService = dialogService;
    }

    protected override async Task OnExecuteAsync(object? parameter)
    {
        // 1. Get all files (only pathes)
        var files = _dialogService.GetFiles();

        // 2. Validate files
        await _fileValidator.ValidateAsync(files);

        await Task.CompletedTask;
    }
}