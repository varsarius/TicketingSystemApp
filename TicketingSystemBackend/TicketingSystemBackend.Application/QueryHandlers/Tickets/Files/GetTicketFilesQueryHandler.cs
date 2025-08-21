using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Application.DTOs;
using TicketingSystemBackend.Application.Interfaces;
using TicketingSystemBackend.Application.Queries.Articles.Files;
using TicketingSystemBackend.Application.Queries.Tickets.Files;

namespace TicketingSystemBackend.Application.QueryHandlers.Tickets.Files;

public class GetTicketFilesQueryHandler : IRequestHandler<GetTicketFilesQuery, List<ArticleFileDto>>
{
    private readonly IFileRepository _fileRepository;
    public GetTicketFilesQueryHandler(IFileRepository fileRepository)
    {
        _fileRepository = fileRepository;
    }

    public async Task<List<ArticleFileDto>> Handle(GetTicketFilesQuery request, CancellationToken cancellationToken)
    {
        var files = await _fileRepository.GetFilesByTicketIdAsync(request.ArticleId);
        return files.Select(f => new ArticleFileDto
        {
            Id = f.Id,
            FileName = Path.GetFileName(f.Path), // just the name for display
        }).ToList();
    }
}
