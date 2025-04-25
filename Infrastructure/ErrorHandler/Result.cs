using TestApp_Wpf.Infrastructure.ErrorHandler.Abstract;
using TestApp_Wpf.Models.Common.Error;

namespace TestApp_Wpf.Infrastructure.ErrorHandler;

public readonly record struct Result<TValue> : IError<TValue>
{
    private readonly TValue? _value = default;
    private readonly ParsingError? _error = null;

    public Result(TValue value) : this()
    {
        _value = value;
        IsError = false;
    }
    public Result(ParsingError error) : this()
    {
        _error = error;
        IsError = true;
    }

    public bool IsError { get; }
    public TValue Value => _value!;
    public ParsingError? Error => IsError ? _error : null;


    public static implicit operator Result<TValue>(TValue value)
    {
        return new Result<TValue>(value);
    }
    public static implicit operator Result<TValue>(ParsingError error)
    {
        return new Result<TValue>(error);
    }


}
