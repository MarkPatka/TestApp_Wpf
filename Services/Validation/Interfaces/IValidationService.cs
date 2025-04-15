using FluentValidation.Results;

namespace TestApp_Wpf.Services.Validation.Interfaces;

public interface IValidationService
{
    public Task<ValidationResult> ValidateAsync<T>(T obj);
}
