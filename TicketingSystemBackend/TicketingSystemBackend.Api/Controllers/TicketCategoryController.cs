using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TicketingSystemBackend.Api.Controllers;
[ApiController]
[Route("/api/tickets/categories")]
public class TicketCategoryController : ControllerBase, IController<CreateTicketCategoryCommand, UpdateTicketCategoryCommand>
{
    private readonly IMediator _mediator;

    public TicketCategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> Create([FromBody] CreateTicketCategoryCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }

    public async Task<IActionResult> DeleteById(int id)
    {
        await _mediator.Send(new DeleteTicketCategoryByIdCommand(id));
        return Ok();
    }

    public async Task<IActionResult> GetAll()
    {
        var ticketCategories = await _mediator.Send(new GetTicketCategoriesQuery(id));
        return Ok(ticketCategories);
    }

    public async Task<IActionResult> GetById(int id)
    {
        var ticketCategory = await _mediator.Send(new GetTicketCategoryQuery(id));
        return Ok(ticketCategory);
    }

    public async Task<IActionResult> Update(int id, [FromBody] UpdateTicketCategoryCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }
}
