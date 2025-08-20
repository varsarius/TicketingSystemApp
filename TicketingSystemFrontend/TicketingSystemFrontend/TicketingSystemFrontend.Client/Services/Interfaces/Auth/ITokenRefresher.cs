namespace TicketingSystemFrontend.Client.Services.Interfaces.Auth;

public interface ITokenRefresher
{
    Task<bool> TryRefreshAsync(CancellationToken cancellationToken = default);
}
