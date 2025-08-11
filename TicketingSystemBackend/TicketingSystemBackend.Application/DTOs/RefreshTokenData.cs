using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingSystemBackend.Application.DTOs;
public class RefreshTokenData
{
    public string Token { get; set; } = null!;
    public DateTime Expires { get; set; }
    public Guid UserId { get; set; }
}
