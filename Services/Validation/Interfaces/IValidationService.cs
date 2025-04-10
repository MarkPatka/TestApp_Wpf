namespace TestApp_Wpf.Services.Validation.Interfaces;

public interface IValidationService
{
    public Task<T> ValidateAsync<T>(T obj);
}
