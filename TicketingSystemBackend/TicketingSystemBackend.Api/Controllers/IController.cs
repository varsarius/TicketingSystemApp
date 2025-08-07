using Microsoft.AspNetCore.Mvc;

namespace TicketingSystemBackend.Api.Controllers;
public interface IController<TCreateCommand, TUpdateCommand>
{

    Task<IActionResult> Create([FromBody] TCreateCommand command);
    Task<IActionResult> GetById(int id);
    Task<IActionResult> GetAll();
    Task<IActionResult> Update(int id, [FromBody] TUpdateCommand command);
    Task<IActionResult> DeleteById(int id);
}