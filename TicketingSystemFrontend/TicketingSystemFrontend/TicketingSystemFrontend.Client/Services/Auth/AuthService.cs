using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using TicketingSystemFrontend.Client.Auth;
using TicketingSystemFrontend.Client.DTOs;
using TicketingSystemFrontend.Client.Requests.Auth;
using TicketingSystemFrontend.Client.Services.Interfaces.Auth;

namespace TicketingSystemFrontend.Client.Services.Auth;

public class AuthService : IAuthService
{
    private readonly HttpClient _http;
    private readonly CustomAuthProvider _authProvider;

    public AuthService(HttpClient http, CustomAuthProvider authProvider)
    {
        _http = http;
        _authProvider = authProvider;
    }

    public async Task<LoginResult> LoginAsync(LoginRequest request)
    {
        var response = await _http.PostAsJsonAsync("api/auth/login", request);
        var loginResult = await response.Content.ReadFromJsonAsync<LoginResult>();
        if (loginResult is not null && loginResult.IsSuccess)
        {
            await _authProvider.SetAuthenticatedUserAsync(
                loginResult.AccessToken,
                loginResult.RefreshToken
            );
        }

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

    public async Task LogoutAsync()
    {
        await _authProvider.LogoutAsync();
    }

    public async Task<Guid> GetUserIdFromTokenAsync()
    {
        //var (access, _) = await GetTokensAsync();
        //if (string.IsNullOrEmpty(access))
        //    throw new InvalidOperationException("No access token available");

        //var handler = new JwtSecurityTokenHandler();
        //var jwt = handler.ReadJwtToken(access);

        //var subClaim = jwt.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
        //if (subClaim == null || !Guid.TryParse(subClaim, out var userId))
        //    throw new InvalidOperationException("UserId (sub) not found in token");

        //return userId;


        var authState = await _authProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (user.Identity?.IsAuthenticated != true)
        {
            throw new Exception("User is not authenticated");
        }

        var userIdClaim = user.FindFirst("sub");
        if (userIdClaim == null)
        {
            throw new Exception("UserId claim not found");
        }

        var userId = Guid.Parse(userIdClaim.Value);
        return userId;
    }
}