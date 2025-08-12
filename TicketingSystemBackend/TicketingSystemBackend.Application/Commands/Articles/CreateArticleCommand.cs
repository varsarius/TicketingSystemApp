using MediatR;
using System.ComponentModel.DataAnnotations;

namespace TicketingSystemBackend.Application.Commands.Articles;

public record CreateArticleCommand
(
    [Required] string Title,
    [Required] string Description,
    int ArticleCategoryId,
    Guid UserId
) : IRequest;
