using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicketingSystemBackend.Application.Commands.TicketComments;
using TicketingSystemBackend.Application.Queries.TicketComments;

namespace TicketingSystemBackend.Api.Controllers;
[ApiController]
[Route("/api/tickets/comments")]
public class TicketCommentController : ControllerBase, IController<CreateTicketCommentCommand, UpdateTicketCommentCommand>
{
    private readonly IMediator _mediator;

    public TicketCommentController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateTicketCommentCommand command)
    {
        var commentId = await _mediator.Send(command);
        return Ok(commentId);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteByIdAsync(int id)
    {
        await _mediator.Send(new DeleteTicketCommentByIdCommand(id));
        return Ok();
    }
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var ticketComments = await _mediator.Send(new GetTicketCommentsQuery());
        return Ok(ticketComments);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var ticketComment = await _mediator.Send(new GetTicketCommentByIdQuery(id));
        return Ok(ticketComment);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateTicketCommentCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }
}
