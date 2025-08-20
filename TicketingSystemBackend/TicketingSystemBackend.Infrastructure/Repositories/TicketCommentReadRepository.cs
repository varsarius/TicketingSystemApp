using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Application.DTOs;
using TicketingSystemBackend.Application.Interfaces;
using TicketingSystemBackend.Domain.Entities;
using TicketingSystemBackend.Infrastructure.Data;

namespace TicketingSystemBackend.Infrastructure.Repositories;
public class TicketCommentReadRepository : ITicketcommentReadRepository
{
    private readonly AppDbContext _context;

    public TicketCommentReadRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<TicketCommentDto>> GetAllAsync()
    {
        return await _context.TicketComments
            .Include(tc => tc.Ticket)
            .Select(tc => new TicketCommentDto(
                tc.Id,
                tc.UserId,
                tc.TicketId,
                _context.Users
                    .Where(u => u.Id == tc.UserId)
                    .Select(u => u.UserName)
                    .FirstOrDefault() ?? string.Empty,
                tc.Description,
                tc.CreatedAt,
                tc.UpdatedAt
            ))
            .ToListAsync();
    }

    public async Task<List<TicketCommentDto?>> GetAllByTicketIdAsync(int ticketId)
    {
        return await _context.TicketComments
       .Where(tc => tc.TicketId == ticketId)
       .Select(tc => new TicketCommentDto(
           tc.Id,
           tc.UserId,
           tc.TicketId,
           _context.Users
               .Where(u => u.Id == tc.UserId)
               .Select(u => u.UserName)
               .FirstOrDefault() ?? string.Empty,
           tc.Description,
           tc.CreatedAt,
           tc.UpdatedAt
       ))
       .ToListAsync();
    }

    public async Task<TicketCommentDto?> GetByIdAsync(int id)
    {
        return await _context.TicketComments
            .Include(tc => tc.Ticket)
            .Where(tc => tc.Id == id)
            .Select(tc => new TicketCommentDto(
                tc.Id,
                tc.UserId,
                tc.TicketId,
                _context.Users
                    .Where(u => u.Id == tc.UserId)
                    .Select(u => u.UserName)
                    .FirstOrDefault() ?? string.Empty,
                tc.Description,
                tc.CreatedAt,
                tc.UpdatedAt
            ))
            .FirstOrDefaultAsync();
    }
}
