using TicketingSystemFrontend.Client.DTOs;
using TicketingSystemFrontend.Client.Requests;

namespace TicketingSystemFrontend.Client.Services.Interfaces;

public interface ITicketCommentService : ICrudService<TicketCommentDto, TicketCommentCreateRequest, TicketCommentUpdateRequest>
{
    Task<bool> DeleteCommentAsync(int ticketId, int commentId);
    Task<List<TicketCommentDto>> GetCommentsByTicketIdAsync(int ticketId);
   // Task<bool> AddCommentAsync(int ticketId, TicketCommentCreateRequest request);
    Task<bool> UpdateCommentAsync(int commentId, TicketCommentUpdateRequest request);


    //Task<bool> DeleteCommentAsync(int commentId);

}
