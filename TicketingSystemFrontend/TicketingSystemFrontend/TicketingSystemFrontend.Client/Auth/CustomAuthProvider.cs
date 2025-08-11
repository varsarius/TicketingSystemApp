using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace TicketingSystemFrontend.Client.Auth;

public class CustomAuthProvider : AuthenticationStateProvider
{
    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        return Task.FromResult(GetAuthorizedUser());
    }
    private AuthenticationState GetAuthorizedUser()
    {
        var identity = new ClaimsIdentity(new[]
        {
                new Claim(ClaimTypes.Name, "User"),
                new Claim(ClaimTypes.Role, "User")
            }, "Fake authentication");

        var user = new ClaimsPrincipal(identity);
        return new AuthenticationState(user);
    }

    private AuthenticationState GetUnauthorizedUser()
    {
        var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
        return new AuthenticationState(anonymous);
    }

}
