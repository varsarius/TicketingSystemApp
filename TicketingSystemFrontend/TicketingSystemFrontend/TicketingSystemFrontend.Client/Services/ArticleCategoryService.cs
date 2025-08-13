using TicketingSystemFrontend.Client.DTOs;
using TicketingSystemFrontend.Client.Requests;
using TicketingSystemFrontend.Client.Services.Interfaces;

namespace TicketingSystemFrontend.Client.Services
{
    public class ArticleCategoryService : IArticleCategoryService
    {
        public Task CreateArticleCategoryAsync(ArticleCategoryCreateRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteArticleCategoryAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ArticleCategoryDto>> GetAllArticleCategoriesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ArticleCategoryDto?> GetArticleCategoryByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateArticleCategoryAsync(ArticleCategoryUpdateRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
