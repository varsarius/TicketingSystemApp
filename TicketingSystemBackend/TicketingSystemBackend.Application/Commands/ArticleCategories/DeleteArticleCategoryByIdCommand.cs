using MediatR;

namespace TicketingSystemBackend.Application.Commands.ArticleCategories;
public record DeleteArticleCategoryByIdCommand(int Id) : IRequest;