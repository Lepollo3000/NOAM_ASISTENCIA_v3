using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using NOAM_ASISTENCIA_v3.Client.Helpers.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace NOAM_ASISTENCIA_v3.Client.Helpers.Services;

public class CustomAutenticationStateProvider(ILocalStorageService localStorageService) : AuthenticationStateProvider
{
    private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());
    private readonly ILocalStorageService _localStorageService = localStorageService;

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
        ClaimsPrincipal claimsPrincipal = _anonymous;

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

        List<Claim> claimList = [
            new(ClaimTypes.Name, claims.Name),
            new(ClaimTypes.Email, claims.Email)
        ];

        foreach (string claim in claims.Roles)
        {
            claimList.Add(new Claim(ClaimTypes.Role, claim));
        }

        return new(new ClaimsIdentity(claimList, "JwtAuth"));
    }

    private static CustomUserClaims DecryptToken(string tokenString)
    {
        if (string.IsNullOrEmpty(tokenString)) { return new(); }

        JwtSecurityTokenHandler handler = new();
        JwtSecurityToken token = handler.ReadJwtToken(tokenString);

        Claim? name = token.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name);
        Claim? email = token.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email);
        List<Claim> roles = token.Claims.Where(claim => claim.Type == ClaimTypes.Role).ToList();

        return new(name?.Value ?? string.Empty, email?.Value ?? string.Empty, roles.Select(claim => claim.Value).ToArray());
    }
}
