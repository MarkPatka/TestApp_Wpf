using Microsoft.Win32;

namespace TestApp_Wpf.Services.FileDialog.Interfaces;

public interface IFileDialogService
{
    public IReadOnlyList<string> GetFiles();
    public string GetFile();
    public OpenFileDialog GetFileDialog(bool allowMultiselect = true);
}
