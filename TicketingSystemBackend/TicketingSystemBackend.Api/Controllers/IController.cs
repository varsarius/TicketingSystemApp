using Microsoft.AspNetCore.Mvc;

namespace TicketingSystemBackend.Api.Controllers;
public interface IController<TCreateCommand, TUpdateCommand>
{

    Task<IActionResult> CreateAsync([FromBody] TCreateCommand command);
    Task<IActionResult> GetByIdAsync(int id);
    Task<IActionResult> GetAllAsync();
    Task<IActionResult> UpdateAsync(int id, [FromBody] TUpdateCommand command);
    Task<IActionResult> DeleteByIdAsync(int id);
}