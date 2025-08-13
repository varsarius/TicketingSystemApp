using TicketingSystemFrontend.Client.DTOs;
using TicketingSystemFrontend.Client.Requests.Commands.Tickets;
namespace TicketingSystemFrontend.Client.Services.Interfaces;

public interface ITicketCrudService : ICrudService<TicketDto, CreateTicketCommand, UpdateTicketCommand>
{

}
