using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace NOAM_ASISTENCIA_v3.Shared.Helpers.Extensions;

public static class EnumExtensions
{
    /// <summary>
    /// Convierte un valor entero a su correspondiente enum, si es que existe
    /// </summary>
    /// <returns></returns>
    public static bool TryConvertToEnum<T>(this int value, out T? result) where T : Enum
    {
        result = default;

        try
        {
            bool valueExists = Enum.IsDefined(typeof(T), value);

            if (valueExists)
            {
                result = (T)Enum.ToObject(typeof(T), value);
            }

            return valueExists;
        }
        catch
        {

            return false;
        }
    }

    /// <summary>
    /// Obtiene el nombre del atributo [Display] adjuntado
    /// </summary>
    /// <returns></returns>
    public static string GetDisplayName(this Enum enumValue)
    {
        return enumValue.GetType()
            .GetMember(enumValue.ToString()).First()
            .GetCustomAttribute<DisplayAttribute>()?
            .Name ?? enumValue.ToString();
    }

    /// <summary>
    /// Obtiene la descripción del atributo [Display] adjuntado
    /// </summary>
    /// <returns></returns>
    public static string GetDisplayDescription(this Enum enumValue)
    {
        return enumValue.GetType()
            .GetMember(enumValue.ToString()).First()
            .GetCustomAttribute<DisplayAttribute>()?
            .Description ?? enumValue.ToString();
    }
}
