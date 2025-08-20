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

    public async Task<List<TicketCommentDto>> GetCommentsByTicketIdAsync(int ticketId)
    {
        var comments = await _http.GetFromJsonAsync<List<TicketCommentDto>>(
            $"api/tickets/{ticketId}/comments"
        );
        return comments ?? new List<TicketCommentDto>();
    }

    public async Task<bool> AddCommentAsync(int ticketId, TicketCommentCreateRequest request)
    {
        var response = await _http.PostAsJsonAsync($"api/tickets/{ticketId}/comments", request);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteCommentAsync(int commentId)
    {
        var response = await _http.DeleteAsync($"api/tickets/comments/{commentId}");
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateCommentAsync(int commentId, TicketCommentUpdateRequest request)
    {
        var response = await _http.PutAsJsonAsync($"api/tickets/comments/{commentId}", request);
        return response.IsSuccessStatusCode;
    }
}