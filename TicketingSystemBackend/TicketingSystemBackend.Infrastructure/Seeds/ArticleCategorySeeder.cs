using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Domain.Entities;
using TicketingSystemBackend.Infrastructure.Data;

namespace TicketingSystemBackend.Infrastructure.Seeds;
public static class ArticleCategorySeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        // Example categories
        var categories = new List<ArticleCategory>
            {
                new ArticleCategory { CategoryName = "General" },
                new ArticleCategory { CategoryName = "Technical" },
                new ArticleCategory { CategoryName = "Billing" }
            };

        foreach (var category in categories)
        {
            // Check if a category with the same name already exists
            bool exists = await context.ArticleCategories
                .AnyAsync(c => c.CategoryName == category.CategoryName);

            if (!exists)
            {
                context.ArticleCategories.Add(category);
            }
        }

        await context.SaveChangesAsync();
    }
}
