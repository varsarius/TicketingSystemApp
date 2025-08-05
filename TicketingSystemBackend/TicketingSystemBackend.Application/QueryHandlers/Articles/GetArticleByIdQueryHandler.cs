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
public class GetArticleByIdQueryHandler : IRequestHandler<GetArticleByIdQuery, Article>
{
    private readonly ArticleRepository _repository;

    public GetArticleByIdQueryHandler(ArticleRepository repository)
    {
        _repository = repository;
    }

    public async Task<Article> Handle(GetArticleByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.Id);
    }
}
