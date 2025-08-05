using MediatR;
using System.ComponentModel.DataAnnotations;

namespace TicketingSystemBackend.Application.Commands.Articles;

public record UpdateArticleCommand
(
    [Required] string Title,
    [Required] string Description,
    int ArticleCategoryId,
    int Id
) : IRequest;

