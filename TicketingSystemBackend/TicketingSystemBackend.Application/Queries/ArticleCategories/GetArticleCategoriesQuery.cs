using MediatR;
using TicketingSystemBackend.Domain.Entities;

namespace TicketingSystemBackend.Application.Queries.ArticleCategories;
public record GetArticleCategoriesQuery : IRequest<List<ArticleCategory>>;