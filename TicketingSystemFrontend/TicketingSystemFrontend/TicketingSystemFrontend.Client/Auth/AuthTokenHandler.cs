using System.Net.Http.Headers;
using TicketingSystemFrontend.Client.Services.Interfaces.Auth;

namespace TicketingSystemFrontend.Client.Auth;

public class AuthTokenHandler : DelegatingHandler
{
    private readonly ITokenStorage _tokenStorage;

    public AuthTokenHandler(ITokenStorage tokenStorage)
    {
        _tokenStorage = tokenStorage;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var (accessToken, _) = await _tokenStorage.GetTokensAsync();
        if (!string.IsNullOrEmpty(accessToken))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }
        return await base.SendAsync(request, cancellationToken);
    }
}