using MediatR;
using TicketingSystemBackend.Application.Interfaces;
using TicketingSystemBackend.Application.Queries.ArticleCategories;
using TicketingSystemBackend.Domain.Entities;

namespace TicketingSystemBackend.Application.QueryHandlers.ArticleCategories;
public class GetArticleCategoryByIdQueryHandler : IRequestHandler<GetArticleCategoryByIdQuery, ArticleCategory>
{
    public readonly IArticleCategoryRepository _repository;

    public GetArticleCategoryByIdQueryHandler(IArticleCategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<ArticleCategory> Handle(GetArticleCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.Id);
    }
}
