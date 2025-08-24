using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystemBackend.Application.DTOs;
using TicketingSystemBackend.Application.Interfaces;
using TicketingSystemBackend.Domain.Enums;
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

    public async Task<List<TicketDto>> GetAllSortFilterAsync(string? sortBy = null, string? sortOrder = null, int? categoryId = null, string? status = null, string? priority = null, Guid? userId = null)
    {
        var query = _context.Tickets
            .Include(t => t.TicketCategory)
            .AsQueryable();

        // Filtering
        if (categoryId.HasValue)
            query = query.Where(t => t.TicketCategoryId == categoryId.Value);

        if (!string.IsNullOrEmpty(status) &&
            Enum.TryParse<Status>(status, true, out var statusEnum))
        {
            query = query.Where(t => t.Status == statusEnum);
        }

        if (!string.IsNullOrEmpty(priority) &&
            Enum.TryParse<Priority>(priority, true, out var priorityEnum))
        {
            query = query.Where(t => t.Priority == priorityEnum);
        }

        if (userId.HasValue)
        {
            query = query.Where(t => t.UserId == userId.Value);
        }

        // Sorting
        if (!string.IsNullOrEmpty(sortBy))
        {
            bool descending = sortOrder?.ToLower() == "desc";

            query = sortBy.ToLower() switch
            {
                "priority" => descending
                    ? query.OrderByDescending(t => t.Priority)
                    : query.OrderBy(t => t.Priority),

                "createdat" or "time" => descending
                    ? query.OrderByDescending(t => t.CreatedAt)
                    : query.OrderBy(t => t.CreatedAt),

                _ => query.OrderBy(t => t.Id) // default
            };
        }

        // Projection to DTO
        return await query
            .Select(t => new TicketDto(
                t.Id,
                t.UserId,
                _context.Users
                    .Where(u => u.Id == t.UserId)
                    .Select(u => u.UserName)
                    .FirstOrDefault() ?? string.Empty,
                t.AgentId,
                t.AgentId != null
                    ? _context.Users
                        .Where(u => u.Id == t.AgentId)
                        .Select(u => u.UserName)
                        .FirstOrDefault() ?? string.Empty
                    : string.Empty,
                t.TicketCategoryId,
                t.Title,
                t.Description,
                t.TicketCategory.CategoryName,
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