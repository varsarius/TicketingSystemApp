using System.ComponentModel.DataAnnotations;

namespace TicketingSystemFrontend.Client.Requests.Commands.Articles;

public record CreateArticleCommand(
    string Title,
    string Description,
    int ArticleCategoryId,
    Guid UserId
);
