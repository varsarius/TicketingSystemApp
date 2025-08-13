using TicketingSystemFrontend.Client.DTOs;
using TicketingSystemFrontend.Client.Requests.Commands.Articles;

namespace TicketingSystemFrontend.Client.Services.Interfaces;

public interface ICrudService<T, TCreateCommand, TUpdateCommand>
    where T : class
{
    Task CreateAsync(TCreateCommand request);
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<bool> DeleteAsync(int id);
    Task UpdateAsync(TUpdateCommand request);

}
