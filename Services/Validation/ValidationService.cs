using FluentValidation;
using FluentValidation.Results;
using TestApp_Wpf.Services.Validation.Interfaces;

namespace TestApp_Wpf.Services.Validation;

public class ValidationService : IValidationService
{
    private readonly IEnumerable<IValidator> _validators;

    public ValidationService(IEnumerable<IValidator> validators) =>
        _validators = validators;

    public async Task<T> ValidateAsync<T>(T obj)
    {
        var validator = _validators
            .OfType<IValidator<T>>()
            .FirstOrDefault() 
            ?? throw new InvalidOperationException(
                $"No validator found for type {typeof(T).Name}");

        ValidationResult result = await validator.ValidateAsync(obj);

        if (!result.IsValid)
            throw new ValidationException(result.Errors);

        return obj;
    } 

}
