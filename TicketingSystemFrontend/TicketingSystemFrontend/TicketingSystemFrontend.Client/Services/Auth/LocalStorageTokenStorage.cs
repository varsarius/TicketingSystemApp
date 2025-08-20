using Blazored.LocalStorage;
using TicketingSystemFrontend.Client.Services.Interfaces.Auth;

namespace TicketingSystemFrontend.Client.Services.Auth;

public class LocalStorageTokenStorage //: ITokenStorage
{
    private readonly ILocalStorageService _localStorage;

    public LocalStorageTokenStorage(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }
    public async Task ClearTokensAsync()
    {
        await _localStorage.RemoveItemAsync("access_token");
        await _localStorage.RemoveItemAsync("refresh_token");
    }

    public async Task<(string? AccessToken, string? RefreshToken)> GetTokensAsync()
    {
        var access = await _localStorage.GetItemAsync<string>("access_token");
        var refresh = await _localStorage.GetItemAsync<string>("refresh_token");
        return (access, refresh);
    }

    public async Task SaveTokensAsync(string accessToken, string refreshToken)
    {
        await _localStorage.SetItemAsync("access_token", accessToken);
        await _localStorage.SetItemAsync("refresh_token", refreshToken);
    }
}
