using Microsoft.Win32;
using System.IO;
using TestApp_Wpf.Services.FileDialog.Interfaces;

namespace TestApp_Wpf.Services.FileDialog;

public class FileDialogService : IFileDialogService
{
    public OpenFileDialog GetFileDialog(bool allowMultiselect = true)
    {
        return new()
        {
            Multiselect = allowMultiselect,
            Filter =
                "Excel Files (*.xlsx)|*.xlsx|" +
                "CSV Files (*.csv)|*.csv|" +
                "Text Files (*.txt)|*.txt|" +
                "JSON Files (*.json)|*.json|" +
                "All Files (*.*)|*.*",

            InitialDirectory = Environment.GetFolderPath(
                Environment.SpecialFolder.MyDocuments)
        };
    }

    public IReadOnlyList<string> GetFiles()
    {
        List<string> files = [];
        OpenFileDialog openFileDialog = GetFileDialog();

        if (openFileDialog.ShowDialog() == true)
        {
            files.AddRange(
                openFileDialog.FileNames.Select(Path.GetFullPath));
        }
        return files.AsReadOnly();
    }
    public string GetFile()
    {
        string file = string.Empty;
        OpenFileDialog openFileDialog = GetFileDialog(false);

        if (openFileDialog.ShowDialog() == true)
        {
            file = openFileDialog.FileName;
        }
        return file;
    }
}
