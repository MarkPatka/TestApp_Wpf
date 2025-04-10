using TestApp_Wpf.Models.Common.Abstract;

namespace TestApp_Wpf.Infrastructure.Commands.Abstract;

public abstract class BaseResultCommand<TResult> : BaseCommand
{
    private CommandResult<TResult>? _result;

    public CommandResult<TResult>? Result
    {
        get => _result;
        protected set
        {
            _result = value;
            OnPropertyChanged();
        }
    }

    public async Task<CommandResult<TResult>?> ExecuteWithResultAsync(object? parameter = null)
    {
        await ExecuteAsync(parameter);
        return Result;
    }

    protected override async Task OnExecuteAsync(object? parameter = null)
    {
        Result = await OnExecuteWithResultAsync(parameter);
    }

    protected abstract Task<CommandResult<TResult>> OnExecuteWithResultAsync(object? parameter = null);
}

