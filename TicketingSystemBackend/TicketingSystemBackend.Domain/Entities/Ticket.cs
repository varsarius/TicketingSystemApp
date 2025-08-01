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
    public DateTime CreatedAt { get; private set;} = DateTime.Now;
    public DateTime UpdatedAt { get; private set;}
    public Priority Priority { get; set; }
    public List<TicketComment> TicketComments { get; set; } = [];
    [Required]
    public TicketCategory Category { get; set; } = null!;
    public List<string> AttachmentFilePaths { get; set; } = [];
    public List<File> Files { get; set; } = [];
}
