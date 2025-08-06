namespace TicketingSystemFrontend.Client.Requests;

public class ArticleUpdateRequest
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int ArticleCategoryId { get; set; }
    public int Id { get; set; }
}
