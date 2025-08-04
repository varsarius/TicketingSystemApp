using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingSystemBackend.Application.DTOs;
public record TicketDto(
    int Id,
    string Title,
    string Description,
    string Priority,
    string CategoryName,
    DateTime CreatedAt
);