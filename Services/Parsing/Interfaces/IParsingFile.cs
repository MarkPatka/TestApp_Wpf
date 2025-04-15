using MimeKit;
using System.IO;

namespace TestApp_Wpf.Services.Parsing.Interfaces;

public interface IParsingFile
{ 
    public string FileName { get; }
    
    /// <summary>
    /// Full path to file
    /// </summary>
    public string FullPath { get; }
    
    /// <summary>
    /// File size in MB
    /// </summary>
    public double Length { get; }          
    
    //public Stream OpenReadStream();            
    //public void CopyTo(Stream target);         
    //public Task CopyToAsync(Stream target, CancellationToken cancellationToken = default);
}
