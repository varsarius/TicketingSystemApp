using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Application.Queries.ArticleCategories;
using TicketingSystemBackend.Domain.Entities;
using TicketingSystemBackend.Infrastructure.Repositories;

namespace TicketingSystemBackend.Application.QueryHandlers.ArticleCategories;
public class GetArticleCategoriesQueryHandler : IRequestHandler<GetArticleCategoriesQuery, List<ArticleCategory>>
{
    private readonly ArticleCategoryRepository _repository;

    public GetArticleCategoriesQueryHandler(ArticleCategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ArticleCategory>> Handle(GetArticleCategoriesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync();
    }
}
