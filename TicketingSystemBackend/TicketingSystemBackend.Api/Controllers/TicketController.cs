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
    public async Task<IActionResult> Create([FromBody] CreateTicketCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var ticket = await _mediator.Send(new GetTicketByIdQuery(id));
        return Ok(ticket);
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tickets = await _mediator.Send(new GetTicketsQuery());
        return Ok(tickets);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTicketCommand command)
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
    public async Task<IActionResult> DeleteById(int id)
    {
        await _mediator.Send(new DeleteTicketByIdCommand(id));
        return Ok();
    }

}
