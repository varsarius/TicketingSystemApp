using MediatR;
using TicketingSystemBackend.Domain.Entities;

namespace TicketingSystemBackend.Application.Queries.ArticleCategories;
public record GetArticleCategoryByIdQuery(int Id) : IRequest<ArticleCategory>;