﻿using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
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
        services.AddValidatorsFromAssembly(
            Assembly.GetExecutingAssembly());

        services
            .AddScoped<IValidationService, ValidationService>()
            .AddScoped<IFileParser, CsvParser>()
            .AddScoped<IFileParser, XlsParser>()
            .AddScoped<IFileParser, JsonParser>()
            .AddScoped<IParsingService, ParsingService>()
            ;

        return services;
    }
}
