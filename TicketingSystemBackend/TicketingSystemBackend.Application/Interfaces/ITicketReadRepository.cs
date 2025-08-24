using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Application.DTOs;

namespace TicketingSystemBackend.Application.Interfaces;
public interface ITicketReadRepository : IReadRepository<TicketDto>
{
    Task<List<TicketDto>> GetAllSortFilterAsync(string? sortBy = null,
        string? sortOrder = null,
        int? categoryId = null,
        string? status = null,
        string? priority = null,
        Guid? userId = null);
}
