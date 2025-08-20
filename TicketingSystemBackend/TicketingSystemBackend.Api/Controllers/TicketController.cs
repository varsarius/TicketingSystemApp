using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicketingSystemBackend.Application.Commands.Tickets;
using TicketingSystemBackend.Application.DTOs;
using TicketingSystemBackend.Application.Queries.Tickets;
namespace TicketingSystemBackend.Api.Controllers;


[ApiController]
[Route("api/tickets")]
public class TicketController : ControllerBase, IController<CreateTicketCommand, UpdateTicketCommand>
{
    private readonly IMediator _mediator;

    public TicketController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateTicketCommand command)
    {
        var ticketId = await _mediator.Send(command);
        return Ok(ticketId);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var ticket = await _mediator.Send(new GetTicketByIdQuery(id));
        return Ok(ticket);
    }



    [HttpGet]
    public async Task<IActionResult> GetAllSortFilterAsync(
        [FromQuery] string? sortBy,
        [FromQuery] string? sortOrder,
        [FromQuery] int? categoryId,
        [FromQuery] string? status,
        [FromQuery] string? priority)
    {

        var query = new GetTicketsQuery(
            sortBy,
            sortOrder,
            categoryId,
            status,
            priority
        );

        var tickets = await _mediator.Send(query);
        return Ok(tickets);
    }



    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateTicketCommand command)
    {
        if (id != command.Id)
            return BadRequest("Id of the update request mismatches the id of the body");
        try
        {
            await _mediator.Send(command);
            return Ok();

        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteByIdAsync(int id)
    {
        await _mediator.Send(new DeleteTicketByIdCommand(id));
        return Ok();
    }

    [HttpGet]
    [Route("allall")]
    public async Task<IActionResult> GetAllAsync()
    {
        var tickets = await _mediator.Send(new GetTicketsQuery());
        return Ok(tickets);
    }
}
