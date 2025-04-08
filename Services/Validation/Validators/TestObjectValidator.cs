using FluentValidation;
using TestApp_Wpf.Models.DomainModels;
using TestApp_Wpf.Services.Validation.Interfaces;

namespace TestApp_Wpf.Services.Validation.Validators;

public class TestObjectValidator 
    : AbstractValidator<TestObject>, ITestObjectValidator
{

}
