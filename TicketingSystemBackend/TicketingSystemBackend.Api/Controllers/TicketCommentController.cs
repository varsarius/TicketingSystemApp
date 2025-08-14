using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicketingSystemBackend.Application.Commands.TicketComments;
using TicketingSystemBackend.Application.Queries.TicketComments;

namespace TicketingSystemBackend.Api.Controllers;
[ApiController]
[Route("/api/tickets/comments")]
public class TicketCommentController : ControllerBase, IController<CreateTicketCommentCommand, UpdateTicketCommentCommand>
{
    private IMediator _mediator;

    public TicketCommentController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTicketCommentCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteById(int id)
    {
        await _mediator.Send(new DeleteTicketCommentByIdCommand(id));
        return Ok();
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var ticketComments = await _mediator.Send(new GetTicketCommentsQuery());
        return Ok(ticketComments);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var ticketComment = await _mediator.Send(new GetTicketCommentByIdQuery(id));
        return Ok(ticketComment);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTicketCommentCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }
}
