using FluentValidation;
using FluentValidation.Results;
using MudBlazor;
using NOAM_ASISTENCIA_v3.Client.Helpers.MudBlazor;
using NOAM_ASISTENCIA_v3.Shared.Contracts.Users;

namespace NOAM_ASISTENCIA_v3.Client.Pages.Account;

public partial class Login
{
    private MudForm form = new();
    private LoginRequest request = new();
    private Validator requestValidator = new();
    private PasswordInputConfigurations passwordConfiguration = new();

    public async Task SubmitAsync()
    {
        await form.Validate();

        if (form.IsValid)
        {
            await AccountManager.LoginAsync(request);

            NavigationManager.NavigateTo("/");
        }
    }

    private class Validator : AbstractValidator<LoginRequest>
    {
        private readonly string _mensajeCampoRequerido = "Campo requerido.";

        public Validator()
        {
            RuleFor(request => request.Username)
                .Must(request => !string.IsNullOrEmpty(request))
                .WithMessage(_mensajeCampoRequerido);

            RuleFor(request => request.Password)
                .Must(request => !string.IsNullOrEmpty(request))
                .WithMessage(_mensajeCampoRequerido);
        }

        public Func<object, string, Task<IEnumerable<string>>> Validation => async (model, propertyName) =>
        {
            ValidationResult result = await ValidateAsync(
                ValidationContext<LoginRequest>
                    .CreateWithOptions((LoginRequest)model,
                        x => x.IncludeProperties(propertyName)));

            if (result.IsValid) { return []; }

            return result.Errors.Select(error => error.ErrorMessage);
        };
    }
}
