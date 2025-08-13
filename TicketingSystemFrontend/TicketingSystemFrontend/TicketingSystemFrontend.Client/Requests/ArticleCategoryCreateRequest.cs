namespace TicketingSystemFrontend.Client.Requests
{
    public class ArticleCategoryCreateRequest
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }
}
