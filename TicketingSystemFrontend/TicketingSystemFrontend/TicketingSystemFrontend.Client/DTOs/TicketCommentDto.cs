using System.ComponentModel.DataAnnotations;

namespace TicketingSystemFrontend.Client.DTOs;

public class TicketCommentDto
{
    public int Id { get; set; }
    [Required]
    public string Description { get; set; } = null!;
    public Guid UserId { get; set; } // Added
    public int TicketId { get; set; } // Added
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; set; }
}