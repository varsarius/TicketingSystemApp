using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Domain.Enums;

namespace TicketingSystemBackend.Domain.Entities;

public class Ticket
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int AgentId { get; set; }
    public int CategoryId { get; set; }

    [Required]
    public string Title { get; set; } = null!;
    [Required]
    public string Description { get; set; } = null!;
    public Priority Priority { get; set; }
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set;}

    public List<TicketComment> TicketComments { get; set; } = [];
    [Required]
    public TicketCategory Category { get; set; } = null!;
    public List<File> Files { get; set; } = [];
}
