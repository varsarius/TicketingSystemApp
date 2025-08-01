using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingSystemBackend.Domain.Entities;
public class File
{
    public int Id { get; set; }
    [Required]
    public string Path { get; set; } = null!;

    public List<Ticket> Tickets { get; set; } = [];
    public List<Article> Articles { get; set; } = [];
}
