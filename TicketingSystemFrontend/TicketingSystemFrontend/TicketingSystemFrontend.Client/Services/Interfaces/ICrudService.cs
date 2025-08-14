namespace TicketingSystemFrontend.Client.Services.Interfaces;

public interface ICrudService<T, TCreateRequest, TUpdateRequest>
    where T : class
{
    Task<int?> CreateAsync(TCreateRequest request);
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<bool> DeleteByIdAsync(int id);
    Task UpdateAsync(TUpdateRequest request);

}
