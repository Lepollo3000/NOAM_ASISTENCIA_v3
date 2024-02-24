using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using NOAM_ASISTENCIA_v3.Shared.Contracts.Authentication;
using NOAM_ASISTENCIA_v3.Shared.Contracts.Users;
using NOAM_ASISTENCIA_v3.Shared.Helpers.Services;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;

namespace NOAM_ASISTENCIA_v3.Client.Helpers.Services;

/// <summary>
/// Handles state for cookie-based auth.
/// </summary>
/// <remarks>
/// Create a new instance of the original AuthenticationStateProvider.
/// </remarks>
/// <param name="localStorage">Service to access local storage.</param>
public class CookieAuthenticationStateProvider(HttpClient httpClient, ILocalStorageService localStorage) : AuthenticationStateProvider, IAccountManagement
{
    /// <summary>
    /// Map the JavaScript-formatted properties to C#-formatted classes.
    /// </summary>
    private readonly JsonSerializerOptions jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

    /// <summary>
    /// Special auth client.
    /// </summary>
    private readonly HttpClient _httpClient = httpClient;

    /// <summary>
    /// Service to access local storage.
    /// </summary>
    private readonly ILocalStorageService _localStorage = localStorage;

    /// <summary>
    /// Current authentication state.
    /// </summary>
    private bool _authenticated = false;

    /// <summary>
    /// Register a new user.
    /// </summary>
    /// <param name="email">The user's email address.</param>
    /// <param name="password">The user's password.</param>
    /// <returns>The result serialized to a <see cref="FormResult"/>.
    /// </returns>
    public async Task<FormResult> RegisterAsync(string email, string password)
    {
        string[] defaultDetail = ["An unknown error prevented registration from succeeding."];

        try
        {
            // make the request
            var result = await _httpClient.PostAsJsonAsync(
                "register", new
                {
                    email,
                    password
                });

            // successful?
            if (result.IsSuccessStatusCode)
            {
                return new FormResult { Succeeded = true };
            }

            // body should contain details about why it failed
            var details = await result.Content.ReadAsStringAsync();
            var problemDetails = JsonDocument.Parse(details);
            var errors = new List<string>();
            var errorList = problemDetails.RootElement.GetProperty("errors");

            foreach (var errorEntry in errorList.EnumerateObject())
            {
                if (errorEntry.Value.ValueKind == JsonValueKind.String)
                {
                    errors.Add(errorEntry.Value.GetString()!);
                }
                else if (errorEntry.Value.ValueKind == JsonValueKind.Array)
                {
                    errors.AddRange(
                        errorEntry.Value.EnumerateArray().Select(
                            e => e.GetString() ?? string.Empty)
                        .Where(e => !string.IsNullOrEmpty(e)));
                }
            }

            // return the error list
            return new FormResult
            {
                Succeeded = false,
                ErrorList = problemDetails == null ? defaultDetail : [.. errors]
            };
        }
        catch { }

        // unknown error
        return new FormResult
        {
            Succeeded = false,
            ErrorList = defaultDetail
        };
    }

    /// <summary>
    /// User login.
    /// </summary>
    /// <param name="email">The user's email address.</param>
    /// <param name="password">The user's password.</param>
    /// <returns>The result of the login request serialized to a <see cref="FormResult"/>.</returns>
    public async Task<FormResult> LoginAsync(string email, string password)
    {
        try
        {
            // login with cookies
            var result = await _httpClient.PostAsJsonAsync(
                "api/accounts/login", new LoginRequest
                {
                    Username = email,
                    Password = password,
                    RememberMe = true
                });

            // success?
            if (result.IsSuccessStatusCode)
            {
                var token = await result.Content.ReadAsStringAsync();
                await _localStorage.SetItemAsync("token", token);
                AuthenticationState state = await GetAuthenticationStateAsync();

                _authenticated = state.User.Identity?.IsAuthenticated ?? false;

                // need to refresh auth state
                NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

                // success!
                return new FormResult { Succeeded = true };
            }
        }
        catch { }

        // unknown error
        return new FormResult
        {
            Succeeded = false,
            ErrorList = ["morido"] /*UserErrors.ErrorInesperado.Errors.ToArray()*/
        };
    }

    /// <summary>
    /// Get authentication state.
    /// </summary>
    /// <remarks>
    /// Called by Blazor anytime and authentication-based decision needs to be made, then cached
    /// until the changed state notification is raised.
    /// </remarks>
    /// <returns>The authentication state asynchronous request.</returns>
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        string token = await _localStorage.GetItemAsStringAsync("token");
        ClaimsIdentity identity = new();

        _httpClient.DefaultRequestHeaders.Authorization = null;

        if (!string.IsNullOrEmpty(token))
        {
            identity = new(ParseClaimsFromJwt(token), "jwt");

            _httpClient.DefaultRequestHeaders
                .Authorization = new("Bearer", token.Replace("\"", ""));
        }

        ClaimsPrincipal user = new(identity);
        AuthenticationState state = new(user);

        NotifyAuthenticationStateChanged(Task.FromResult(state));

        await GetAuthenticationStateAsync();

        return state;
    }

    /// <summary>
    /// Cierra la sesión y elimina todo rastro de la misma
    /// </summary>
    public async Task LogoutAsync()
    {
        await _localStorage.RemoveItemAsync("token");

        ClaimsPrincipal anonymousUser = new(new ClaimsIdentity());
        AuthenticationState state = new(anonymousUser);

        _httpClient.DefaultRequestHeaders.Authorization = null;

        NotifyAuthenticationStateChanged(Task.FromResult(state));
    }

    /// <summary>
    /// Revisa si hay una sesión actualmente iniciada
    /// </summary>
    public async Task<bool> CheckAuthenticatedAsync()
    {
        await GetAuthenticationStateAsync();

        return _authenticated;
    }

    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer
            .Deserialize<Dictionary<string, object>>(jsonBytes);

        return keyValuePairs?.Select(kvp =>
            new Claim(kvp.Key, kvp.Value?.ToString()!))!;
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
}