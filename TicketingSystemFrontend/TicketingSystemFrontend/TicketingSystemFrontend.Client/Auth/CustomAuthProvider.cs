using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace TicketingSystemFrontend.Client.Auth;

public class CustomAuthProvider : AuthenticationStateProvider
{
    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        // Always return unauthorized for now
        var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
        return Task.FromResult(new AuthenticationState(anonymous));
    }
}
