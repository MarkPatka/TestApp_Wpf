namespace TestApp_Wpf.Infrastructure.Commands.Abstract;

public interface ICommandFactory
{
    T CreateCommand<T>() where T : BaseCommand;
}
