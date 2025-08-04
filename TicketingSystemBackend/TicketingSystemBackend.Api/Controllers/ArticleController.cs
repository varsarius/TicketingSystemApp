using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicketingSystemBackend.Api.Commands.Articles;

namespace TicketingSystemBackend.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ArticleController : ControllerBase
{
    private readonly IMediator _mediator;

    public ArticleController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateArticleCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }

}
