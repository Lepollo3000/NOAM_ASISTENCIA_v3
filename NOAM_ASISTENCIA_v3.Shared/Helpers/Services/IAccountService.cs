using NOAM_ASISTENCIA_v3.Shared.Features.Accounts.Login;

namespace NOAM_ASISTENCIA_v3.Shared.Helpers.Services;

/// <summary>
/// Servicio para manejo de lógica de usuarios
/// </summary>
public interface IAccountService
{
    /// <summary>
    /// Registrar usuario.
    /// </summary>
    /// <param name="email">User's email.</param>
    /// <param name="password">User's password.</param>
    /// <returns></returns>
    public Task RegisterAsync(string email, string password);
    /// <summary>
    /// Inicio de sesión.
    /// </summary>
    /// <param name="loginRequest">La consulta para el inicio de sesión</param>
    /// <returns></returns>
    public Task LoginAsync(LoginRequest loginRequest);
    /// <summary>
    /// Cierre de sesión.
    /// </summary>
    /// <returns></returns>
    public Task LogoutAsync();
    /// <summary>
    /// Revisión de sesión
    /// </summary>
    /// <returns></returns>
    public Task<bool> CheckAuthenticatedAsync();
}
