namespace NOAM_ASISTENCIA_v3.Shared.Features.Accounts.Login;

public class LoginResponse(string token)
{
    public string Token { get; } = token;
}
