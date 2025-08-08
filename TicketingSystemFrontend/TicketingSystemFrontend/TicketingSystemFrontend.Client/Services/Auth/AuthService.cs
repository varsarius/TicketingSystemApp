using System.Net.Http.Json;
using TicketingSystemFrontend.Client.DTOs;
using TicketingSystemFrontend.Client.Requests.Auth;
using TicketingSystemFrontend.Client.Services.Interfaces.Auth;

namespace TicketingSystemFrontend.Client.Services.Auth;

public class AuthService : IAuthService
{
    private readonly HttpClient _http;

    public AuthService(HttpClient http)
    {
        _http = http;
    }

    public async Task<LoginResult> LoginAsync(LoginRequest request)
    {
        var response = await _http.PostAsJsonAsync("api/auth/login", request);
        var loginResult = await response.Content.ReadFromJsonAsync<LoginResult>();
        return loginResult!;
    }

    public async Task<AuthResult> RegisterAsync(RegisterRequest request)
    {
        var response = await _http.PostAsJsonAsync("api/auth/register", request);
        if (response.IsSuccessStatusCode)
        {
            return new AuthResult { IsSuccess = true };
        }

        if (response.StatusCode == System.Net.HttpStatusCode.Conflict) // 409
        {
            var err = await response.Content.ReadAsStringAsync();
            return new AuthResult
            {
                IsSuccess = false,
                ErrorMessage = err,
            };
        }

        var error = await response.Content.ReadAsStringAsync();
        return new AuthResult { IsSuccess = false, ErrorMessage = error };
    }
}