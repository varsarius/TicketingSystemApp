using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using System.Net.Http.Json;
using TicketingSystemFrontend.Client.Auth;
using TicketingSystemFrontend.Client.DTOs;
using TicketingSystemFrontend.Client.Requests;
using TicketingSystemFrontend.Client.Services.Interfaces;

namespace TicketingSystemFrontend.Client.Services;

public class TicketCommentService : ICrudService<TicketCommentDto, TicketCommentCreateRequest, TicketCommentUpdateRequest>
{
    private readonly HttpClient _http;
    private readonly CustomAuthProvider _authenticationStateProvider;

    public TicketCommentService(HttpClient http, CustomAuthProvider authenticationStateProvider)
    {
        _http = http;
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<int?> CreateAsync(TicketCommentCreateRequest request)
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
        request.UserId = userId;

        var response = await _http.PostAsJsonAsync("api/tickets/comments", request);
        response.EnsureSuccessStatusCode();

        return null; //TODO: return normall id

    }

    public async Task<List<TicketCommentDto>> GetAllAsync()
    {
        var comments = await _http.GetFromJsonAsync<List<TicketCommentDto>>("api/tickets/comments");
        return comments ?? new List<TicketCommentDto>();
    }

    public async Task<TicketCommentDto?> GetByIdAsync(int id)
    {
        return await _http.GetFromJsonAsync<TicketCommentDto>($"api/tickets/comments/{id}");
    }

    public async Task<bool> DeleteByIdAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/tickets/comments/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return false;
        }
        return true;
    }

    public async Task UpdateAsync(TicketCommentUpdateRequest request)
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
        request.UserId = userId;

        var response = await _http.PutAsJsonAsync($"api/tickets/comments/{request.Id}", request);
        response.EnsureSuccessStatusCode();
    }
}