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
public class TicketRepository : ITicketRepository
{
    private readonly AppDbContext _context;
    public TicketRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task CreateAsync(Ticket ticket)
    {
        await _context.AddAsync(ticket);
        await _context.SaveChangesAsync();
    }
    public async Task<Ticket> GetByIdAsync(int id)
    {
        var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == id)
                    ?? throw new Exception($"Ticket with id {id} not found.");
        return ticket;
    }
    public async Task<Ticket> GetByTitleAsync(string title)
    {
        var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Title == title)
                    ?? throw new Exception($"Ticket with title {title} not found.");
        return ticket;
    }
    public async Task<List<Ticket>> GetAllAsync()
    {
        var tickets = await _context.Tickets.ToListAsync();
        return tickets;
    }
    public async Task UpdateAsync(Ticket ticket)
    {
        _context.Entry(ticket).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
    public async Task DeleteByIdAsync(int id)
    {
        var ticket = GetByIdAsync(id);
        if (ticket is not null)
        {
            _context.Remove(ticket);
            await _context.SaveChangesAsync();
        }

    }
}
