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
}

public class TicketCreateRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Priority { get; set; }
    public string AssignedTo { get; set; }
}
