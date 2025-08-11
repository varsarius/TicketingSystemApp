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
        var ticketComment = GetByIdAsync(id);
        if (ticketComment is not null)
        {
            _context.Remove(ticketComment);
            await _context.SaveChangesAsync();
        }
    }

    public Task<List<TicketComment>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<TicketComment> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(TicketComment entity)
    {
        throw new NotImplementedException();
    }
}
