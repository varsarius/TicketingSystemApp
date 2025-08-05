using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Application.Queries.Articles;
using TicketingSystemBackend.Domain.Entities;
using TicketingSystemBackend.Infrastructure.Repositories;

namespace TicketingSystemBackend.Application.QueryHandlers.Articles;
public class GetArticlesQueryHandler : IRequestHandler<GetArticlesQuery, List<Article>>
{
    private readonly ArticleRepository _repository;

    public GetArticlesQueryHandler(ArticleRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Article>> Handle(GetArticlesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync();
    }
}
