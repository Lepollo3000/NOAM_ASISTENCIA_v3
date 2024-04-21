using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

namespace NOAM_ASISTENCIA_v3.Shared.Helpers.Extensions;

public static class DisplayExtensions
{
    public static string GetDisplayName<TModel, TProperty>(this TModel model, Expression<Func<TModel, TProperty>> expression)
    {
        DisplayAttribute? attribute = model.GetDisplayAttribute(expression);

        return attribute?.Name ?? string.Empty;
    }

    public static string GetDisplayDescription<TModel, TProperty>(this TModel model, Expression<Func<TModel, TProperty>> expression)
    {
        DisplayAttribute? attribute = model.GetDisplayAttribute(expression);

        return attribute?.Description ?? string.Empty;
    }

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
            MetadataTypeAttribute? metadataType = type
                .GetCustomAttributes(typeof(MetadataTypeAttribute), true)
                .FirstOrDefault() as MetadataTypeAttribute;

            if (metadataType != null)
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
