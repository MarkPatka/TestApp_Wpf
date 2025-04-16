using System.Collections.Frozen;
using System.Reflection;
using System.Runtime.InteropServices;
using TestApp_Wpf.Models.Common.Abstract;

namespace TestApp_Wpf.Infrastructure.Helpers;

public class SupportedParsingTypes
{
    private static readonly FrozenDictionary<Type, string[]> _supportedTypes;

    static SupportedParsingTypes()
    {
        var domainModels = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(t => typeof(IDomainModel)
            .IsAssignableFrom(t)
            && !t.IsAbstract
            && !t.IsInterface);

        Dictionary<Type, string[]> supportedTypes = [];
        foreach (var domainModel in domainModels) 
        {
            var headers = domainModel
                .GetProperties(BindingFlags.Public)
                .Select(p => p.Name)
                .ToArray();

            supportedTypes.TryAdd(domainModel, headers);
        }
        _supportedTypes = supportedTypes.ToFrozenDictionary();
    }

    
}

