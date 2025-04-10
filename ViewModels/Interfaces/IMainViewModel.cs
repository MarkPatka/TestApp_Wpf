using System.Windows.Input;

namespace TestApp_Wpf.ViewModels.Interfaces;

public interface IMainViewModel
{
    public string Title { get; }
    public ICommand LoadFilesCommand { get; }
}
