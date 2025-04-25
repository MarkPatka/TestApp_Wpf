using TestApp_Wpf.Infrastructure.Commands.MainViewModel.Commands;
using System.Windows.Controls;
using System.Windows.Input;
using TestApp_Wpf.ViewModels.Interfaces;
using TestApp_Wpf.Models.DomainModels;
using TestApp_Wpf.Infrastructure.Factories.Abstract;
using TestApp_Wpf.Infrastructure.Factories;
using TestApp_Wpf.Infrastructure.Commands.Abstract;
using TestApp_Wpf.Models.Common.Abstract;
using System.Data.Common;
using System.Collections.ObjectModel;

namespace TestApp_Wpf.ViewModels.Implementations;

public class MainViewModel
    : ViewModelBase, IMainViewModel, IDisposable
{
    private const string TITLE = "TestApp_MainView";

    private readonly ICommandFactory _commandFactory;
    private readonly IScopedCommandFactory _scopedCommandFactory;
    private readonly List<BaseCommand> _disposableCommands = [];
    private List<TestObject> _testObjects = [];
    private ICommand _loadTestObjects;


    public MainViewModel(
        ICommandFactory commandFactory, 
        IScopedCommandFactory scopedCommandFactory)
    {
        _commandFactory = commandFactory;
        _scopedCommandFactory = scopedCommandFactory;
        _loadTestObjects = _commandFactory
            .GetCommand<LoadTestObjectsCommand>();


    }
    public string Title => TITLE;
    public List<TestObject> TestObjects 
    { 
        get => _testObjects;
        set
        {
            Set(ref _testObjects, value);
            OnPropertyChanged();
        } 
    }


    private async Task LoadDataAsync()
    {
        if (_loadTestObjects is LoadTestObjectsCommand loadCommand)
        {
            var command = _scopedCommandFactory
                .CreateAsyncCommand(loadCommand.ExecuteAsync);

            var load = Task.Run(() => command.Execute(this));
            await load;
            if (load.Status == TaskStatus.RanToCompletion) 
            {
                TestObjects = [.. loadCommand.Result.Value ?? []];
            }
        }
    }



    public ICommand LoadTestObjects => _scopedCommandFactory
        .CreateAsyncCommand(LoadDataAsync);

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}
