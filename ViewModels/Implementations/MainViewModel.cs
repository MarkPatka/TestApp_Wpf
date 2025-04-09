using TestApp_Wpf.Infrastructure.Commands.MainViewModel.Commands;
using System.Windows.Controls;
using System.Windows.Input;
using TestApp_Wpf.ViewModels.Interfaces;
using TestApp_Wpf.Models.DomainModels;

namespace TestApp_Wpf.ViewModels.Implementations;

public class MainViewModel 
    : ViewModelBase, IMainViewModel
{
    private const string TITLE = "TestApp_MainView";
    private readonly LoadFilesCommand _loadFilesCommand;
    public ICommand LoadTestObjectFilesCommand => LoadFiles<TestObject>();

    public string Title => TITLE;


    public MainViewModel()
    {
        _loadFilesCommand = new LoadFilesCommand();
    }


    private LoadFilesCommand LoadFiles<T>()
    {
        _loadFilesCommand.Execute(typeof(T));
        return _loadFilesCommand;
    }
}
