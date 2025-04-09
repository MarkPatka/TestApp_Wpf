using Microsoft.Win32;
using System.IO;

namespace TestApp_Wpf.Infrastructure.Commands.MainViewModel.Commands;

public class LoadFilesCommand : BaseCommand
{
    private IReadOnlyList<string> _files = [];

    protected override Task OnExecuteAsync(object? parameter)
    {
        OpenFileDialog openFileDialog = GetFileDialog();

        List<string> files = [];
        if (openFileDialog.ShowDialog() == true)
        {
            foreach (string filename in openFileDialog.FileNames)
            {
                files.Add(Path.GetFileName(filename));
            }
        }

        _files = files;
        return Task.CompletedTask;
    }

    private static OpenFileDialog GetFileDialog()
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

    public IReadOnlyList<string> Files => _files;
}