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
        // Call the Blazor Server endpoint, not the backend API directly
        var response = await _http.PostAsJsonAsync("/server-login", request);

        if (!response.IsSuccessStatusCode)
        {
            // Login failed
            return new LoginResult(); // empty, IsSuccess will be false
        }

        // Since the server sets HttpOnly cookies, we no longer get tokens here
        // You can optionally return a dummy LoginResult to satisfy the interface
        return new LoginResult
        {
            AccessToken = "b2",      // optional, just to satisfy the DTO
            RefreshToken = ""
        };
    }

    public async Task<AuthResult> RegisterAsync(RegisterRequest request)
    {
        var response = await _http.PostAsJsonAsync("/server-register", request);
        if (response.IsSuccessStatusCode)
            return new AuthResult { IsSuccess = true };

        var error = await response.Content.ReadAsStringAsync();
        return new AuthResult { IsSuccess = false, ErrorMessage = error };
    }

    public void LogoutAsync()
    {
        _authProvider.NotifyUserLogout();
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