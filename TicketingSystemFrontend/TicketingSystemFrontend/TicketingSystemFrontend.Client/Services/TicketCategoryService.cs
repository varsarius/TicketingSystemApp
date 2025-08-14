using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using System.Net.Http.Json;
using TicketingSystemFrontend.Client.Auth;
using TicketingSystemFrontend.Client.DTOs;
using TicketingSystemFrontend.Client.Requests;
using TicketingSystemFrontend.Client.Services.Interfaces;

namespace TicketingSystemFrontend.Client.Services;

public class TicketCategoryService : ITicketCategoryService
{
    private readonly HttpClient _http;
    private readonly CustomAuthProvider _authenticationStateProvider;

    public TicketCategoryService(HttpClient http, CustomAuthProvider authenticationStateProvider)
    {
        _http = http;
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<int?> CreateAsync(TicketCategoryCreateRequest request)
    {
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (user.Identity?.IsAuthenticated != true)
        {
            throw new Exception("User is not authenticated");
        }

        var response = await _http.PostAsJsonAsync("api/tickets/categories", request);
        response.EnsureSuccessStatusCode();

        return null; //TODO: return normall id
    }

    public async Task<bool> DeleteByIdAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/tickets/categories/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return false;
        }
        return true;
    }

    public async Task<List<TicketCategoryDto>> GetAllAsync()
    {
        var categories = await _http.GetFromJsonAsync<List<TicketCategoryDto>>("api/tickets/categories");
        return categories ?? new List<TicketCategoryDto>();
    }

    public async Task<TicketCategoryDto?> GetByIdAsync(int id)
    {
        return await _http.GetFromJsonAsync<TicketCategoryDto>($"api/tickets/categories/{id}");
    }

    public async Task UpdateAsync(TicketCategoryUpdateRequest request)
    {
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (user.Identity?.IsAuthenticated != true)
        {
            throw new Exception("User is not authenticated");
        }

        var response = await _http.PutAsJsonAsync($"api/tickets/categories/{request.Id}", request);
        response.EnsureSuccessStatusCode();
    }
}