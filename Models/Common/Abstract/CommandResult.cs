using TestApp_Wpf.Models.Common.Enumerations;
using TestApp_Wpf.Models.Common.Error;

namespace TestApp_Wpf.Models.Common.Abstract;

public record struct CommandResult<TValue>
{
    public TValue? Value { get; set; } = default;
    public CommandStatus Status { get; set; } = CommandStatus.DEFAULT;
    public Exception? Error { get; set; } = default;

    public CommandResult(TValue value) : this()
    {
        Value = value;
        Status = CommandStatus.SUCCESS;
    }
    public CommandResult(Exception error) : this()
    {
        Error = error;
        Status = CommandStatus.ERROR;
    }


}
