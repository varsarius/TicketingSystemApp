using System.Net.Http.Json;
using TicketingSystemFrontend.Client.DTOs;
using TicketingSystemFrontend.Client.Requests;
using TicketingSystemFrontend.Client.Services.Interfaces;

namespace TicketingSystemFrontend.Client.Services;

public class TicketCategoryService : ITicketCategoryService
{
    private readonly HttpClient _http;

    public TicketCategoryService(HttpClient http)
    {
        _http = http;
    }

    public async Task CreateAsync(TicketCategoryCreateRequest request)
    {
        var response = await _http.PostAsJsonAsync("api/tickets/categories", request);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Failed to create ticket category. Status: {response.StatusCode}, Error: {error}");
        }
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
        var response = await _http.PutAsJsonAsync($"api/tickets/categories/{request.Id}", request);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Failed to update ticket category. Status: {response.StatusCode}, Error: {error}");
        }
    }
}
