using TicketingSystemFrontend.Client.DTOs;
using TicketingSystemFrontend.Client.Requests;

namespace TicketingSystemFrontend.Client.Services.Interfaces;

public interface ITicketCategoryService
{
    Task CreateTicketCategoryAsync(TicketCategoryCreateRequest request);
    Task<List<TicketCategoryDto>> GetAllTicketCategoriesAsync();
    Task<TicketCategoryDto?> GetTicketCategoryByIdAsync(int id);
    Task<bool> DeleteTicketCategoryAsync(int id);
    Task UpdateTicketCategoryAsync(TicketCategoryUpdateRequest request);
}
