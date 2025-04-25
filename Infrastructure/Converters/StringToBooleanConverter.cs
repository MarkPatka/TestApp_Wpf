using System.Globalization;
using System.Windows.Data;
using System.Windows.Navigation;
using TestApp_Wpf.Infrastructure.ErrorHandler;
using TestApp_Wpf.Models.Common.Enumerations;
using TestApp_Wpf.Models.Common.Error;

namespace TestApp_Wpf.Infrastructure.Converters;

public class StringToBooleanConverter
{
    private readonly Dictionary<string, bool> _stringBoolValues = new()
    {
        { "true", true },
        { "false", false },
        { "Да", true },
        { "Нет", false },
        { "да", true },
        { "нет", false },
        { "yes", true },
        { "no", false },
        { "Yes", true },
        { "No", false },
    };


    public Result<bool> Convert(object strValue, Type targetType)
    {

        if (ReferenceEquals(targetType, typeof(bool)))
        {
            string? val = strValue?.ToString() ?? null;
            
            if (string.IsNullOrEmpty(val)) return new ParsingError(
                ErrorType.CONVERTION_ERROR, 
                DateTime.Now, $"NULL cannot be converted to boolean");

            if (_stringBoolValues.TryGetValue(val, out bool value))
            {
                return value;
            }
            else 
                return new ParsingError(
                    ErrorType.CONVERTION_ERROR, 
                    DateTime.Now, $"value \"{strValue}\" cannot be converterted to boolean");
        }
        return new ParsingError(
                    ErrorType.CONVERTION_ERROR, 
                    DateTime.Now, "Target type must be boolean");
    }

    public Result<string> ConvertBack(object boolValue, Type targetType)
    {
        if (ReferenceEquals(targetType, typeof(string)))
        {
            try
            {
                bool val = System.Convert.ToBoolean(boolValue);
                return val ? "true" : "false";

            }
            catch
            {
                throw new InvalidCastException(
                    $"Value \"{boolValue}\" is not convertable in the bool format");
            }
        }
        throw new Exception("targetType should be string");


    }
}
