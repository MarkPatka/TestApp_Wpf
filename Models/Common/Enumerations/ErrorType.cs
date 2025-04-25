using TestApp_Wpf.Models.Common.Abstract;

namespace TestApp_Wpf.Models.Common.Enumerations;

public sealed class ErrorType(int id, string name, string? description = null)
    : Enumeration(id, name, description)
{
    public static readonly ErrorType CONVERTION_ERROR = new(1, nameof(CONVERTION_ERROR), "");
}
