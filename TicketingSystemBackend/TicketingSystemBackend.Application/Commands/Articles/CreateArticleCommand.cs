using MediatR;
using System.ComponentModel.DataAnnotations;

namespace TicketingSystemBackend.Application.Commands.Articles;

public record CreateArticleCommand
(
    [Required] string Title,
    [Required] string Description,
    int CategoryId,
    int UserId
) : IRequest;
