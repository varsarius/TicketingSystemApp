using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Application.Interfaces;
using TicketingSystemBackend.Application.Queries.Articles;

namespace TicketingSystemBackend.Application.QueryHandlers.Articles;
public class GetArticleFileByIdQueryHandler : IRequestHandler<GetArticleFileByIdQuery, Domain.Entities.File?>
{
    private readonly IArticleFileRepository _fileRepository;

    public GetArticleFileByIdQueryHandler(IArticleFileRepository articleFileRepository)
    {
        _fileRepository = articleFileRepository;
    }

    public async Task<Domain.Entities.File?> Handle(GetArticleFileByIdQuery request, CancellationToken cancellationToken)
    {
        return await _fileRepository.GetFileByIdAndArticleIdAsync(request.FileId, request.ArticleId, cancellationToken);
    }
}
