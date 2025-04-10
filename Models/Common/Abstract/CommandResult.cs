using TestApp_Wpf.Models.Common.Enumerations;

namespace TestApp_Wpf.Models.Common.Abstract;

public class CommandResult<T>
{
    public T? Value { get; }
    public CommandStatus Status { get; } = CommandStatus.DEFAULT;
    public List<Exception> Errors { get; } = [];
}
