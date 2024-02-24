namespace NOAM_ASISTENCIA_v3.Shared.Helpers.Services;

public interface IUserService
{
    Task<bool> UsernameExistsAsync(string username);
}
