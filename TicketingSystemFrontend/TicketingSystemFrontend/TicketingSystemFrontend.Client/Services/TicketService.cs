using System.Net.Http.Json;
using TicketingSystemFrontend.Client.Services.Interfaces;

namespace TicketingSystemFrontend.Client.Services;

public class TicketService : ITicketService
{
    private readonly HttpClient _http;

    public TicketService(HttpClient http)
    {
        _http = http;
    }

    public async Task CreateTicketAsync(TicketCreateRequest request)
    {
        var response = await _http.PostAsJsonAsync("api/tickets", request);
        response.EnsureSuccessStatusCode();
    }

    public async Task<List<TicketDto>> GetAllTicketsAsync()
    {
        var tickets = await _http.GetFromJsonAsync<List<TicketDto>>("api/tickets");
        return tickets ?? new List<TicketDto>();
    }
}

public class TicketCreateRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Priority { get; set; }
    public string AssignedTo { get; set; }
}

public class TicketDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Priority { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
