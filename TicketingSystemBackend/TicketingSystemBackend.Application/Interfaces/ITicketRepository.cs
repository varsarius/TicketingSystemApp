using TicketingSystemBackend.Domain.Entities;

namespace TicketingSystemBackend.Application.Interfaces;
public interface ITicketRepository : IRepository<Ticket>
{
    Task<Ticket> GetByTitleAsync(string title);
}
