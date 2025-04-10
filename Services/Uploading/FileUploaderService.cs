using System.IO;
using TestApp_Wpf.Services.Uploading.Interfaces;

namespace TestApp_Wpf.Services.Uploading;

public class FileUploaderService : IFileUploaderService
{
    public Stream UploadFile(string filePath)
    {
        throw new NotImplementedException();
    }

    public Task<Stream> UploadFileAsync(string filePath)
    {
        throw new NotImplementedException();
    }
}
