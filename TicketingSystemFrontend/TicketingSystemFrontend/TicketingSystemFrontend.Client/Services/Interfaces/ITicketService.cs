using TicketingSystemFrontend.Client.DTOs;
using TicketingSystemFrontend.Client.Requests;
using TicketingSystemFrontend.Client.Services.Extensions;

namespace TicketingSystemFrontend.Client.Services.Interfaces
{
    public interface ITicketService : ICrudService<TicketDto, TicketCreateRequest, TicketUpdateRequest>
        , IFileEntityService
    {
        Task<List<TicketDto>> GetAllSortFilterAsync(string? sortBy = null,
            string? sortOrder = null,
            int? categoryId = null,
            string? status = null,
            string? priority = null);
    }
}
