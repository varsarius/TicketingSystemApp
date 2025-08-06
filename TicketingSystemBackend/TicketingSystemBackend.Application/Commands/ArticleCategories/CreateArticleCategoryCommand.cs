using MediatR;
using System.ComponentModel.DataAnnotations;

namespace TicketingSystemBackend.Application.Commands.ArticleCategories;

public record CreateArticleCategoryCommand(
      [Required] string CategoryName
) : IRequest;