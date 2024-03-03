using Blazored.LocalStorage;
using NOAM_ASISTENCIA_v3.Shared.Contracts.Authentication;
using NOAM_ASISTENCIA_v3.Shared.Helpers.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace NOAM_ASISTENCIA_v3.Client.Helpers.Services;

public class AuthenticationService(HttpClient httpClient, ILocalStorageService localStorageService) : IAuthenticationService
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly ILocalStorageService _localStorageService = localStorageService;

    public async Task<UserInformation?> SendAuthenticateRequestAsync(string username, string password)
    {
        var response = await _httpClient.GetAsync($"/example-data/{username}.json");

        if (response.IsSuccessStatusCode)
        {
            string token = await response.Content.ReadAsStringAsync();
            var claimPrincipal = CreateClaimsPrincipalFromToken(token);
            var user = UserInformation.FromClaimsPrincipal(claimPrincipal);

            await PersistUserToBrowser(token);

            return user;
        }

        return null;
    }

    public async Task<UserInformation?> FetchUserFromBrowserAsync()
    {
        var claimsPrincipal = CreateClaimsPrincipalFromToken(await
            _localStorageService.GetItemAsStringAsync("token") ?? string.Empty);
        var user = UserInformation.FromClaimsPrincipal(claimsPrincipal);

        return user;
    }

    public async Task ClearBrowserUserDataAsync() => await _localStorageService.SetItemAsStringAsync("token", string.Empty);

    private static ClaimsPrincipal CreateClaimsPrincipalFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var identity = new ClaimsIdentity();

        if (tokenHandler.CanReadToken(token))
        {
            var jwtSecurityToken = tokenHandler.ReadJwtToken(token);
            identity = new(jwtSecurityToken.Claims, "Blazor School");
        }

        return new(identity);
    }

    private async Task PersistUserToBrowser(string token) => await _localStorageService.SetItemAsStringAsync("token", token);
}
