using TicketingSystemFrontend.Client.DTOs;
using TicketingSystemFrontend.Client.Requests;
namespace TicketingSystemFrontend.Client.Services.Interfaces;

public interface ITicketService
{
    Task CreateTicketAsync(TicketCreateRequest request);
    Task<List<TicketDto>> GetAllTicketsAsync();
}
