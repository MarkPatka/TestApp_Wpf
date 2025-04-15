using HeyRed.Mime;
using MimeKit;
using System.IO;
using System.Windows.Shapes;
using TestApp_Wpf.Services.Parsing.Interfaces;
using TestApp_Wpf.Services.Uploading.Interfaces;

namespace TestApp_Wpf.Services.Uploading;

public class FileUploaderService : IFileUploaderService
{
    public IParsingFile ReadFileMetadata(string filePath)
    {

        throw new NotImplementedException();
    }

    public Stream UploadFile(string filePath)
    {
        throw new NotImplementedException();
    }

    public Task<Stream> UploadFileAsync(string filePath)
    {
        throw new NotImplementedException();
    }
}
