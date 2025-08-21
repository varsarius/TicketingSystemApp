using System.ComponentModel.DataAnnotations;

namespace TicketingSystemFrontend.Client.Requests;

public class ArticleCreateRequest
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ArticleCategoryId { get; set; }
    public Guid UserId { get; set; }
}
