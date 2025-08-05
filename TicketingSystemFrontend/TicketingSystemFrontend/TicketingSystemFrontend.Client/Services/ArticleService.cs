using System.Net.Http;
using System.Net.Http.Json;
using TicketingSystemFrontend.Client.DTOs;
using TicketingSystemFrontend.Client.Requests;
using TicketingSystemFrontend.Client.Services.Interfaces;

namespace TicketingSystemFrontend.Client.Services;

public class ArticleService : IArticleService
{
    private readonly HttpClient _http;

    public ArticleService(HttpClient http)
    {
        _http = http;
    }
    public async Task CreateArticleAsync(ArticleCreateRequest request)
    {
        var response = await _http.PostAsJsonAsync("api/articles", request);
        response.EnsureSuccessStatusCode();
    }

    public async Task<List<ArticleDto>> GetAllArticlesAsync()
    {
        var articles = await _http.GetFromJsonAsync<List<ArticleDto>>("api/articles");
        return articles ?? new List<ArticleDto>();
    }
    public async Task<ArticleDto?> GetArticleByIdAsync(int id)
    {
        return await _http.GetFromJsonAsync<ArticleDto>($"api/articles/{id}");
    }

    public async Task<bool> DeleteArticleAsync(int id)
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
    public async Task UpdateArticleAsync(int id, ArticleCreateRequest request)
    {
        var response = await _http.PutAsJsonAsync($"api/articles/{id}", request);
        response.EnsureSuccessStatusCode();
    }

}
