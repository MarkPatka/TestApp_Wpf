using System.IO;
using TestApp_Wpf.Services.Parsing.Interfaces;

namespace TestApp_Wpf.Services.Uploading.Interfaces;

public interface IFileUploaderService
{
    public IParsingFile ReadFileMetadata(string filePath);

    public Stream UploadFile(string filePath);
    public Task<Stream> UploadFileAsync(string filePath);
}
