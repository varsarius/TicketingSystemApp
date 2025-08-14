using TicketingSystemFrontend.Client.DTOs;
using TicketingSystemFrontend.Client.Requests;
namespace TicketingSystemFrontend.Client.Services.Interfaces;

public interface ITicketService : ICrudService<TicketDto, TicketCreateRequest, TicketUpdateRequest>
{

}
