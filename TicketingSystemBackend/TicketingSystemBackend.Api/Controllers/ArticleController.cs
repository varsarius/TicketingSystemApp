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

        var relativePath = file.Path.TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        var fullPath = Path.Combine(_env.WebRootPath, relativePath);

        if (!System.IO.File.Exists(fullPath))
            return NotFound();

        // Detect content type
        var contentType = GetContentType(fullPath);

        // Return physical file with name
        return PhysicalFile(fullPath, contentType, Path.GetFileName(fullPath));
    }

    // Detects MIME type from file extension
    private static string GetContentType(string path)
    {
        var provider = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();
        if (!provider.TryGetContentType(path, out var contentType))
        {
            contentType = "application/octet-stream"; // fallback
        }
        return contentType;
    }

}