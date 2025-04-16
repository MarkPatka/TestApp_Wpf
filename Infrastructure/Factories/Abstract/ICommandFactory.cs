using TestApp_Wpf.Infrastructure.Commands.Abstract;

namespace TestApp_Wpf.Infrastructure.Factories.Abstract;

public interface ICommandFactory
{
    T CreateCommand<T>() where T : BaseCommand;
}
