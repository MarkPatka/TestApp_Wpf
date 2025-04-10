using MimeKit;
using System.IO;

namespace TestApp_Wpf.Services.Parsing.Interfaces;

public interface IParsingFile
{
    /// <summary>
    /// The file type (extension)
    /// </summary>
    public ContentType ContentType { get; }
    
    public string FileName { get; }
    
    /// <summary>
    /// Full path to file
    /// </summary>
    public string Path { get; }
    
    /// <summary>
    /// File size in MB
    /// </summary>
    public long Length { get; }          
    
    //public Stream OpenReadStream();            
    //public void CopyTo(Stream target);         
    //public Task CopyToAsync(Stream target, CancellationToken cancellationToken = default);
}
