using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystemBackend.Domain.Entities;
using TicketingSystemBackend.Domain.Enums;
using TicketingSystemBackend.Infrastructure.Data;

namespace TicketingSystemBackend.Infrastructure.Seeds;

public static class TicketSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        // Ensure TicketCategories exist
        var categories = await context.TicketCategories.ToListAsync();
        if (categories.Count == 0)
        {
            await TicketCategorySeeder.SeedAsync(context);
            categories = await context.TicketCategories.ToListAsync();
        }

        // Possible Priority values
        var priorities = Enum.GetValues(typeof(Priority)).Cast<Priority>().ToList();
        var random = new Random();

        // Generate 50 unique Tickets
        var tickets = new List<Ticket>();
        for (int i = 1; i <= 50; i++)
        {
            var category = categories[random.Next(categories.Count)]; // Random category
            var ticket = new Ticket
            {
                //WARNING: these Guid's are not tied to any real user or agent! This may cause errors
                UserId = Guid.NewGuid(), // Random Guid for user
                AgentId = Guid.NewGuid(), // Random Guid for agent
                TicketCategoryId = category.Id,
                Title = $"Ticket {i} with Issue",
                Description = $"Description for ticket {i}: Issue related to {category.CategoryName}.",
                Priority = priorities[random.Next(priorities.Count)], // Random priority
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null
            };

            // Check if ticket with same title exists
            bool exists = await context.Tickets.AnyAsync(t => t.Title == ticket.Title);
            if (!exists)
            {
                tickets.Add(ticket);
            }
        }

        // Add and save tickets
        context.Tickets.AddRange(tickets);
        await context.SaveChangesAsync();
    }
}