namespace TicketingSystemFrontend.Client.Requests
{
    public class ArticleCategoryUpdateRequest
    {
        public int Id { get; set; }

        public string CategoryName { get; set; } = string.Empty;
    }
}
