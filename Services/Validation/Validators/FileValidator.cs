using FluentValidation;
using FluentValidation.Validators;
using HeyRed.Mime;
using MimeKit;
using System.IO;
using TestApp_Wpf.Infrastructure.Helpers;
using TestApp_Wpf.Models.ParsedModels;
using ContentType = MimeKit.ContentType;

namespace TestApp_Wpf.Services.Validation.Validators;

public class FileValidator : AbstractValidator<ParsedFileResult>
{
    public FileValidator()
    {
        RuleFor(x => x.Length).LessThan(1000);
        RuleFor(x => x.FileName).NotEmpty();

        RuleFor(x => x.FullPath).NotEmpty()
            .SetValidator(new MimeTypeValidator<ContentType>());
    }



    class MimeTypeValidator<T> 
        : PropertyValidator<ParsedFileResult, string>
    {
        public override string Name => nameof(this.Name);

        public override bool IsValid(
            ValidationContext<ParsedFileResult> context, string fullpath)
        {
            try
            {
                if (!File.Exists(fullpath)) return false;

                string extension = Path.GetExtension(fullpath);
                string mimeMessageByExtension = MimeTypesMap.GetMimeType(Path.GetFileName(fullpath));
                string mimeMessageByContent = DetectMimeTypeByContent(fullpath);

                bool messagesAreEqual = string.Equals(
                    mimeMessageByExtension,
                    mimeMessageByContent,
                    StringComparison.OrdinalIgnoreCase);

                bool mimeTypeIsAllowed = AllowedMimeTypes
                    .Contains(extension, mimeMessageByContent);

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
}
    
