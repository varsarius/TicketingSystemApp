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
public class GetArticleCategoryByIdQueryHandler : IRequestHandler<GetArticleCategoryByIdQuery, ArticleCategory>
{
    public readonly ArticleCategoryRepository _repository;

    public GetArticleCategoryByIdQueryHandler(ArticleCategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<ArticleCategory> Handle(GetArticleCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.Id);
    }
}
