using System.ComponentModel.DataAnnotations;

namespace TicketingSystemFrontend.Client.DTOs;

public class TicketCommentDto
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public int TicketId { get; set; }

    [Required]
    public string Description { get; set; } = null!;
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; set; }

}