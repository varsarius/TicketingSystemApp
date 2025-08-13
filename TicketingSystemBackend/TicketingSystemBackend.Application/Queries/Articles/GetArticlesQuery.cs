using MediatR;
using TicketingSystemBackend.Application.DTOs;
using TicketingSystemBackend.Domain.Entities;

namespace TicketingSystemBackend.Application.Queries.Articles;

public record GetArticlesQuery : IRequest<List<ArticleDto>>;
