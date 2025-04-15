using TestApp_Wpf.Infrastructure.Commands.MainViewModel.Commands;
using System.Windows.Controls;
using System.Windows.Input;
using TestApp_Wpf.Models.DomainModels;
using TestApp_Wpf.Infrastructure.Commands.Abstract;
using TestApp_Wpf.ViewModels.Interfaces;

namespace TestApp_Wpf.ViewModels.Implementations;

public class MainViewModel 
    : ViewModelBase, IMainViewModel
{
    private const string TITLE = "MainView";
    private readonly ICommandFactory _commandFactory;

    public ICommand LoadFilesCommand { get; }

    public string Title => TITLE;


    public MainViewModel(ICommandFactory commandFactory)
    {
        _commandFactory = commandFactory;
        LoadFilesCommand = _commandFactory
            .CreateCommand<LoadFilesCommand>(); ;
    }
}
