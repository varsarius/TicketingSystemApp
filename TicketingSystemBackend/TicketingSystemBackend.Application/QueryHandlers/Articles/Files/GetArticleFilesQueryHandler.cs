using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Application.DTOs;
using TicketingSystemBackend.Application.Interfaces;
using TicketingSystemBackend.Application.Queries.Articles.Files;

namespace TicketingSystemBackend.Application.QueryHandlers.Articles.Files;
public class GetArticleFilesQueryHandler : IRequestHandler<GetArticleFilesQuery, List<ArticleFileDto>>
{
    private readonly IFileRepository _fileRepository;
    public GetArticleFilesQueryHandler(IFileRepository fileRepository)
    {
        _fileRepository = fileRepository;
    }

    public async Task<List<ArticleFileDto>> Handle(GetArticleFilesQuery request, CancellationToken cancellationToken)
    {
        var files = await _fileRepository.GetFilesByArticleIdAsync(request.ArticleId);
        return files.Select(f => new ArticleFileDto
        {
            Id = f.Id,
            FileName = Path.GetFileName(f.Path), // just the name for display
        }).ToList();
    }
}
