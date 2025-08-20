using MediatR;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Application.Commands.Articles;
using TicketingSystemBackend.Application.Interfaces;

namespace TicketingSystemBackend.Application.CommandHandlers.Articles;
public class DeleteArticleFileCommandHandler : IRequestHandler<DeleteArticleFileCommand>
{
    private readonly IFileRepository _fileRepository;
    private readonly IWebHostEnvironment _env;

    public DeleteArticleFileCommandHandler(IFileRepository fileRepository, IWebHostEnvironment env)
    {
        _fileRepository = fileRepository;
        _env = env;
    }

    public async Task Handle(DeleteArticleFileCommand request, CancellationToken cancellationToken)
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
