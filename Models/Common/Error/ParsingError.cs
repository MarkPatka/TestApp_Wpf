using TestApp_Wpf.Models.Common.Enumerations;

namespace TestApp_Wpf.Models.Common.Error;

public readonly record struct ParsingError
{
    private static string message = "Parsing error: ";

    public ParsingError(
        ErrorType errorType, 
        DateTime dateTime, 
        string message)
    {
        ErrorType = errorType;
        DateTime = dateTime;
        Message = message;
    }

    public ErrorType ErrorType { get; }
    public DateTime DateTime { get; }
    public string Message 
    {
        get => message;
        private set => message += (value + $"{ErrorType.Description}");
    }
}
