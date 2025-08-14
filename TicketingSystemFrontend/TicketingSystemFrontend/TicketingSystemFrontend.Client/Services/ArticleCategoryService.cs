using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using System.Net.Http.Json;
using TicketingSystemFrontend.Client.Auth;
using TicketingSystemFrontend.Client.DTOs;
using TicketingSystemFrontend.Client.Requests;
using TicketingSystemFrontend.Client.Services.Interfaces;

namespace TicketingSystemFrontend.Client.Services;

public class ArticleCategoryService : IArticleCategoryService
{
    private readonly HttpClient _http;
    private readonly CustomAuthProvider _authenticationStateProvider;

    public ArticleCategoryService(HttpClient http, CustomAuthProvider authenticationStateProvider)
    {
        _http = http;
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task CreateAsync(ArticleCategoryCreateRequest request)
    {
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (user.Identity?.IsAuthenticated != true)
        {
            throw new Exception("User is not authenticated");
        }


        var response = await _http.PostAsJsonAsync("api/articles/categories", request);
        response.EnsureSuccessStatusCode();
    }

    public async Task<bool> DeleteByIdAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/articles/categories/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return false;
        }
        return true;
    }

    public async Task<List<ArticleCategoryDto>> GetAllAsync()
    {
        var categories = await _http.GetFromJsonAsync<List<ArticleCategoryDto>>("api/articles/categories");
        return categories ?? new List<ArticleCategoryDto>();
    }

    public async Task<ArticleCategoryDto?> GetByIdAsync(int id)
    {
        return await _http.GetFromJsonAsync<ArticleCategoryDto>($"api/articles/categories/{id}");
    }

    public async Task UpdateAsync(ArticleCategoryUpdateRequest request)
    {
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (user.Identity?.IsAuthenticated != true)
        {
            throw new Exception("User is not authenticated");
        }

        var response = await _http.PutAsJsonAsync($"api/articles/categories/{request.Id}", request);
        response.EnsureSuccessStatusCode();
    }
}