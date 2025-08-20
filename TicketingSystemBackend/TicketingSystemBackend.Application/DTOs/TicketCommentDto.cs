using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingSystemBackend.Application.DTOs;
public record TicketCommentDto(
    int Id,
    Guid UserId,
    int TicketId,
    string UserName,
    [property: Required] string Description,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);

