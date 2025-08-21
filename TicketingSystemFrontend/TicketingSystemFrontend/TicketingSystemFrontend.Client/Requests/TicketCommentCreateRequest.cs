using System.ComponentModel.DataAnnotations;

namespace TicketingSystemFrontend.Client.Requests;

public class TicketCommentCreateRequest
{
    [Required]
    public string Description { get; set; } = string.Empty;
    public Guid UserId { get; set; } // Added for authentication
    public int TicketId { get; set; } // Added to link to a ticket
}