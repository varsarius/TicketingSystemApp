using TicketingSystemFrontend.Client.DTOs;
using TicketingSystemFrontend.Client.Requests;

namespace TicketingSystemFrontend.Client.Services.Interfaces;

public interface IArticleCategoryService
{
    Task CreateArticleCategoryAsync(ArticleCategoryCreateRequest request);
    Task<ArticleCategoryDto?> GetArticleCategoryByIdAsync(int id);
    Task<bool> DeleteArticleCategoryAsync(int id);
    Task UpdateArticleCategoryAsync(ArticleCategoryUpdateRequest request);
    Task<List<ArticleCategoryDto>> GetAllArticleCategoriesAsync();
}


