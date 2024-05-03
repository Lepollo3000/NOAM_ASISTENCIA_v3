using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

namespace NOAM_ASISTENCIA_v3.Shared.Helpers.Extensions;

public static class DisplayExtensions
{
    /// <summary>
    /// Obtiene el nombre del atributo [Display] adjuntado a una propiedad dada.
    /// </summary>
    /// <returns></returns>
    public static string GetDisplayName<TModel, TProperty>(this TModel model, Expression<Func<TModel, TProperty>> expression)
    {
        DisplayAttribute? attribute = model.GetDisplayAttribute(expression);

        return attribute?.Name ?? string.Empty;
    }

    /// <summary>
    /// Obtiene la descripción del atributo [Display] adjuntado a una propiedad dada.
    /// </summary>
    /// <returns></returns>
    public static string GetDisplayDescription<TModel, TProperty>(this TModel model, Expression<Func<TModel, TProperty>> expression)
    {
        DisplayAttribute? attribute = model.GetDisplayAttribute(expression);

        return attribute?.Description ?? string.Empty;
    }

    /// <summary>
    /// Obtiene el atributo [Display] adjuntado a una propiedad dada
    /// </summary>
    /// <returns></returns>
    private static DisplayAttribute? GetDisplayAttribute<TModel, TProperty>(this TModel _, Expression<Func<TModel, TProperty>> expression)
    {
        Type type = typeof(TModel);

        MemberExpression memberExpression = (MemberExpression)expression.Body;
        string propertyName = (memberExpression.Member is PropertyInfo)
            ? memberExpression.Member.Name
            : null!;

        DisplayAttribute? attribute = type
            .GetProperty(propertyName)?
            .GetCustomAttributes(typeof(DisplayAttribute), true)
            .SingleOrDefault() as DisplayAttribute;

        if (attribute == null)
        {
            if (type
                .GetCustomAttributes(typeof(MetadataTypeAttribute), true)
                .FirstOrDefault() is MetadataTypeAttribute metadataType)
            {
                var property = metadataType.MetadataClassType.GetProperty(propertyName);

                if (property != null)
                {
                    attribute = property
                        .GetCustomAttributes(typeof(DisplayAttribute), true)
                        .SingleOrDefault() as DisplayAttribute;
                }
            }
        }

        return attribute;
    }
}
