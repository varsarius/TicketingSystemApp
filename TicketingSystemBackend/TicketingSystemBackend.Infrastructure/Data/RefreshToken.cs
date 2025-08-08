using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingSystemBackend.Infrastructure.Data;
public class RefreshToken
{
    public int Id { get; set; }

    public string Token { get; set; } = null!;        

    public DateTime Expires { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; }

    // FK to User
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; } = default!;
}

