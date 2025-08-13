using MediatR;
using TicketingSystemBackend.Application.DTOs;
using TicketingSystemBackend.Application.Interfaces;
using TicketingSystemBackend.Application.Queries.Articles;
using TicketingSystemBackend.Domain.Entities;

namespace TicketingSystemBackend.Application.QueryHandlers.Articles;
public class GetArticlesQueryHandler : IRequestHandler<GetArticlesQuery, List<ArticleDto>>
{
    private readonly IArticleReadRepository _repository;

    public GetArticlesQueryHandler(IArticleReadRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ArticleDto>> Handle(GetArticlesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync();
    }
}
