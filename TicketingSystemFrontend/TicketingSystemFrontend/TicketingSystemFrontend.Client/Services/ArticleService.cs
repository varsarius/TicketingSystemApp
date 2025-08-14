using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using System.Net.Http.Json;
using TicketingSystemFrontend.Client.Auth;
using TicketingSystemFrontend.Client.DTOs;
using TicketingSystemFrontend.Client.Requests;
using TicketingSystemFrontend.Client.Services.Interfaces;

namespace TicketingSystemFrontend.Client.Services;

public class ArticleService : IArticleService
{
    private readonly HttpClient _http;
    private readonly CustomAuthProvider _authenticationStateProvider;

    public ArticleService(HttpClient http, CustomAuthProvider authenticationStateProvider)
    {
        _http = http;
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task CreateAsync(ArticleCreateRequest request)
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

        var response = await _http.PostAsJsonAsync("api/articles", request);
        response.EnsureSuccessStatusCode();
    }

    public async Task<List<ArticleDto>> GetAllAsync()
    {
        var articles = await _http.GetFromJsonAsync<List<ArticleDto>>("api/articles");
        return articles ?? new List<ArticleDto>();
    }

    public async Task<ArticleDto?> GetByIdAsync(int id)
    {
        return await _http.GetFromJsonAsync<ArticleDto>($"api/articles/{id}");
    }

    public async Task<bool> DeleteByIdAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/articles/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return false;
        }
        return true;
    }

    public async Task UpdateAsync(ArticleUpdateRequest request)
    {
        var response = await _http.PutAsJsonAsync($"api/articles/{request.Id}", request);
        response.EnsureSuccessStatusCode();
    }
}