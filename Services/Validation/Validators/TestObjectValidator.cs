using FluentValidation;
using TestApp_Wpf.Models.DomainModels;

namespace TestApp_Wpf.Services.Validation.Validators;

public class TestObjectValidator : AbstractValidator<TestObject>
{
    public TestObjectValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Object has to be named"); ;
        RuleFor(x => x.Data.Distance).InclusiveBetween(0, 20);
        RuleFor(x => x.Data.Angle).InclusiveBetween(0, 12);
        RuleFor(x => x.Data.Width).InclusiveBetween(0, 20);
        RuleFor(x => x.Data.Height).InclusiveBetween(0, 12);
    }
}
