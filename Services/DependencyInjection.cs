using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TestApp_Wpf.Models.DomainModels;
using TestApp_Wpf.Models.ParsedModels;
using TestApp_Wpf.Services.FileDialog;
using TestApp_Wpf.Services.FileDialog.Interfaces;
using TestApp_Wpf.Services.Parsing;
using TestApp_Wpf.Services.Parsing.Interfaces;
using TestApp_Wpf.Services.Parsing.Parsers;
using TestApp_Wpf.Services.Validation;
using TestApp_Wpf.Services.Validation.Interfaces;

namespace TestApp_Wpf.Services;

internal static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {

        services
            .AddValidators()
            .AddParsers();

        services
            .AddScoped<IValidationService, ValidationService>()
            .AddScoped<IParsingService, ParsingService>()
            .AddScoped<IFileDialogService, FileDialogService>()
            ;

        return services;
    }
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services
            .AddValidatorsFromAssemblyContaining<ParsedFileResult>()
            .AddValidatorsFromAssemblyContaining<TestObject>()
            ;
        
        return services;
    }
    public static IServiceCollection AddParsers(this IServiceCollection services)
    {
        services
            .AddScoped<IFileParser, CsvParser>()
            .AddScoped<IFileParser, XlsxParser>()
            .AddScoped<IFileParser, JsonParser>()
            ;

        return services;
    }
}
