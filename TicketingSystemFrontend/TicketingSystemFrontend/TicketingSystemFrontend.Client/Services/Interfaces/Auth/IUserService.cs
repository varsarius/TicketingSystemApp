using static TicketingSystemFrontend.Client.Pages.Auth.AdminPanel;

namespace TicketingSystemFrontend.Client.Services.Interfaces.Auth;

public interface IUserService
{
    Task<List<UserDto>> GetUsersAsync();
    Task UpdateUserRoleAsync(string username, string newRole);

    Task UpdateUserName(string username, string newUserName);
}
