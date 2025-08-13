using MediatR;
using TicketingSystemBackend.Application.DTOs;
using TicketingSystemBackend.Application.Interfaces;
using TicketingSystemBackend.Application.Queries.Articles;
using TicketingSystemBackend.Domain.Entities;

namespace TicketingSystemBackend.Application.QueryHandlers.Articles;
public class GetArticleByIdQueryHandler : IRequestHandler<GetArticleByIdQuery, ArticleDto>
{
    private readonly IArticleReadRepository _repository;

    public GetArticleByIdQueryHandler(IArticleReadRepository repository)
    {
        _repository = repository;
    }

    public async Task<ArticleDto> Handle(GetArticleByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.Id);
    }
}
