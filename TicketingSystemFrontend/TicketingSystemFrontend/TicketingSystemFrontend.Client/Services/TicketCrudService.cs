using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using System.Net.Http.Json;
using TicketingSystemFrontend.Client.DTOs;
using TicketingSystemFrontend.Client.Requests.Commands.Tickets;
using TicketingSystemFrontend.Client.Services.Interfaces;

namespace TicketingSystemFrontend.Client.Services;

public class TicketCrudService : ITicketCrudService
{
    private readonly HttpClient _http;
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public TicketCrudService(HttpClient http, AuthenticationStateProvider authenticationStateProvider)
    {
        _http = http;
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task CreateAsync(CreateTicketCommand request)
    {
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (user.Identity?.IsAuthenticated != true)
        {
            throw new Exception("User is not authenticated");
        }

        var userIdClaim = user.FindFirst("sub");
        if (userIdClaim == null)
        {
            throw new Exception("UserId claim not found");
        }

        var userId = Guid.Parse(userIdClaim.Value);
        request = request with { UserId = userId }; // Set UserId from JWT token claim

        var response = await _http.PostAsJsonAsync("api/tickets", request);
        response.EnsureSuccessStatusCode();
    }

    public async Task<List<TicketDto>> GetAllAsync()
    {
        var tickets = await _http.GetFromJsonAsync<List<TicketDto>>("api/tickets");
        return tickets ?? new List<TicketDto>();
    }

    public async Task<TicketDto?> GetByIdAsync(int id)
    {
        return await _http.GetFromJsonAsync<TicketDto>($"api/tickets/{id}");
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/tickets/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return false;
        }
        return true;
    }

    public async Task UpdateAsync(UpdateTicketCommand request)
    {
        var response = await _http.PutAsJsonAsync($"api/tickets/{request.Id}", request);
        response.EnsureSuccessStatusCode();
    }
}