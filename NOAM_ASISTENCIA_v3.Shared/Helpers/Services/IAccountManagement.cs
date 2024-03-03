using NOAM_ASISTENCIA_v3.Shared.Contracts.Authentication;

namespace NOAM_ASISTENCIA_v3.Shared.Helpers.Services;

/// <summary>
/// Account management services.
/// </summary>
public interface IAccountManagement
{
    /// <summary>
    /// Registration service.
    /// </summary>
    /// <param name="email">User's email.</param>
    /// <param name="password">User's password.</param>
    /// <returns>The result of the request serialized to <see cref="FormResult"/>.</returns>
    public Task RegisterAsync(string email, string password);
    /// <summary>
    /// Login service.
    /// </summary>
    /// <param name="email">User's email.</param>
    /// <param name="password">User's password.</param>
    /// <returns>The result of the request serialized to <see cref="FormResult"/>.</returns>
    public Task LoginAsync(string email, string password);
    /// <summary>
    /// Log out the logged in user.
    /// </summary>
    /// <returns>The asynchronous task.</returns>
    public Task LogoutAsync();
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Task<bool> CheckAuthenticatedAsync();
}
