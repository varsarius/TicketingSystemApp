using System.ComponentModel.DataAnnotations;

namespace TicketingSystemBackend.Domain.Entities;

public class TicketComment
{
    public int Id { get; set; }
    public int TicketId { get; set; }

    [Required]
    public string Description { get; set; } = null!;
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public Ticket Ticket { get; set; } = null!;
}