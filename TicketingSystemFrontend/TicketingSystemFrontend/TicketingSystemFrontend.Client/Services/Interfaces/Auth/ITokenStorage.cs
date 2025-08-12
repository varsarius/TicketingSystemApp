namespace TicketingSystemFrontend.Client.Services.Interfaces.Auth;

public interface ITokenStorage
{
    Task SaveTokensAsync(string accessToken, string refreshToken);
    Task<(string? AccessToken, string? RefreshToken)> GetTokensAsync();
    Task ClearTokensAsync();
}
