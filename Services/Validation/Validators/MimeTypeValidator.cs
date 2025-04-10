using System.IO;
using MimeKit;
using HeyRed.Mime;
using TestApp_Wpf.Infrastructure.Helpers;
using FluentValidation;
using FluentValidation.Validators;

namespace TestApp_Wpf.Services.Validation.Validators;

public class MimeTypeValidator<T> : PropertyValidator<T, string>
{
    private readonly string _filePath;  
    
    public MimeTypeValidator(string filePath) =>
        _filePath = filePath;

    public override string Name => nameof(this.Name);

    public override bool IsValid(ValidationContext<T> context, string path)
    {
        try
        {
            string extension = Path.GetExtension(path);

            string mimeMessageByExtension = MimeTypesMap.GetMimeType(path);
            string mimeMessageByContent = DetectMimeTypeByContent(path);

            bool messagesAreEqual = string.Equals(
                mimeMessageByExtension,
                mimeMessageByContent,
                StringComparison.OrdinalIgnoreCase);

            bool mimeTypeIsAllowed = AllowedMimeTypes
                .Has(extension, mimeMessageByContent);

            return messagesAreEqual && mimeTypeIsAllowed;
        }
        catch { return false; }
    }


    static string DetectMimeTypeByContent(string _filePath)
    {
        try
        {
            byte[] buffer = new byte[256];

            using FileStream stream = new(_filePath, FileMode.Open, FileAccess.Read);
            var mimeParser = new MimeParser(stream, MimeFormat.Entity);
            var message = mimeParser.ParseMessage();

            return message.Body.ContentType.MediaType + "/" +
                   message.Body.ContentType.MediaSubtype;
        }
        catch
        {
            throw new ValidationException(
                "Exception was thrown while MIME type validaton by file content");
        }
    }
}
