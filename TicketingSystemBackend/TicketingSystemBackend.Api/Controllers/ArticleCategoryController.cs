using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicketingSystemBackend.Application.Commands.ArticleCategories;

namespace TicketingSystemBackend.Api.Controllers;

[ApiController]
[Route("api/categories/articles")]
public class ArticleCategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public ArticleCategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> Create([FromBody] CreateArticleCategoryCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }
    public async Task<IActionResult> GetById(int id)
    {
        var articleCategory = await _mediator.Send(new GetArticleCategoryByIdQuery(id));
        return Ok(articleCategory);
    }
    public async Task<IActionResult> GetAll()
    {
        var articleCategories = await _mediator.Send(new GetArticleCategoriesQuery());
        return Ok(articleCategories);
    }
    public async Task<IActionResult> Update([FromBody] UpdateArticleCategoryCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }
    public async Task<IActionResult> DeleteById(int id)
    {
        await _mediator.Send(new DeleteArticleCategoryByIdCommand(id));
        return Ok();
    }
}
