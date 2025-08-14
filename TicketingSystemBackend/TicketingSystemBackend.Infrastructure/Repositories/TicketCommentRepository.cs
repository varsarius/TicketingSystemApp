using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Application.Interfaces;
using TicketingSystemBackend.Domain.Entities;
using TicketingSystemBackend.Infrastructure.Data;

namespace TicketingSystemBackend.Infrastructure.Repositories;
public class TicketCommentRepository : ITicketCommentRepository
{
    private readonly AppDbContext _context;

    public TicketCommentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(TicketComment entity)
    {
        _context.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteByIdAsync(int id)
    {
        var ticketComment = await GetByIdAsync(id);
        if (ticketComment is not null)
        {
            _context.Remove(ticketComment);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<TicketComment>> GetAllAsync()
    {
        return await _context.TicketComments.ToListAsync();
    }

    public async Task<TicketComment> GetByIdAsync(int id)
    {
        return await _context.TicketComments.FirstOrDefaultAsync(tc => tc.Id == id)
                    ?? throw new Exception($"Ticket category with id {id} not found.");
    }

    public async Task UpdateAsync(TicketComment entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
}
