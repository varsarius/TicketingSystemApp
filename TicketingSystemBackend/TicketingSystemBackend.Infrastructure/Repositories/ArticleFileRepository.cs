using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Application.Interfaces;
using TicketingSystemBackend.Infrastructure.Data;
using TicketingSystemBackend.Domain.Entities;

namespace TicketingSystemBackend.Infrastructure.Repositories;
public class ArticleFileRepository : IArticleFileRepository
{
    private readonly AppDbContext _context;
    public ArticleFileRepository(AppDbContext context)
    {
        _context = context;
    }

    // Add a new File entity
    public async Task AddAsync(Domain.Entities.File file)
    {
        await _context.Files.AddAsync(file);
    }

    // Save all pending changes
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
