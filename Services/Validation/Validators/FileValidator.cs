using FluentValidation;
using FluentValidation.Validators;
using System.IO;
using System.IO.Compression;
using TestApp_Wpf.Infrastructure.Helpers;
using TestApp_Wpf.Models.ParsedModels;

namespace TestApp_Wpf.Services.Validation.Validators;

public class FileValidator : AbstractValidator<ParsedFileResult>
{
    public FileValidator()
    {
        RuleFor(x => x.Length).LessThan(100_000);
        RuleFor(x => x.FileName).NotEmpty();
        RuleFor(x => x.Extension).NotEmpty();
        RuleFor(x => x.FullPath).NotEmpty()
            .SetValidator(new MimeTypeValidator<System.Net.Mime.ContentType>());
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
                
                string mimeMessageByExtension = MimeTypes
                    .GetMimeType(Path.GetFileName(fullpath));

                if (SupportedMimeTypes.IsTextType(extension))
                {
                    return SupportedMimeTypes
                        .Contains(extension, mimeMessageByExtension);
                }

                string mimeMessageByContent = DetectMimeTypeByContent(fullpath);

                bool messagesAreEqual = string.Equals(
                    mimeMessageByExtension,
                    mimeMessageByContent,
                    StringComparison.OrdinalIgnoreCase);

                bool mimeTypeIsAllowed = SupportedMimeTypes
                    .Contains(extension, mimeMessageByContent);

                return messagesAreEqual && mimeTypeIsAllowed;
            }
            catch { return false; }
        }



        static string DetectMimeTypeByContent(string filePath)
        {
            try
            {
                byte[] buffer = new byte[256];

                using FileStream stream = new(filePath, FileMode.Open, FileAccess.Read);
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                if (bytesRead == 0)
                    throw new InvalidOperationException("File is empty");

                return GetMimeTypeFromSignature(buffer, filePath);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to detect MIME type", ex);
            }
        }
        static string GetMimeTypeFromSignature(byte[] buffer, string filePath)
        {
            string hexString = Convert.ToHexString(buffer);

            foreach (var mimeCode in SupportedMimeTypes.MimeCodes)
            {
                if (hexString.StartsWith(
                    mimeCode.Key, StringComparison.OrdinalIgnoreCase))
                {
                    return DetectZipBasedMimeType(filePath) ?? "application/octet-stream";
                }
            }
            return "application/octet-stream";
        }

        static string? DetectZipBasedMimeType(string filePath)
        {
            using ZipArchive archive = ZipFile.OpenRead(filePath);
            if (archive.GetEntry("[Content_Types].xml") != null)
                return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            return null;
        }

    }
}
    
