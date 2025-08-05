using MediatR;
using System.ComponentModel.DataAnnotations;

namespace TicketingSystemBackend.Api.Commands.Articles;

public record CreateArticleCommand
(
    [Required] string Title,
    [Required] string Description,
    int ArticleCategoryId,
    int UserId
) : IRequest;
