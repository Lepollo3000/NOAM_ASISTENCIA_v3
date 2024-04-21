using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using NOAM_ASISTENCIA_v3.Shared.Helpers.Errors.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace NOAM_ASISTENCIA_v3.Client.Helpers.Services;

public class CustomAutenticationStateProvider(HttpClient httpClient, ILocalStorageService localStorageService) : AuthenticationStateProvider
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly ILocalStorageService _localStorageService = localStorageService;
    private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            string? savedToken = await _localStorageService.GetItemAsync<string>("token");

            if (string.IsNullOrEmpty(savedToken))
            {
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }

            CustomUserClaims userClaims = DecryptToken(savedToken);

            if (userClaims == null)
            {
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }

            ClaimsPrincipal claimsPrincipal = SetClaimsPrincipal(userClaims);

            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }
        catch
        {
            return await Task.FromResult(new AuthenticationState(_anonymous));
        }
    }

    public async Task UpdateAuthenticationState(string? tokenString = null)
    {
        ClaimsPrincipal claimsPrincipal = new();

        if (!string.IsNullOrEmpty(tokenString))
        {
            await _localStorageService.SetItemAsync("token", tokenString);

            CustomUserClaims userClaims = DecryptToken(tokenString);
            claimsPrincipal = SetClaimsPrincipal(userClaims);
        }
        else
        {
            await _localStorageService.RemoveItemAsync("token");
        }

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }

    public static ClaimsPrincipal SetClaimsPrincipal(CustomUserClaims claims)
    {
        if (claims.Email is null) { return new(); }

        return new(new ClaimsIdentity(
            [
                new(ClaimTypes.Name, claims.Name),
                new(ClaimTypes.Email, claims.Email)
            ],
            "JwtAuth"));
    }

    private static CustomUserClaims DecryptToken(string tokenString)
    {
        if (string.IsNullOrEmpty(tokenString)) { return new(); }

        JwtSecurityTokenHandler handler = new();
        JwtSecurityToken token = handler.ReadJwtToken(tokenString);

        Claim? name = token.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name);
        Claim? email = token.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email);

        return new(name?.Value ?? string.Empty, email?.Value ?? string.Empty);
    }
}
