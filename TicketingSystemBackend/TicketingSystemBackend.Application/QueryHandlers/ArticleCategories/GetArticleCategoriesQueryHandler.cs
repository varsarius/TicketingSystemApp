using MediatR;
using TicketingSystemBackend.Application.Interfaces;
using TicketingSystemBackend.Application.Queries.ArticleCategories;
using TicketingSystemBackend.Domain.Entities;

namespace TicketingSystemBackend.Application.QueryHandlers.ArticleCategories;
public class GetArticleCategoriesQueryHandler : IRequestHandler<GetArticleCategoriesQuery, List<ArticleCategory>>
{
    private readonly IArticleCategoryRepository _repository;

    public GetArticleCategoriesQueryHandler(IArticleCategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ArticleCategory>> Handle(GetArticleCategoriesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync();
    }
}
