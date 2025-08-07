namespace TicketingSystemBackend.Application.Interfaces;
public interface IRepository<T> where T : class
{
    Task CreateAsync(T entity);
    Task DeleteByIdAsync(int id);
    Task<List<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task UpdateAsync(T entity);
}