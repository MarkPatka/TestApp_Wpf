using FluentValidation.Results;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TestApp_Wpf.Infrastructure.Commands.Abstract;
using TestApp_Wpf.Infrastructure.Extensions;
using TestApp_Wpf.Models.Common.Abstract;
using TestApp_Wpf.Models.Common.Enumerations;
using TestApp_Wpf.Models.DomainModels;
using TestApp_Wpf.Models.ParsedModels;
using TestApp_Wpf.Services.FileDialog.Interfaces;
using TestApp_Wpf.Services.Parsing.Interfaces;
using TestApp_Wpf.Services.Validation.Interfaces;

namespace TestApp_Wpf.Infrastructure.Commands.MainViewModel.Commands;

public class LoadTestObjectsCommand : BaseCommand
{
    private readonly IFileDialogService _dialogService;
    private readonly IValidationService _validator;
    private readonly IParsingService    _fileParser;
    private CommandResult<IEnumerable<TestObject>> _result = new();

    public CommandResult<IEnumerable<TestObject>> Result  => _result;

    public LoadTestObjectsCommand(
        IValidationService fileValidator,                           
        IFileDialogService dialogService,
        IParsingService parsingService) 
    { 
        _validator = fileValidator;
        _dialogService = dialogService;
        _fileParser = parsingService;
    }

    public async Task ExecuteAsync()
    {
        try
        {
            // Get file
            var file = _dialogService.GetFile()
                .MapParsedFile<ParsedFileResult>();

            // Validate file
            ValidationResult fileValidation = await _validator
                .ValidateAsync<ParsedFileResult>(file);

            if (fileValidation.IsValid)
            {
                List<IDomainModel> results = await _fileParser
                    .ParseFileAsync(file);

                // Parse valid model
                var templateModel = results.FirstOrDefault();
                if (templateModel is TestObject)
                {
                    var models = await _validator
                        .ValidateDomainModels<TestObject>(results);

                    InitResult(models, CommandStatus.SUCCESS);
                    return;
                }
                else InitResult(
                    value: [],
                    status: CommandStatus.ERROR,
                    error: results.Count == 0
                           ? new Exception($"The provided file doesn`t contain models of type {typeof(TestObject).Name}")
                           : new InvalidCastException($"{templateModel!.GetType().Name} is not a valid {typeof(TestObject).Name} type"));
            }
            else InitResult(
                    value:  [], 
                    status: CommandStatus.ERROR, 
                    error:  new Exception(fileValidation.Errors.FirstOrDefault()!.ErrorMessage));
        }
        catch (Exception ex) 
        { 
            InitResult([], CommandStatus.ERROR, ex);
        }
    }

    private void InitResult(
        IEnumerable<TestObject> value, 
        CommandStatus status, 
        Exception? error = null)
    {
        _result.Value = value;
        _result.Error = error;
        _result.Status = status;

        OnPropertyChanged("Result");
    }
}