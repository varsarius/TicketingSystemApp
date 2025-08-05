using MediatR;
using TicketingSystemBackend.Domain.Entities;

namespace TicketingSystemBackend.Application.Queries.Articles;

public record GetArticlesQuery : IRequest<List<Article>>;
