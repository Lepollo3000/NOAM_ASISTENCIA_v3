using NOAM_ASISTENCIA_v3.Shared.Contracts.Users;
using NOAM_ASISTENCIA_v3.Shared.Helpers.Services;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace NOAM_ASISTENCIA_v3.Client.Helpers.Services;

public class AccountService(HttpClient httpClient) : IAccountService
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task LoginAsync(LoginRequest loginRequest)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("accounts/login", loginRequest);

        if (response.IsSuccessStatusCode)
        {
            LoginResponse result = await response.Content.ReadFromJsonAsync<LoginResponse>() ?? new(string.Empty);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);
        }
    }
}
