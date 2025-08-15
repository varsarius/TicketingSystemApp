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

    public async Task DeleteAsync(Domain.Entities.File file)
    {
        _context.Files.Remove(file);
        await SaveChangesAsync();
    }

    public async Task<Domain.Entities.File?> GetByIdAsync(int id)
    {
        return await _context.Files
            .FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<Domain.Entities.File?> GetFileByIdAndArticleIdAsync(int fileId, int articleId, CancellationToken cancellationToken)
    {
        return await _context.Files
            .Include(f => f.Articles)
            .FirstOrDefaultAsync(f => f.Id == fileId && f.Articles.Any(a => a.Id == articleId), cancellationToken);
    }

    public async Task<List<Domain.Entities.File>> GetFilesByArticleIdAsync(int articleId)
    {
        return await _context.Files
            .Where(f => f.Articles.Any(a => a.Id == articleId))
            .ToListAsync();
    }

    // Save all pending changes
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
