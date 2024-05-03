namespace NOAM_ASISTENCIA_v3.Shared.Helpers.Services;

public interface IUserService
{
    /// <summary>
    /// Delibera si el usuario existe mediante un nombre dado
    /// </summary>
    /// <returns></returns>
    Task<bool> UsernameExistsAsync(string username);
}
