using System.ComponentModel.DataAnnotations;

namespace TicketingSystemFrontend.Client.Requests.Commands.Articles;

public record UpdateArticleCommand(
    [Required] string Title,
    [Required] string Description,
    int ArticleCategoryId,
    int Id
);
