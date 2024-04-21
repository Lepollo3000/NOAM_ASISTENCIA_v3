using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using NOAM_ASISTENCIA_v3.Shared.Contracts.Users;
using NOAM_ASISTENCIA_v3.Shared.Helpers.Services;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace NOAM_ASISTENCIA_v3.Client.Helpers.Services;

public class AccountManagement(HttpClient httpClient, ILocalStorageService localStorageService, AuthenticationStateProvider authenticationStateProvider) : IAccountManagement
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly ILocalStorageService _localStorageService = localStorageService;
    private readonly AuthenticationStateProvider _authenticationStateProvider = authenticationStateProvider;

    public async Task RegisterAsync(string email, string password)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("accounts/", new { email, password });

        if (response.IsSuccessStatusCode)
        {
            string result = await response.Content.ReadFromJsonAsync<string>() ?? string.Empty;


        }
    }

    public async Task LoginAsync(LoginRequest loginRequest)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("accounts/login", loginRequest);

        if (response.IsSuccessStatusCode)
        {
            LoginResponse result = await response.Content.ReadFromJsonAsync<LoginResponse>() ?? new(string.Empty);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);

            await ((CustomAutenticationStateProvider)_authenticationStateProvider).UpdateAuthenticationState(result.Token);
        }
    }

    public async Task LogoutAsync()
    {
        _httpClient.DefaultRequestHeaders.Authorization = null;

        await ((CustomAutenticationStateProvider)_authenticationStateProvider).UpdateAuthenticationState();
    }

    public async Task<bool> CheckAuthenticatedAsync()
    {
        AuthenticationState authenticationState = await
            ((CustomAutenticationStateProvider)_authenticationStateProvider).GetAuthenticationStateAsync();

        return authenticationState.User.Identity?.IsAuthenticated ?? false;
    }
}
