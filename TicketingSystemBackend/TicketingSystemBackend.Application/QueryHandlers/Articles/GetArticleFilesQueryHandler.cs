using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Application.Interfaces;
using TicketingSystemBackend.Application.Queries.Articles;

namespace TicketingSystemBackend.Application.QueryHandlers.Articles;
public class GetArticleFilesQueryHandler : IRequestHandler<GetArticleFilesQuery, List<string>>
{
    private readonly IArticleFileRepository _fileRepository;
    public GetArticleFilesQueryHandler(IArticleFileRepository fileRepository)
    {
        _fileRepository = fileRepository;
    }

    public async Task<List<string>> Handle(GetArticleFilesQuery request, CancellationToken cancellationToken)
    {
        var files = await _fileRepository.GetFilesByArticleIdAsync(request.ArticleId);
        return files.Select(f => f.Path).ToList();
    }
}
