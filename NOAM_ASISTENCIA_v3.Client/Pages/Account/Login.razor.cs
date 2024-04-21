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

    private bool success, errors;
    private string email = string.Empty;
    private string password = string.Empty;
    private string[] errorList = [];

    public async Task SubmitAsync()
    {
        success = errors = false;
        errorList = [];

        if (string.IsNullOrWhiteSpace(email))
        {
            errors = true;
            errorList = ["Email is required."];

            return;
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            errors = true;
            errorList = ["Password is required."];

            return;
        }

        await AccountManager.LoginAsync(new() { Username = email, Password = password, RememberMe = true });

        NavigationManager.NavigateTo("/");
    }

    private class Validator : AbstractValidator<LoginRequest>
    {
        private readonly string _mensajeCampoRequerido = "Campo requerido.";

        public Validator()
        {
            RuleFor(request => request.Username)
                .NotNull()
                .NotEmpty()
                .WithMessage(_mensajeCampoRequerido);

            RuleFor(request => request.Password)
                .NotNull()
                .NotEmpty()
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
