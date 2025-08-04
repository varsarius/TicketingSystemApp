namespace TicketingSystemFrontend.Client.Services.Interfaces;

public interface ITicketService
{
    Task CreateTicketAsync(TicketCreateRequest request);
}
