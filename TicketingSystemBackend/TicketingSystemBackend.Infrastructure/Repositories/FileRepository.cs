using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Application.Interfaces;
using TicketingSystemBackend.Infrastructure.Data;

namespace TicketingSystemBackend.Infrastructure.Repositories;
public class FileRepository : IFileRepository
{
    private readonly AppDbContext _context;

    public FileRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Domain.Entities.File file, CancellationToken cancellationToken = default)
    {
        await _context.Files.AddAsync(file, cancellationToken);
    }

    public Task DeleteAsync(Domain.Entities.File file, CancellationToken cancellationToken = default)
    {
        _context.Files.Remove(file);
        return Task.CompletedTask;
    }

    public async Task<Domain.Entities.File?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Files
            .FirstOrDefaultAsync(f => f.Id == id, cancellationToken);
    }

    public async Task<Domain.Entities.File?> GetFileByIdAndArticleIdAsync(int fileId, int articleId, CancellationToken cancellationToken = default)
    {
        return await _context.Files
            .Include(f => f.Articles)
            .FirstOrDefaultAsync(f => f.Id == fileId && f.Articles.Any(a => a.Id == articleId), cancellationToken);
    }

    public async Task<Domain.Entities.File?> GetFileByIdAndTicketIdAsync(int fileId, int ticketId, CancellationToken cancellationToken = default)
    {
        return await _context.Files
            .Include(f => f.Tickets)
            .FirstOrDefaultAsync(f => f.Id == fileId && f.Tickets.Any(t => t.Id == ticketId), cancellationToken);
    }

    public async Task<List<Domain.Entities.File>> GetFilesByArticleIdAsync(int articleId, CancellationToken cancellationToken = default)
    {
        return await _context.Files
            .Where(f => f.Articles.Any(a => a.Id == articleId))
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Domain.Entities.File>> GetFilesByTicketIdAsync(int ticketId, CancellationToken cancellationToken = default)
    {
        return await _context.Files
            .Where(f => f.Tickets.Any(t => t.Id == ticketId))
            .ToListAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
