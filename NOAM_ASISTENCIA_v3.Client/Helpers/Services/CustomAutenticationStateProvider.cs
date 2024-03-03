using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using NOAM_ASISTENCIA_v3.Shared.Helpers.Services;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace NOAM_ASISTENCIA_v3.Client.Helpers.Services;

public class CustomAutenticationStateProvider(HttpClient httpClient, ILocalStorageService localStorageService, IAuthenticationService authenticationService)
: AuthenticationStateProvider, IDisposable
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly ILocalStorageService _localStorageService = localStorageService;
    private readonly IAuthenticationService _authenticationService = authenticationService;

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        string? token = await _localStorageService.GetItemAsync<string>("token");

        ClaimsIdentity identity;

        if (string.IsNullOrWhiteSpace(token)) { identity = new(); }
        else { identity = new(ParseClaimsFromJwt(token), "jwt"); }

        ClaimsPrincipal user = new(identity);
        AuthenticationState state = new(user);

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

        NotifyAuthenticationStateChanged(Task.FromResult(state));

        return state;
    }

    public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        return keyValuePairs?.Select(kvp => new Claim(kvp.Key, kvp.ToString() ?? string.Empty)) ?? [];
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }

    public void Dispose() => AuthenticationStateChanged -= NotifyAuthenticationStateChanged;
}
