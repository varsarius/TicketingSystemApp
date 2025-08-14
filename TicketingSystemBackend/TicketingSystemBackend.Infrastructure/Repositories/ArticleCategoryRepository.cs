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
public class ArticleCategoryRepository : IArticleCategoryRepository
{
    private readonly AppDbContext _context;

    public ArticleCategoryRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task CreateAsync(ArticleCategory articleCategory)
    {
        _context.Add(articleCategory);
        await _context.SaveChangesAsync();
    }
    public async Task<ArticleCategory> GetByIdAsync(int id)
    { 
        return await _context.ArticleCategories.FirstOrDefaultAsync(c => c.Id == id)
                            ?? throw new Exception($"Article category with id {id} not found.");
    }
    public async Task<ArticleCategory> GetByCategoryNameAsync(string categoryName)
    {
        return await _context.ArticleCategories.FirstOrDefaultAsync(c => c.CategoryName == categoryName)
                            ?? throw new Exception($"Article category with name {categoryName} not found");
    }
    public async Task<List<ArticleCategory>> GetAllAsync()
    {
        return await _context.ArticleCategories
            .ToListAsync();
    }
    public async Task UpdateAsync(ArticleCategory articleCategory)
    {
        _context.Entry(articleCategory).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
    public async Task DeleteByIdAsync(int id)
    {
        var articleCategory = await GetByIdAsync(id);
        if (articleCategory is not null)
        {
            _context.Remove(articleCategory);
            await _context.SaveChangesAsync();
        }
    }
}
