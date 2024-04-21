using FluentValidation;

namespace NOAM_ASISTENCIA_v3.Client.Helpers.MudBlazor;

/// <summary>
/// Allows to create fluent validation rules for primitive type values
///
/// Usage:
/// var validator = new FluentValueValidator<string>(x => x.NotEmpty().CreditCard());
/// var validationFunc = validator.Validation;
/// Pass the validationFunc to a MudTextfield's Validation property
/// </summary>
/// <typeparam name="T"></typeparam>
public class MudBlazorValidator<T> : AbstractValidator<T>
{
    //public MudBlazorValidator(Action<IRuleBuilderInitial<T, T>> ruleBuilder)
    //{
    //    ruleBuilder(RuleFor(rules => rules));
    //}

    private IEnumerable<string> ValidateValue(T model)
    {
        var result = Validate(model);

        if (result.IsValid) { return []; }

        return result.Errors.Select(error => error.ErrorMessage);
    }

    public Func<T, IEnumerable<string>> Validation => ValidateValue;
}
