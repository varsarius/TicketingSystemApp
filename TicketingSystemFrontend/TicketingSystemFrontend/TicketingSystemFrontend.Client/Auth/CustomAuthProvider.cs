using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TicketingSystemFrontend.Client.Services.Interfaces.Auth;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace TicketingSystemFrontend.Client.Auth;

public class CustomAuthProvider : AuthenticationStateProvider
{
    private readonly HttpClient _http;

    public CustomAuthProvider(HttpClient http)
    {
        _http = http;
    }
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            Console.WriteLine("CustomAuthProvider: Getting authentication state...");
            var request = new HttpRequestMessage(HttpMethod.Get, "/server-auth-state");
            Console.WriteLine("CustomAuthProvider: Sending request to /server-auth-state");
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
            Console.WriteLine("CustomAuthProvider: Request prepared, sending to server...");
            var response = await _http.SendAsync(request);
            Console.WriteLine("CustomAuthProvider: Response received from server");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("CustomAuthProvider: Response not successful, returning unauthorized user");
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            Console.WriteLine("CustomAuthProvider: Reading user data from response");
            var user = await response.Content.ReadFromJsonAsync<UserDto>();
            Console.WriteLine("CustomAuthProvider: User data read successfully");
            if (user == null)
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

            Console.WriteLine($"CustomAuthProvider: User found with ID: {user.Id}, Email: {user.Email}");
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim("sub", user.Id.ToString()),
                // Add other claims if needed
            }, "serverAuth");

            Console.WriteLine("CustomAuthProvider: ClaimsIdentity created successfully");
            return new AuthenticationState(new ClaimsPrincipal(identity));
        }
        catch
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
    }

    // public async Task SetAuthenticatedUserAsync(string accessToken, string refreshToken)
    // {
    //     await _tokenStorage.SaveTokensAsync(accessToken, refreshToken);
    //     var jwtToken = _tokenHandler.ReadJwtToken(accessToken);
    //     var identity = new ClaimsIdentity(jwtToken.Claims, "jwt");

    //     NotifyAuthenticationStateChanged(Task.FromResult(
    //         new AuthenticationState(new ClaimsPrincipal(identity))
    //     ));
    // }

    // public async Task LogoutAsync()
    // {
    //     await _tokenStorage.ClearTokensAsync();
    //     NotifyAuthenticationStateChanged(Task.FromResult(GetUnauthorizedUser()));
    // }

    //private AuthenticationState GetAuthorizedUser()
    //{
    //    var identity = new ClaimsIdentity(new[]
    //    {
    //        new Claim(ClaimTypes.Name, "User"),
    //        new Claim(ClaimTypes.Role, "User")
    //    }, "jwt");

    //    return new AuthenticationState(new ClaimsPrincipal(identity));
    //}

    private AuthenticationState GetUnauthorizedUser()
    {
        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }

    public void NotifyUserAuthentication(ClaimsPrincipal user)
    {
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }
    public void NotifyUserLogout()
    {
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()))));
    }

}


class UserDto
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    // Add other properties as needed
}