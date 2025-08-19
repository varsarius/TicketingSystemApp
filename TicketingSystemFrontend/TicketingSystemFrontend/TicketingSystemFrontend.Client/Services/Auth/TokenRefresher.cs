using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using TicketingSystemFrontend.Client.Auth;
using TicketingSystemFrontend.Client.DTOs;
using TicketingSystemFrontend.Client.Services.Interfaces.Auth;

namespace TicketingSystemFrontend.Client.Services.Auth;

public class TokenRefresher : ITokenRefresher
{
    private readonly ITokenStorage _tokenStorage;
    private readonly CustomAuthProvider _authProvider;
    private readonly HttpClient _httpClient; // barebones client, no handlers

    public TokenRefresher(ITokenStorage tokenStorage, CustomAuthProvider authProvider, IHttpClientFactory httpClientFactory)
    {
        _tokenStorage = tokenStorage;
        _authProvider = authProvider;
        _httpClient = httpClientFactory.CreateClient("NoAuth"); // avoid recursion
    }
    public async Task<bool> TryRefreshAsync(CancellationToken cancellationToken = default)
    {
        var (accessToken, refreshToken) = await _tokenStorage.GetTokensAsync();
        if (string.IsNullOrEmpty(refreshToken))
            return false;

        var refreshResponse = await _httpClient.PostAsJsonAsync("api/auth/refresh",
            new RefreshTokenRequest { RefreshToken = refreshToken }, cancellationToken);

        if (!refreshResponse.IsSuccessStatusCode)
            return false;

        var dto = await refreshResponse.Content.ReadFromJsonAsync<AuthResponseDto>(cancellationToken: cancellationToken);
        if (dto == null)
            return false;

        await _authProvider.SetAuthenticatedUserAsync(dto.AccessToken, dto.RefreshToken);
        return true;
    }
}

public class RefreshTokenRequest
{
    [Required]
    [JsonPropertyName("refreshToken")]
    public string RefreshToken { get; set; } = null!;
}
