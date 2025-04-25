namespace TestApp_Wpf.Infrastructure.ErrorHandler.Abstract;

public interface IError<out TValue>
{
    TValue Value { get; }

}
