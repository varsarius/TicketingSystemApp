using System.ComponentModel.DataAnnotations;

namespace TicketingSystemFrontend.Client.Requests;

public class TicketCommentUpdateRequest
{
    public int Id { get; set; }
    [Required]

    public int ArticleId { get; set; }

    public string Description { get; set; } = string.Empty;
    public Guid UserId { get; set; } // Added for authentication
    public int TicketId { get; set; } // Added to link to a ticket
}