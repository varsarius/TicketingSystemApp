namespace TicketingSystemFrontend.Client.Requests;

public class ArticleUpdateRequest
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ArticleCategoryId { get; set; }
    public int Id { get; set; }
}
