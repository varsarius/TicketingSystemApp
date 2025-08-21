using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using TicketingSystemBackend.Application.Commands.Articles.Files;
using TicketingSystemBackend.Application.Commands.Tickets;
using TicketingSystemBackend.Application.Commands.Tickets.Files;
using TicketingSystemBackend.Application.DTOs;
using TicketingSystemBackend.Application.Queries.Articles.Files;
using TicketingSystemBackend.Application.Queries.Tickets;
using TicketingSystemBackend.Application.Queries.Tickets.Files;
namespace TicketingSystemBackend.Api.Controllers;


[ApiController]
[Route("api/tickets")]
public class TicketController : ControllerBase, IController<CreateTicketCommand, UpdateTicketCommand>
{
    private readonly IMediator _mediator;
    private readonly IWebHostEnvironment _env;

    public TicketController(IMediator mediator, IWebHostEnvironment env)
    {
        _mediator = mediator;
        _env = env;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateTicketCommand command)
    {
        var ticketId = await _mediator.Send(command);
        return Ok(ticketId);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var ticket = await _mediator.Send(new GetTicketByIdQuery(id));
        return Ok(ticket);
    }



    [HttpGet]
    public async Task<IActionResult> GetAllSortFilterAsync(
        [FromQuery] string? sortBy,
        [FromQuery] string? sortOrder,
        [FromQuery] int? categoryId,
        [FromQuery] string? status,
        [FromQuery] string? priority)
    {

        var query = new GetTicketsQuery(
            sortBy,
            sortOrder,
            categoryId,
            status,
            priority
        );

        var tickets = await _mediator.Send(query);
        return Ok(tickets);
    }



    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateTicketCommand command)
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
    public async Task<IActionResult> DeleteByIdAsync(int id)
    {
        await _mediator.Send(new DeleteTicketByIdCommand(id));
        return Ok();
    }

    [HttpGet]
    [Route("allall")]
    public async Task<IActionResult> GetAllAsync()
    {
        var tickets = await _mediator.Send(new GetTicketsQuery());
        return Ok(tickets);
    }


    [HttpPost("{ticketId}/files")]
    public async Task<IActionResult> UploadFiles(int ticketId, [FromForm] List<IFormFile> files)
    {
        Console.WriteLine("file creation controller called for uploading on ticket");

        if (files == null || !files.Any())
            return BadRequest("No files provided.");

        var command = new UploadTicketFilesCommand
        {
            TicketId = ticketId,
            Files = files.ToList()
        };


        try
        {
            await _mediator.Send(command);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.StackTrace);
            throw;
        }
        
        return Ok();
    }

    [HttpGet("{ticketId}/files")]
    public async Task<ActionResult<List<ArticleFileDto>>> GetArticleFiles(int ticketId)
    {
        var files = await _mediator.Send(new GetTicketFilesQuery(ticketId));
        return Ok(files);
    }

    [HttpGet("{ticketId}/files/{fileId}")]
    public async Task<IActionResult> DownloadFile(int ticketId, int fileId)
    {
        var file = await _mediator.Send(new GetTicketFileByIdQuery(ticketId, fileId));

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

    [HttpDelete("{ticketId}/files/{fileId}")]
    public async Task<IActionResult> DeleteFile(int ticketId, int fileId)
    {
        try
        {
            await _mediator.Send(new DeleteTicketFileCommand(ticketId, fileId));
            return Ok();
        }
        catch (FileNotFoundException)
        {
            return NotFound($"File with ID {fileId} not found for article {ticketId}.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
