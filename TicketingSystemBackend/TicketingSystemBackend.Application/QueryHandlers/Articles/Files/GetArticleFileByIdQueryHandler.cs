using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Application.Interfaces;
using TicketingSystemBackend.Application.Queries.Articles.Files;

namespace TicketingSystemBackend.Application.QueryHandlers.Articles.Files;
public class GetArticleFileByIdQueryHandler : IRequestHandler<GetArticleFileByIdQuery, Domain.Entities.File?>
{
    private readonly IFileRepository _fileRepository;

    public GetArticleFileByIdQueryHandler(IFileRepository fileRepository)
    {
        _fileRepository = fileRepository;
    }

    public async Task<Domain.Entities.File?> Handle(GetArticleFileByIdQuery request, CancellationToken cancellationToken)
    {
        return await _fileRepository.GetFileByIdAndArticleIdAsync(request.FileId, request.ArticleId, cancellationToken);
    }
}
