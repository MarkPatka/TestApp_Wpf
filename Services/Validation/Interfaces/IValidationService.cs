using FluentValidation.Results;
using TestApp_Wpf.Models.Common.Abstract;

namespace TestApp_Wpf.Services.Validation.Interfaces;

public interface IValidationService
{
    public Task<ValidationResult> ValidateAsync<T>(T obj);

    public Task<List<T>> ValidateDomainModels<T>(
        IEnumerable<IDomainModel> models);
}
