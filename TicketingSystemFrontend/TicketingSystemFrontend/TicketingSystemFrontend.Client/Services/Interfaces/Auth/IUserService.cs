using static TicketingSystemFrontend.Client.Pages.AdminPanel;

namespace TicketingSystemFrontend.Client.Services.Interfaces.Auth;

public interface IUserService
{
    Task<List<UserDto>> GetUsersAsync();
    Task UpdateUserRoleAsync(Guid userId, string newRole);
}
