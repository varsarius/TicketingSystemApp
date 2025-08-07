using MediatR;
using TicketingSystemBackend.Application.Interfaces;
using TicketingSystemBackend.Application.Queries.Articles;
using TicketingSystemBackend.Domain.Entities;

namespace TicketingSystemBackend.Application.QueryHandlers.Articles;
public class GetArticleByIdQueryHandler : IRequestHandler<GetArticleByIdQuery, Article>
{
    private readonly IArticleRepository _repository;

    public GetArticleByIdQueryHandler(IArticleRepository repository)
    {
        _repository = repository;
    }

    public async Task<Article> Handle(GetArticleByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.Id);
    }
}
