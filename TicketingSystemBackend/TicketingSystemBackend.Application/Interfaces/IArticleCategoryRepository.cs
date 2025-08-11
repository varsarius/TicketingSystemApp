using TicketingSystemBackend.Domain.Entities;

namespace TicketingSystemBackend.Application.Interfaces;
public interface IArticleCategoryRepository : IRepository<ArticleCategory>
{
    Task<ArticleCategory> GetByCategoryNameAsync(string categoryName);
}
