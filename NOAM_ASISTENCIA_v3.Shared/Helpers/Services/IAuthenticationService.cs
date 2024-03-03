using NOAM_ASISTENCIA_v3.Shared.Contracts.Authentication;

namespace NOAM_ASISTENCIA_v3.Shared.Helpers.Services;

public interface IAuthenticationService
{
    Task<UserInformation?> SendAuthenticateRequestAsync(string username, string password);
    Task<UserInformation?> FetchUserFromBrowserAsync();
    Task ClearBrowserUserDataAsync();
}
