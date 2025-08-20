using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http;
using System.Net.Http.Json;
using TicketingSystemFrontend.Client.Auth;
using TicketingSystemFrontend.Client.DTOs;
using TicketingSystemFrontend.Client.Requests;
using TicketingSystemFrontend.Client.Services.Interfaces;

namespace TicketingSystemFrontend.Client.Services;

public class TicketService : ITicketService
{
    private readonly HttpClient _http;
    private readonly CustomAuthProvider _authenticationStateProvider;

    public TicketService(HttpClient http, CustomAuthProvider authenticationStateProvider)
    {
        _http = http;
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<int?> CreateAsync(TicketCreateRequest request)
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
        request.UserId = userId; // Set UserId from JWT token claim

        var response = await _http.PostAsJsonAsync("api/tickets", request);
        response.EnsureSuccessStatusCode();

        var ticketId = await response.Content.ReadFromJsonAsync<int>();
        return ticketId; //TODO: return normall id

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

    public async Task<bool> DeleteByIdAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/tickets/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return false;
        }
        return true;
    }

    public async Task UpdateAsync(TicketUpdateRequest request)
    {
        var response = await _http.PutAsJsonAsync($"api/tickets/{request.Id}", request);
        response.EnsureSuccessStatusCode();
    }

    public async Task<List<TicketDto>> GetAllSortFilterAsync(string? sortBy = null, string? sortOrder = null, int? categoryId = null, string? status = null, string? priority = null)
    {
        var queryParams = new List<string>();

        if (!string.IsNullOrEmpty(sortBy))
            queryParams.Add($"sortBy={Uri.EscapeDataString(sortBy)}");

        if (!string.IsNullOrEmpty(sortOrder))
            queryParams.Add($"sortOrder={Uri.EscapeDataString(sortOrder)}");

        if (categoryId.HasValue)
            queryParams.Add($"categoryId={categoryId.Value}");

        if (!string.IsNullOrEmpty(status))
            queryParams.Add($"status={Uri.EscapeDataString(status)}");

        if (!string.IsNullOrEmpty(priority))
            queryParams.Add($"priority={Uri.EscapeDataString(priority)}");

        var queryString = queryParams.Count > 0
            ? "?" + string.Join("&", queryParams)
            : string.Empty;

        var url = $"api/tickets{queryString}";

        var tickets = await _http.GetFromJsonAsync<List<TicketDto>>(url);

        return tickets ?? new List<TicketDto>();
    }
}