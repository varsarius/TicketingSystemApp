using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystemBackend.Domain.Entities;
using TicketingSystemBackend.Infrastructure.Data;

namespace TicketingSystemBackend.Infrastructure.Seeds;

public static class ArticleSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        // Ensure ArticleCategories exist
        var categories = await context.ArticleCategories.ToListAsync();
        if (!categories.Any())
        {
            await ArticleCategorySeeder.SeedAsync(context);
            categories = await context.ArticleCategories.ToListAsync();
        }

        // Generate 50 unique Articles
        var articles = new List<Article>();
        var random = new Random();

        for (int i = 1; i <= 50; i++)
        {
            var category = categories[random.Next(categories.Count)]; // Random category
            var article = new Article
            {
                ArticleCategoryId = category.Id,
                UserId = Guid.NewGuid(), //WARNING: this Guid is not tied to any real user! This may cause errors
                Title = $"Article Title {i}",
                Description = $"Description for article {i}: This is a sample article related to {category.CategoryName}.",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null
            };

            // Check if article with same title exists
            bool exists = await context.Articles.AnyAsync(a => a.Title == article.Title);
            if (!exists)
            {
                articles.Add(article);
            }
        }

        // Add and save articles
        context.Articles.AddRange(articles);
        await context.SaveChangesAsync();
    }
}