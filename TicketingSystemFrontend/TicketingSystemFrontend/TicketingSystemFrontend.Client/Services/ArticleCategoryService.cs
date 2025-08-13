using System.Net.Http.Json;
using TicketingSystemFrontend.Client.DTOs;
using TicketingSystemFrontend.Client.Requests;
using TicketingSystemFrontend.Client.Services.Interfaces;

namespace TicketingSystemFrontend.Client.Services
{
    public class ArticleCategoryService : IArticleCategoryService
    {
        private readonly HttpClient _http;

        public ArticleCategoryService(HttpClient http)
        {
            _http = http;
        }

        public async Task CreateArticleCategoryAsync(ArticleCategoryCreateRequest request)
        {
            var response = await _http.PostAsJsonAsync("api/articles/categories", request);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Failed to create article category. Status: {response.StatusCode}, Error: {error}");
            }
        }

        public async Task<bool> DeleteArticleCategoryAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/articles/categories/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            return true;
        }

        public async Task<List<ArticleCategoryDto>> GetAllArticleCategoriesAsync()
        {
            var categories = await _http.GetFromJsonAsync<List<ArticleCategoryDto>>("api/articles/categories");
            return categories ?? new List<ArticleCategoryDto>();
        }

        public async Task<ArticleCategoryDto?> GetArticleCategoryByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<ArticleCategoryDto>($"api/articles/categories/{id}");
        }

        public async Task UpdateArticleCategoryAsync(ArticleCategoryUpdateRequest request)
        {
            var response = await _http.PutAsJsonAsync($"api/articles/categories/{request.Id}", request);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Failed to update article category. Status: {response.StatusCode}, Error: {error}");
            }
        }
    }
}
