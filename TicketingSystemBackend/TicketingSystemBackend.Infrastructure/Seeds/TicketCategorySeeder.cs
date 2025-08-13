using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Domain.Entities;
using TicketingSystemBackend.Infrastructure.Data;

namespace TicketingSystemBackend.Infrastructure.Seeds;
public static class TicketCategorySeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        // Example categories
        var categories = new List<TicketCategory>
            {
                new() { CategoryName = "Hardware" },
                new() { CategoryName = "Software" },
                new() { CategoryName = "Services" }
            };

        foreach (var category in categories)
        {
            // Check if a category with the same name already exists
            bool exists = await context.TicketCategories
                .AnyAsync(t => t.CategoryName == category.CategoryName);

            if (!exists)
            {
                context.TicketCategories.Add(category);
            }
        }

        await context.SaveChangesAsync();
    }
}
