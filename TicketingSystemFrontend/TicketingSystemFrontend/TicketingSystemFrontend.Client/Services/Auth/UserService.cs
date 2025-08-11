using System.Net.Http;
using System.Net.Http.Json;
using TicketingSystemFrontend.Client.Pages;
using TicketingSystemFrontend.Client.Services.Interfaces.Auth;
using static TicketingSystemFrontend.Client.Pages.AdminPanel;

namespace TicketingSystemFrontend.Client.Services.Auth;

public class UserService : IUserService
{
    private readonly HttpClient _http;

    public UserService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<AdminPanel.UserDto>> GetUsersAsync()
    {
        // Replace "api/admin/users" with your actual API endpoint
        
        var users = await _http.GetFromJsonAsync<List<UserDto>>("api/admin/users");
        return users ?? new List<UserDto>();
    }

    public async Task UpdateUserRoleAsync(Guid userId, string newRole)
    {
        var url = $"api/users/{userId}/role";

        var content = new { role = newRole };

        var response = await _http.PutAsJsonAsync(url, content);

        if (!response.IsSuccessStatusCode)
        {
            // Optional: you can throw or handle errors as you want
            throw new Exception($"Failed to update role: {response.StatusCode}");
        }
    }
}
