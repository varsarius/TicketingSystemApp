using MediatR;
using TicketingSystemBackend.Application.Interfaces;
using TicketingSystemBackend.Application.Queries.Articles;
using TicketingSystemBackend.Domain.Entities;

namespace TicketingSystemBackend.Application.QueryHandlers.Articles;
public class GetArticlesQueryHandler : IRequestHandler<GetArticlesQuery, List<Article>>
{
    private readonly IArticleRepository _repository;

    public GetArticlesQueryHandler(IArticleRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Article>> Handle(GetArticlesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync();
    }
}
