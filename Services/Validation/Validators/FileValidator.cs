using FluentValidation;
using MimeKit;
using TestApp_Wpf.Infrastructure.Helpers;
using TestApp_Wpf.Models.DomainModels;
using TestApp_Wpf.Models.ParsedModels;

namespace TestApp_Wpf.Services.Validation.Validators;

public class FileValidator : AbstractValidator<ParsedFileResult>
{
    public FileValidator()
    {
        //RuleFor(x => x.ContentType).SetValidator(x => new MimeTypeValidator<ContentType>(x.Path));
        RuleFor(x => x.Length).LessThan(1000);
        RuleFor(x => x.FileName).NotEmpty();
        RuleFor(x => x.Path).NotEmpty();
    }


}
