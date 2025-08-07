using Microsoft.AspNetCore.Mvc;
using TicketingSystemBackend.Application.Commands.Articles;

namespace TicketingSystemBackend.Api.Controllers;
public interface IController<TCreateCommand, TUpdateComman>
{

    Task<IActionResult> Create([FromBody] TCreateCommand command);
    Task<IActionResult> GetById(int id);
    Task<IActionResult> GetAll();
    Task<IActionResult> Update(int id, [FromBody] TUpdateComman command);
    Task<IActionResult> DeleteById(int id);
}