using System.ComponentModel.DataAnnotations;

namespace TicketingSystemFrontend.Client.Requests;

public class ArticleCreateRequest
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int ArticleCategoryId { get; set; }
    public Guid UserId { get; set; }
}
