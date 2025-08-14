using System.Net.Http.Json;
using TicketingSystemFrontend.Client.DTOs;
using TicketingSystemFrontend.Client.Requests;
using TicketingSystemFrontend.Client.Services.Interfaces;

namespace TicketingSystemFrontend.Client.Services;

public class TicketService : ITicketService
{
    private readonly HttpClient _http;

    public TicketService(HttpClient http)
    {
        _http = http;
    }

    public async Task CreateAsync(TicketCreateRequest request)
    {
        var response = await _http.PostAsJsonAsync("api/tickets", request);
        response.EnsureSuccessStatusCode();
    }

    public Task<bool> DeleteByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<TicketDto>> GetAllAsync()
    {
        try
        {
            var tickets = await _http.GetFromJsonAsync<List<TicketDto>>("api/tickets");
            return tickets ?? new List<TicketDto>();
        } 
        catch
        {
            return new List<TicketDto>();
        }
        
    }

    public Task<TicketDto?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(TicketUpdateRequest request)
    {
        throw new NotImplementedException();
    }
}
