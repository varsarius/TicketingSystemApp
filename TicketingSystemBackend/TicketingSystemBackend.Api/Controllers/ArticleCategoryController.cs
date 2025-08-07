using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicketingSystemBackend.Application.Commands.ArticleCategories;
using TicketingSystemBackend.Application.Queries.ArticleCategories;

namespace TicketingSystemBackend.Api.Controllers;

[ApiController]
[Route("api/articles/categories")]
public class ArticleCategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public ArticleCategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateArticleCategoryCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var articleCategory = await _mediator.Send(new GetArticleCategoryByIdQuery(id));
        return Ok(articleCategory);
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var articleCategories = await _mediator.Send(new GetArticleCategoriesQuery());
        return Ok(articleCategories);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateArticleCategoryCommand command)
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
    [HttpDelete]
    public async Task<IActionResult> DeleteById(int id)
    {
        await _mediator.Send(new DeleteArticleCategoryByIdCommand(id));
        return Ok();
    }
}
