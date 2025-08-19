using TicketingSystemFrontend.Client.DTOs;
using TicketingSystemFrontend.Client.Requests;

namespace TicketingSystemFrontend.Client.Services.Interfaces
{
    public interface ITicketService : ICrudService<TicketDto, TicketCreateRequest, TicketUpdateRequest>
    {
        Task<List<TicketCommentDto>> GetCommentsByTicketIdAsync(int ticketId);
        Task<bool> AddCommentAsync(int ticketId, TicketCommentCreateRequest request);
        Task<bool> UpdateCommentAsync(int commentId, TicketCommentUpdateRequest request);
        Task<bool> DeleteCommentAsync(int commentId);
    }
}
