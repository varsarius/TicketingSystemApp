namespace TicketingSystemFrontend.Client.DTOs;

public class ArticleDto
{
    public int Id { get; set; }

    public int ArticleCategoryId { get; set; }
    public string ArticleCategoryName { get; set; } = null!;

    public Guid UserId { get; set; }
    public string UserName { get; set; } = null!;

    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }


}
