using TicketingSystemFrontend.Client.DTOs;
using TicketingSystemFrontend.Client.Requests.Auth;

namespace TicketingSystemFrontend.Client.Services.Interfaces.Auth;

public interface IAuthService
{
    Task<AuthResult> RegisterAsync(RegisterRequest request);
    Task<LoginResult> LoginAsync(LoginRequest request);
    void LogoutAsync();
    Task<Guid> GetUserIdFromTokenAsync();

}