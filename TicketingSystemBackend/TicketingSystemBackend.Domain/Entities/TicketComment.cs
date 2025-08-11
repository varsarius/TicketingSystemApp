using System.ComponentModel.DataAnnotations;

namespace TicketingSystemBackend.Domain.Entities;

public class TicketComment
{
    public int Id { get; set; }
    public int TicketId { get; set; }

    [Required]
    public string Description { get; set; } = null!;
    public DateTime CreatedAt { get; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; }
}