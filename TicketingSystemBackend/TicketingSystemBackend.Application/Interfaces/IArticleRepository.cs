using TicketingSystemBackend.Domain.Entities;

namespace TicketingSystemBackend.Application.Interfaces;
public interface IArticleRepository : IRepository<Article>
{
    Task<Article> GetByTitleAsync(string title);
}
