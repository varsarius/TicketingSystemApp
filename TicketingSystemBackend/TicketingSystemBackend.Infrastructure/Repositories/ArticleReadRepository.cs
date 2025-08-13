using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Application.DTOs;
using TicketingSystemBackend.Application.Interfaces;
using TicketingSystemBackend.Infrastructure.Data;

namespace TicketingSystemBackend.Infrastructure.Repositories;

public class ArticleReadRepository : IArticleReadRepository
{

    private readonly AppDbContext _context;

    public ArticleReadRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ArticleDto>> GetAllAsync()
    {
        var articles = await _context.Articles
            .Include(a => a.ArticleCategory)
            .Select(a => new ArticleDto
            {
                Id = a.Id,
                ArticleCategoryId = a.ArticleCategoryId,
                ArticleCategoryName = a.ArticleCategory.CategoryName,
                UserId = a.UserId,
                UserName = _context.Users
                    .Where(u => u.Id == a.UserId)
                    .Select(u => u.UserName)
                    .FirstOrDefault() ?? string.Empty,
                Title = a.Title,
                Description = a.Description,
                CreatedAt = a.CreatedAt,
                UpdatedAt = a.UpdatedAt
            })
            .ToListAsync();



        return articles;
    }

    public async Task<ArticleDto?> GetByIdAsync(int id)
    {
        return await _context.Articles
            .Include(a => a.ArticleCategory)
            .Where(a => a.Id == id)
            .Select(a => new ArticleDto
            {
                Id = a.Id,
                ArticleCategoryId = a.ArticleCategoryId,
                ArticleCategoryName = a.ArticleCategory.CategoryName,
                UserId = a.UserId,
                UserName = _context.Users
                    .Where(u => u.Id == a.UserId)
                    .Select(u => u.UserName)
                    .FirstOrDefault() ?? string.Empty,
                Title = a.Title,
                Description = a.Description,
                CreatedAt = a.CreatedAt,
                UpdatedAt = a.UpdatedAt
            })
            .FirstOrDefaultAsync();
    }
}
