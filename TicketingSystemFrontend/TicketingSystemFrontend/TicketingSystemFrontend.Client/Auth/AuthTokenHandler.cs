using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using TicketingSystemFrontend.Client.DTOs;
using TicketingSystemFrontend.Client.Services.Auth;
using TicketingSystemFrontend.Client.Services.Interfaces.Auth;

namespace TicketingSystemFrontend.Client.Auth;

public class AuthTokenHandler : DelegatingHandler
{
    private readonly ITokenStorage _tokenStorage;
    private readonly CustomAuthProvider _authProvider;
    private readonly HttpClient _httpClient; // raw client for refresh calls
    private readonly ITokenRefresher _tokenRefresher;

    public AuthTokenHandler(ITokenStorage tokenStorage, CustomAuthProvider authProvider, IHttpClientFactory httpClientFactory, ITokenRefresher tokenRefresher)
    {
        _tokenStorage = tokenStorage;
        _authProvider = authProvider;
        // use a dedicated client that has no handlers attached -> avoids infinite recursion
        _httpClient = httpClientFactory.CreateClient("NoAuth");
        _tokenRefresher = tokenRefresher;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var (accessToken, _) = await _tokenStorage.GetTokensAsync();
        if (!string.IsNullOrEmpty(accessToken))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        var response = await base.SendAsync(request, cancellationToken);

        // Handle unauthorized -> try refresh once
        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            response.Dispose();
            if (await _tokenRefresher.TryRefreshAsync(cancellationToken))
            {
                // retry original request with fresh token
                var (newAccessToken, _) = await _tokenStorage.GetTokensAsync();
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", newAccessToken);
                response = await base.SendAsync(request, cancellationToken);
            }
        }

        return response;
    }
}