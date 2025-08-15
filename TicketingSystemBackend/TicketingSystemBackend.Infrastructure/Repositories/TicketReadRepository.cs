using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystemBackend.Application.DTOs;
using TicketingSystemBackend.Application.Interfaces;
using TicketingSystemBackend.Infrastructure.Data;

namespace TicketingSystemBackend.Infrastructure.Repositories;

public class TicketReadRepository : ITicketReadRepository
{
    private readonly AppDbContext _context;

    public TicketReadRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<TicketDto>> GetAllAsync()
    {
        return await _context.Tickets
            .Include(t => t.TicketCategory)
            .Select(t => new TicketDto(
                t.Id,
                t.UserId,
                _context.Users
                    .Where(u => u.Id == t.UserId)
                    .Select(u => u.UserName)
                    .FirstOrDefault() ?? string.Empty,
                t.AgentId,
                t.AgentId.HasValue
                    ? _context.Users
                        .Where(u => u.Id == t.AgentId.Value)
                        .Select(u => u.UserName)
                        .FirstOrDefault() ?? string.Empty
                    : null,
                t.TicketCategoryId,
                t.Title,
                t.Description,
                t.TicketCategory != null ? t.TicketCategory.CategoryName : string.Empty,
                t.Priority,
                t.Status,
                t.CreatedAt,
                t.UpdatedAt
            ))
            .ToListAsync();
    }

    public async Task<TicketDto?> GetByIdAsync(int id)
    {
        return await _context.Tickets
            .Include(t => t.TicketCategory)
            .Where(t => t.Id == id)
            .Select(t => new TicketDto(
                t.Id,
                t.UserId,
                _context.Users
                    .Where(u => u.Id == t.UserId)
                    .Select(u => u.UserName)
                    .FirstOrDefault() ?? string.Empty,
                t.AgentId,
                t.AgentId.HasValue
                    ? _context.Users
                        .Where(u => u.Id == t.AgentId.Value)
                        .Select(u => u.UserName)
                        .FirstOrDefault() ?? string.Empty
                    : null,
                t.TicketCategoryId,
                t.Title,
                t.Description,
                t.TicketCategory != null ? t.TicketCategory.CategoryName : string.Empty,
                t.Priority,
                t.Status,
                t.CreatedAt,
                t.UpdatedAt
            ))
            .FirstOrDefaultAsync();
    }
}