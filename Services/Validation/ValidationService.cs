using FluentValidation;
using FluentValidation.Results;
using TestApp_Wpf.Models.Common.Abstract;
using TestApp_Wpf.Services.Validation.Interfaces;

namespace TestApp_Wpf.Services.Validation;

public class ValidationService : IValidationService
{
    private readonly IServiceProvider _validators;

    public ValidationService(IServiceProvider validators) =>
        _validators = validators;

    public async Task<ValidationResult> ValidateAsync<T>(T obj)
    {
        var validator = _validators.GetService(typeof(IValidator<T>)) as IValidator<T>
            ?? throw new InvalidOperationException(
                $"There is no registered validator found for type {typeof(T).Name}");


        ValidationResult result = await validator.ValidateAsync(obj);
        return result;        
    }

    public async Task<List<T>> ValidateDomainModels<T>(
        IEnumerable<IDomainModel> models)
    {
        List<T> validParsedModels = [];
        var iterator = models.GetEnumerator();
        while (iterator.MoveNext())
        {
            if (iterator.Current is T model)
            {
                var modelValidation = await ValidateAsync(model);

                if (modelValidation.IsValid)
                    validParsedModels.Add(model);
            }
        }
        return validParsedModels;
    }

}
