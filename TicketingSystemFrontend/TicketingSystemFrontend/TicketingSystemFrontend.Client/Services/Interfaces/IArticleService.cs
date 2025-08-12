using TicketingSystemFrontend.Client.DTOs;
using TicketingSystemFrontend.Client.Requests;

namespace TicketingSystemFrontend.Client.Services.Interfaces;

public interface IArticleService
{
    Task CreateArticleAsync(ArticleCreateRequest request);
    Task<List<ArticleDto>> GetAllArticlesAsync();
    Task<ArticleDto?> GetArticleByIdAsync(int id);
    Task<bool> DeleteArticleAsync(int id);
    Task UpdateArticleAsync(ArticleUpdateRequest request);
    Task<List<ArticleCategoryDto>> GetAllArticleCategoriesAsync();

}
