using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using TicketingSystemFrontend.Client.Auth;
using TicketingSystemFrontend.Client.Services.Interfaces.Auth;

namespace TicketingSystemFrontend.Client.Services.Auth;

public class CookieTokenStorage : ITokenStorage
{
    private readonly IJSRuntime _js;

    public CookieTokenStorage(IJSRuntime js)
    {
        _js = js;
    }

    public async Task ClearTokensAsync()
    {
        await _js.InvokeVoidAsync("cookieHelper.erase", "access_token");
        await _js.InvokeVoidAsync("cookieHelper.erase", "refresh_token");
    }

    public async Task<(string? AccessToken, string? RefreshToken)> GetTokensAsync()
    {
        var access = await _js.InvokeAsync<string>("cookieHelper.get", "access_token");
        var refresh = await _js.InvokeAsync<string>("cookieHelper.get", "refresh_token");
        return (string.IsNullOrEmpty(access) ? null : access,
                string.IsNullOrEmpty(refresh) ? null : refresh);
    }

    

    public async Task SaveTokensAsync(string accessToken, string refreshToken)
    {
        // Save cookies for 7 days, adjust if needed
        await _js.InvokeVoidAsync("cookieHelper.set", "access_token", accessToken, 7);
        await _js.InvokeVoidAsync("cookieHelper.set", "refresh_token", refreshToken, 30);
    }
}
