using MediatR;
using System.ComponentModel.DataAnnotations;

namespace TicketingSystemBackend.Application.Commands.ArticleCategories;

public record UpdateArticleCategoryCommand(
    int Id,
    [Required] string CategoryName
) : IRequest;