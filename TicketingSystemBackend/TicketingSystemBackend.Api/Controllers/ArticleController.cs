using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicketingSystemBackend.Application.Commands.Articles;
using TicketingSystemBackend.Application.DTOs;
using TicketingSystemBackend.Application.Queries.Articles;

namespace TicketingSystemBackend.Api.Controllers;

[ApiController]
[Route("api/articles")]
public class ArticleController : ControllerBase, IController<CreateArticleCommand, UpdateArticleCommand>
{
    private readonly IMediator _mediator;
    private readonly IWebHostEnvironment _env;

    public ArticleController(IMediator mediator, IWebHostEnvironment env)
    {
        _mediator = mediator;
        _env = env;
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateArticleCommand command)
    {
        var articleId = await _mediator.Send(command);
        return Ok(articleId);
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
        await _mediator.Send(new DeleteArticleByIdCommand(id));
        return Ok();
    }

    [HttpPost("{articleId}/files")]
    public async Task<IActionResult> UploadFiles(int articleId, [FromForm] List<IFormFile> files)
    {
        if (files == null || !files.Any())
            return BadRequest("No files provided.");

        var command = new UploadArticleFilesCommand
        {
            ArticleId = articleId,
            Files = files.ToList()
        };

        await _mediator.Send(command);
        return Ok();
    }

    [HttpGet("{articleId}/files")]
    public async Task<ActionResult<List<ArticleFileDto>>> GetArticleFiles(int articleId)
    {
        var files = await _mediator.Send(new GetArticleFilesQuery(articleId));
        return Ok(files);
    }

    [HttpGet("{articleId}/files/{fileId}")]
    public async Task<IActionResult> DownloadFile(int articleId, int fileId)
    {
        var file = await _mediator.Send(new GetArticleFileByIdQuery(articleId, fileId));

        if (file == null)
            return NotFound();

        //// Example authorization logic
        //var userId = User.FindFirst("sub")?.Value;
        //if (!User.IsInRole("Admin") && file.Article.UserId.ToString() != userId)
        //{
        //    return Forbid();
        //}


        //HERE SHOULD BE DONE IN HANDLER
        var fullPath = Path.Combine(_env.ContentRootPath, file.Path); // file.Path stores relative path
        var contentType = "application/octet-stream";

        var memory = new MemoryStream();
        using (var stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
        {
            await stream.CopyToAsync(memory);
        }
        memory.Position = 0;

        return File(memory, contentType);
    }

}