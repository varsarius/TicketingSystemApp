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
public class ArticleRepository : IArticleRepository
{
    private readonly AppDbContext _context;
    public ArticleRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task CreateAsync(Article article)
    {
        _context.Add(article);
        await _context.SaveChangesAsync();
    }
    public async Task<Article> GetByIdAsync(int id)
    {
        return await _context.Articles.FirstOrDefaultAsync(a => a.Id == id)
                    ?? throw new Exception($"Article with id {id} not found.");
    }
    public async Task<Article> GetByTitleAsync(string title)
    {
        return await _context.Articles.FirstOrDefaultAsync(a => a.Title == title)
                    ?? throw new Exception($"Article with title {title} not found.");
    }
    public async Task<List<Article>> GetAllAsync()
    {
        return await _context.Articles.ToListAsync();
    }
    public async Task UpdateAsync(Article article)
    {
        _context.Entry(article).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
    public async Task DeleteByIdAsync(int id)
    {
        var article = await GetByIdAsync(id);

        if (article is not null)
        {
            _context.Remove(article);
            await _context.SaveChangesAsync();
        }

    }
}
