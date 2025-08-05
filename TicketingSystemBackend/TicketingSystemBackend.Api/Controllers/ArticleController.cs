using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicketingSystemBackend.Application.Commands.Articles;
using TicketingSystemBackend.Application.Queries.Articles;

namespace TicketingSystemBackend.Api.Controllers;

[Route("api/articles")]
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
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var article = await _mediator.Send(new GetArticleByIdQuery(id));
        return Ok(article);
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var articles = await _mediator.Send(new GetArticlesQuery());
        return Ok(articles);
    }
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateArticleCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteById(int id)
    {
        await _mediator.Send(new DeleteArticleByIdCommand(id));
        return Ok();
    }
}
