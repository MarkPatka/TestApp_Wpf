using System.IO;

namespace TestApp_Wpf.Services.Uploading.Interfaces;

public interface IFileUploaderService
{
    public Stream UploadFile(string filePath);
    public Task<Stream> UploadFileAsync(string filePath);
}
