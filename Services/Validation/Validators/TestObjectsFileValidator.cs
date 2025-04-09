using FluentValidation;
using TestApp_Wpf.Infrastructure.Helpers;
using TestApp_Wpf.Services.Parsing.ParsedModels;

namespace TestApp_Wpf.Services.Validation.Validators;

public class TestObjectsFileValidator 
    : AbstractValidator<TestObjectsParsedResult>
{
    public TestObjectsFileValidator()
    {
        RuleFor(x => x.ContentType).Must(
            AllowedForParsingFileFormatsExtensions.Contains);

        RuleFor(x => x.Length).LessThan(100);
        RuleFor(x => x.FileName).NotEmpty();
        RuleFor(x => x.Path).NotEmpty();
    }


}
