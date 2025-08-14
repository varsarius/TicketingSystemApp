using System.ComponentModel.DataAnnotations;
using TicketingSystemFrontend.Client.Requests.Enums;

namespace TicketingSystemFrontend.Client.Requests;

public class TicketCreateRequest
{
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    public Priority Priority { get; set; }
    public Status Status { get; set; }
    public Guid UserId { get; set; }
    public Guid? AgentId { get; set; }
    public int TicketCategoryId { get; set; }
}
