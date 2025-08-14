namespace TicketingSystemFrontend.Client.Requests;

public class TicketCategoryCreateRequest
{
    public int Id { get; set; }
    public string CategoryName { get; set; } = string.Empty;
}
