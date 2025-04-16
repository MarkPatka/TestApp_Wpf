using FluentValidation;
using TestApp_Wpf.Models.DomainModels;

namespace TestApp_Wpf.Services.Validation.Validators;

public class TestObjectValidator : AbstractValidator<TestObject>
{
    public TestObjectValidator()
    {
        RuleFor(x => x.Name).NotEmpty()
            .WithMessage("Object has to be named");

        RuleFor(x => x.Distance).InclusiveBetween(0, 20)
            .WithMessage("Distance cannot be greater than 20 and less then 0");

        RuleFor(x => x.Angle).InclusiveBetween(0, 12)
            .WithMessage("Angle cannot be greater than 12 and less then 0");

        RuleFor(x => x.Width).InclusiveBetween(0, 20)
            .WithMessage("Width cannot be greater than 20 and less then 0");

        RuleFor(x => x.Height).InclusiveBetween(0, 12)
            .WithMessage("Height cannot be greater than 12 and less then 0");
    }
}
