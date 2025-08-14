using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using TicketingSystemFrontend.Client.DTOs;
using TicketingSystemFrontend.Client.Requests;
using TicketingSystemFrontend.Client.Services.Interfaces;

namespace TicketingSystemFrontend.Client.Services;

public class ArticleService : IArticleService
{
    private readonly HttpClient _http;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly IFileUploadService _fileUploadService;

    public ArticleService(HttpClient http, AuthenticationStateProvider authenticationStateProvider, IFileUploadService fileUploadService)
    {
        _http = http;
        _authenticationStateProvider = authenticationStateProvider;
        _fileUploadService = fileUploadService;
    }
    public async Task<int> CreateAsync(ArticleCreateRequest request)
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

        request.UserId = userId;// find from JWT token claim the Id of current authorized user

        var response = await _http.PostAsJsonAsync("api/articles", request);
        response.EnsureSuccessStatusCode();

        // Read the created article ID from response (assuming API returns int)
        var articleId = await response.Content.ReadFromJsonAsync<int>();
        return articleId;
    }

    public async Task<List<ArticleDto>> GetAllAsync()
    {
        var articles = await _http.GetFromJsonAsync<List<ArticleDto>>("api/articles");
        return articles ?? new List<ArticleDto>();
    }
    public async Task<ArticleDto?> GetArticleByIdAsync(int id)
    {
        return await _http.GetFromJsonAsync<ArticleDto>($"api/articles/{id}");
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/articles/{id}");

        if (!response.IsSuccessStatusCode)
        {
            // You could throw or just return false
            // throw new Exception($"Failed to delete article with ID {id}. Status: {response.StatusCode}");
            return false;
        }

        return true;
    }
    public async Task UpdateAsync(ArticleUpdateRequest request)
    {
        var response = await _http.PutAsJsonAsync($"api/articles/{request.Id}", request);
        response.EnsureSuccessStatusCode();
    }

    // NOT IMPLEMENTED!!!! (do I need this? really?)
    public Task<ArticleDto?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task UploadFilesAsync(int articleId, List<IBrowserFile> files)
    {
        await _fileUploadService.UploadFilesAsync(articleId, files, "articles");
    }

}
