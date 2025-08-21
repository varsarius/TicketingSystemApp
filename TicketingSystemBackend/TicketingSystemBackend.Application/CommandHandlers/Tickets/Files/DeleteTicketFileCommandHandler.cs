using MediatR;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Application.Commands.Articles.Files;
using TicketingSystemBackend.Application.Commands.Tickets.Files;
using TicketingSystemBackend.Application.Interfaces;

namespace TicketingSystemBackend.Application.CommandHandlers.Tickets.Files;
public class DeleteTicketFileCommandHandler : IRequestHandler<DeleteTicketFileCommand>
{
    private readonly IFileRepository _fileRepository;
    private readonly IWebHostEnvironment _env;

    public DeleteTicketFileCommandHandler(IFileRepository fileRepository, IWebHostEnvironment env)
    {
        _fileRepository = fileRepository;
        _env = env;
    }

    public async Task Handle(DeleteTicketFileCommand request, CancellationToken cancellationToken)
    {
        // 1️⃣ Find file info from DB
        var file = await _fileRepository.GetByIdAsync(request.FileId);
        if (file == null)
            throw new FileNotFoundException("File not found in database");

        // 2️⃣ Remove from DB
        await _fileRepository.DeleteAsync(file);

        // 3️⃣ Delete from disk if it exists
        var relativePath = file.Path.TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        var fullPath = Path.Combine(_env.WebRootPath, relativePath);
        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }

    }
}
