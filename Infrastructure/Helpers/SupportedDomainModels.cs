using System.Reflection;

namespace TestApp_Wpf.Infrastructure.Helpers;

public static class SupportedDomainModels
{
    public static Type[] GetImplementationsOfInterface<T>()
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        Type interfaceType = typeof(T);

        Type[] implementingTypes = [];
        if (interfaceType.IsInterface)
        {
            implementingTypes = assembly.GetTypes()
                .Where(t => interfaceType.IsAssignableFrom(t) && !t.IsInterface)
                .ToArray();
        }
        return implementingTypes;      
    }
}
