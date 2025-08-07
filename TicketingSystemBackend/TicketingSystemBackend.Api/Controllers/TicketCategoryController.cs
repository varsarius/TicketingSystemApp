using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicketingSystemBackend.Application.Commands.TicketCategories;
using TicketingSystemBackend.Application.Queries.TicketCategories;

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
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTicketCategoryCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteById(int id)
    {
        await _mediator.Send(new DeleteTicketCategoryByIdCommand(id));
        return Ok();
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var ticketCategories = await _mediator.Send(new GetTicketCategoriesQuery());
        return Ok(ticketCategories);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var ticketCategory = await _mediator.Send(new GetTicketCategoryByIdQuery(id));
        return Ok(ticketCategory);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTicketCategoryCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }
}
