namespace TicketingSystemBackend.Api.Models;

public class TicketCreateRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Priority { get; set; }
    public string AssignedTo { get; set; }
}