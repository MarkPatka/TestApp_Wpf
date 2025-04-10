using Microsoft.Win32;
using System.IO;
using TestApp_Wpf.Services.FileDialog.Interfaces;

namespace TestApp_Wpf.Services.FileDialog;

public class FileDialogService : IFileDialogService
{
    public OpenFileDialog GetFileDialog()
    {
        return new()
        {
            Multiselect = true,
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
}
