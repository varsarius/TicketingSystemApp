using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Domain.Enums;

namespace TicketingSystemBackend.Domain.Entities;

public class TicketCategory
{
    public int Id { get; set; }

    [Required]
    public string CategoryName { get; set; } = null!;
    public DateTime CreatedAt { get; } = DateTime.Now;
    public DateTime UpdatedAt {  get; set; }

    public List<Ticket> Tickets { get; set; } = [];
}
