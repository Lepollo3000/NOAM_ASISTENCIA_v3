namespace NOAM_ASISTENCIA_v3.Shared.Contracts.Users;

public class LoginResponse(string token)
{
    public string Token { get; } = token;
}
