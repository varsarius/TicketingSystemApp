using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Application.Commands.Articles.Files;
using TicketingSystemBackend.Application.Commands.Tickets.Files;
using TicketingSystemBackend.Application.Interfaces;
using TicketingSystemBackend.Domain.Entities;


namespace TicketingSystemBackend.Application.CommandHandlers.Tickets.Files;
public class UploadTicketFilesCommandHandler : IRequestHandler<UploadTicketFilesCommand>
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IFileRepository _fileRepository;
    private readonly IWebHostEnvironment _env;

    public UploadTicketFilesCommandHandler(
        ITicketRepository ticketRepository,
        IFileRepository fileRepository,
        IWebHostEnvironment env)
    {
        _ticketRepository = ticketRepository;
        _fileRepository = fileRepository;
        _env = env;
    }


    public async Task Handle(UploadTicketFilesCommand request, CancellationToken cancellationToken)
    {
        var ticket = await _ticketRepository.GetByIdAsync(request.TicketId);
        if (ticket == null)
            throw new Exception("Ticket not found");

        if (request.Files == null || !request.Files.Any())
            throw new Exception("No files provided");

        var webRoot = _env.WebRootPath ?? Path.Combine(_env.ContentRootPath, "wwwroot");
        var uploadsFolder = Path.Combine(webRoot, "uploads", "tickets", request.TicketId.ToString());

        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        foreach (var file in request.Files)
        {
            var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream, cancellationToken);

            var fileEntity = new Domain.Entities.File
            {
                Path = $"/uploads/tickets/{request.TicketId}/{uniqueFileName}"
            };
            Console.WriteLine($"File saved to: {fileEntity.Path}");

            fileEntity.Tickets.Add(ticket); // do not assign a new list

            await _fileRepository.AddAsync(fileEntity, cancellationToken);
        }

        await _fileRepository.SaveChangesAsync(cancellationToken);


    }
}
