using TicketingSystemFrontend.Client.DTOs;
using TicketingSystemFrontend.Client.Requests;

namespace TicketingSystemFrontend.Client.Services.Interfaces
{
    public interface ITicketService : ICrudService<TicketDto, TicketCreateRequest, TicketUpdateRequest>
    {
        Task<List<TicketDto>> GetAllSortFilterAsync(string? sortBy = null,
            string? sortOrder = null,
            int? categoryId = null,
            string? status = null,
            string? priority = null);
    }
}
