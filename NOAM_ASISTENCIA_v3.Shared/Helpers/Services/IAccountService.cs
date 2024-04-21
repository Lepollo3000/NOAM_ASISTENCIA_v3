using NOAM_ASISTENCIA_v3.Shared.Contracts.Users;

namespace NOAM_ASISTENCIA_v3.Shared.Helpers.Services;

public interface IAccountService
{
    Task LoginAsync(LoginRequest loginRequest);
}
