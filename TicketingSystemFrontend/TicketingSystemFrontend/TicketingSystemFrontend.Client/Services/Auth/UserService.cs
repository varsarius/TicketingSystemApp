using System.Net.Http;
using System.Net.Http.Json;
using TicketingSystemFrontend.Client.Pages;
using TicketingSystemFrontend.Client.Pages.Auth;
using TicketingSystemFrontend.Client.Services.Interfaces.Auth;
using static TicketingSystemFrontend.Client.Pages.Auth.AdminPanel;
//using static TicketingSystemFrontend.Client.Pages.Auth.AdminPanel;


namespace TicketingSystemFrontend.Client.Services.Auth;

public class UserService : IUserService
{
    private readonly HttpClient _http;

    public UserService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<UserDto>> GetUsersAsync()
    {      
        var users = await _http.GetFromJsonAsync<List<UserDto>>("api/admin/users");
        return users ?? new List<UserDto>();
    }

    public async Task UpdateUserName(string username, string newUserName)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("Username cannot be empty.", nameof(username));

        if (string.IsNullOrWhiteSpace(newUserName))
            throw new ArgumentException("New username cannot be empty.", nameof(newUserName));

        var url = $"api/auth/users/{username}/name";

        var content = new { newUserName };

        var response = await _http.PatchAsJsonAsync(url, content);

        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException(
                $"Failed to update username ({response.StatusCode}): {errorMessage}"
            );
        }
    }


    public async Task UpdateUserRoleAsync(string username, string newRole)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("Username cannot be empty.", nameof(username));

        if (string.IsNullOrWhiteSpace(newRole))
            throw new ArgumentException("Role cannot be empty.", nameof(newRole));



        var url = $"api/auth/users/{username}/role";

        var content = new { newRole = newRole };

        var response = await _http.PatchAsJsonAsync(url, content);

        
        
        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException(
                $"Failed to update role ({response.StatusCode}): {errorMessage}"
            );
        }
    }

}
