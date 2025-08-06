using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicketingSystemBackend.Application.Commands.Articles;
using TicketingSystemBackend.Application.Queries.Articles;

namespace TicketingSystemBackend.Api.Controllers;

[ApiController]
[Route("api/articles")]
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
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateArticleCommand command)
    {
        if (id != command.Id)
            return BadRequest("Id of the update request does not match the id of the body");
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
        await _mediator.Send(new DeleteArticleByIdCommand(id));
        return Ok();
    }
}
