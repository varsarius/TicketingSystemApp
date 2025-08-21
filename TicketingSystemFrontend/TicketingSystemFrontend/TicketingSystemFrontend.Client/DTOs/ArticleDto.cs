namespace TicketingSystemFrontend.Client.DTOs;

public class ArticleDto
{
    public int Id { get; set; }

    public int ArticleCategoryId { get; set; }
    public string ArticleCategoryName { get; set; } = string.Empty;

    public Guid UserId { get; set; }
    public string UserName { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }


}
