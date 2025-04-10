using Microsoft.Win32;

namespace TestApp_Wpf.Services.FileDialog.Interfaces;

public interface IFileDialogService
{
    public IReadOnlyList<string> GetFiles();
    public OpenFileDialog GetFileDialog();
}
