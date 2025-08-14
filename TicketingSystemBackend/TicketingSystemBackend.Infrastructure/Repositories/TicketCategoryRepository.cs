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
public class TicketCategoryRepository : ITicketCategoryRepository
{
    private readonly AppDbContext _context;

    public TicketCategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(TicketCategory entity)
    {
        _context.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteByIdAsync(int id)
    {
        var ticketCategory = GetByIdAsync(id);
        if (ticketCategory is not null)
        {
            _context.Remove(ticketCategory);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<TicketCategory>> GetAllAsync()
    {
        return await _context.TicketCategories
            .ToListAsync();
    }

    public async Task<TicketCategory> GetByCategoryNameAsync(string categoryName)
    {
        return await _context.TicketCategories.FirstOrDefaultAsync(tc => tc.CategoryName == categoryName)
                    ?? throw new Exception($"Ticket category with name {categoryName} not found.");
    }

    public async Task<TicketCategory> GetByIdAsync(int id)
    { 
        return await _context.TicketCategories.FirstOrDefaultAsync(tc => tc.Id == id)
                    ?? throw new Exception($"Ticket category with id {id} not found.");
    }

    public async Task UpdateAsync(TicketCategory entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
}
