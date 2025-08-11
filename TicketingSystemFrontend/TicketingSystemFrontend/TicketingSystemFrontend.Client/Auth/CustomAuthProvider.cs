using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TicketingSystemFrontend.Client.Services.Interfaces.Auth;

namespace TicketingSystemFrontend.Client.Auth;

public class CustomAuthProvider : AuthenticationStateProvider
{
    private readonly ITokenStorage _tokenStorage;
    private readonly JwtSecurityTokenHandler _tokenHandler = new();
    public CustomAuthProvider(ITokenStorage tokenStorage)
    {
        _tokenStorage = tokenStorage;
    }
    public async override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var (accessToken, _) = await _tokenStorage.GetTokensAsync();
        if (string.IsNullOrEmpty(accessToken))
            return GetUnauthorizedUser();

        try
        {
            var jwtToken = _tokenHandler.ReadJwtToken(accessToken);

            var identity = new ClaimsIdentity(jwtToken.Claims, "jwt");
            var user = new ClaimsPrincipal(identity);

            return new AuthenticationState(user);
        }
        catch
        {
            // Invalid token format → treat as unauthenticated
            return GetUnauthorizedUser();
        }
    }
    public async Task SetAuthenticatedUserAsync(string accessToken, string refreshToken)
    {
        await _tokenStorage.SaveTokensAsync(accessToken, refreshToken);
        var jwtToken = _tokenHandler.ReadJwtToken(accessToken);
        var identity = new ClaimsIdentity(jwtToken.Claims, "jwt");

        NotifyAuthenticationStateChanged(Task.FromResult(
            new AuthenticationState(new ClaimsPrincipal(identity))
        ));
    }

    public async Task LogoutAsync()
    {
        await _tokenStorage.ClearTokensAsync();
        NotifyAuthenticationStateChanged(Task.FromResult(GetUnauthorizedUser()));
    }

    //private AuthenticationState GetAuthorizedUser()
    //{
    //    var identity = new ClaimsIdentity(new[]
    //    {
    //        new Claim(ClaimTypes.Name, "User"),
    //        new Claim(ClaimTypes.Role, "User")
    //    }, "jwt");

    //    return new AuthenticationState(new ClaimsPrincipal(identity));
    //}

    private AuthenticationState GetUnauthorizedUser()
    {
        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }

}
