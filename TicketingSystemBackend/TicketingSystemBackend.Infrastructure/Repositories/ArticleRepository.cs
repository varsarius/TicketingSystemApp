using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Domain.Entities;
using TicketingSystemBackend.Infrastructure.Data;

namespace TicketingSystemBackend.Infrastructure.Repositories;
public class ArticleRepository
{
    private readonly AppDbContext _context;
    public ArticleRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task CreateAsync(Article article)
    {
        await _context.AddAsync(article);
        await _context.SaveChangesAsync();
    }
    public async Task<Article> GetByIdAsync(int id)
    {
        var article = await _context.Articles.FirstOrDefaultAsync(a => a.Id == id)
                    ?? throw new Exception($"Ticket with id {id} not found.");
        return article;
    }
    public async Task<Article> GetByTitleAsync(string title)
    {
        var article = await _context.Articles.FirstOrDefaultAsync(a => a.Title == title)
                    ?? throw new Exception($"Ticket with title {title} not found.");
        return article;
    }
    public async Task<List<Article>> GetAllAsync()
    {
        var articles = await _context.Articles.ToListAsync();
        return articles;
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
