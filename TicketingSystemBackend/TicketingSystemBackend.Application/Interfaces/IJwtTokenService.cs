using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingSystemBackend.Application.Interfaces;
public interface IJwtTokenService
{
    string GenerateJwtToken(Guid userId, string email, string username);
}
