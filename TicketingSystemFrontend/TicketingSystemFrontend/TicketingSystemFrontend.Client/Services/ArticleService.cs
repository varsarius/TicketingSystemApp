using System.Net.Http;
using System.Net.Http.Json;
using TicketingSystemFrontend.Client.DTOs;
using TicketingSystemFrontend.Client.Requests;
using TicketingSystemFrontend.Client.Services.Interfaces;

namespace TicketingSystemFrontend.Client.Services;

public class ArticleService : IArticleService
{
    private readonly HttpClient _http;

    public ArticleService(HttpClient http)
    {
        _http = http;
    }
    public async Task CreateArticleAsync(ArticleCreateRequest request)
    {
        var response = await _http.PostAsJsonAsync("api/article", request);
        response.EnsureSuccessStatusCode();
    }

    //public async Task<List<ArticleDto>> GetAllArticlesAsync()
    //{
    //    var articles = await _http.GetFromJsonAsync<List<ArticleDto>>("api/article");
    //    return articles ?? new List<ArticleDto>();
    //}
    public async Task<ArticleDto?> GetArticleByIdAsync(int id)
    {
        //return await _http.GetFromJsonAsync<ArticleDto>($"api/article/{id}");
        return new ArticleDto
        {
            Id = 1,
            ArticleCategoryId = 101,
            ArticleCategoryName = "Guides",
            UserName = "Alice Johnson",
            Title = "Getting Started with the System",
            Description = "Learn how to navigate the ticketing system efficiently.",
            CreatedAt = DateTime.UtcNow.AddDays(-10),
            UpdatedAt = DateTime.UtcNow.AddDays(-2)
        };
    }


    public async Task<List<ArticleDto>> GetAllArticlesAsync()
    {
        // Simulate network delay
        await Task.Delay(300);

        return new List<ArticleDto>
    {
        new ArticleDto
        {
            Id = 1,
            ArticleCategoryId = 101,
            ArticleCategoryName = "Guides",
            UserName = "Alice Johnson",
            Title = "Getting Started with the System",
            Description = "Learn how to navigate the ticketing system efficiently.",
            CreatedAt = DateTime.UtcNow.AddDays(-10),
            UpdatedAt = DateTime.UtcNow.AddDays(-2)
        },
        new ArticleDto
        {
            Id = 2,
            ArticleCategoryId = 102,
            ArticleCategoryName = "Troubleshooting",
            UserName = "Bob Smith",
            Title = "Common Login Issues",
            Description = "A detailed guide to resolve frequent login problems.",
            CreatedAt = DateTime.UtcNow.AddDays(-7),
            UpdatedAt = DateTime.UtcNow.AddDays(-1)
        },
        new ArticleDto
        {
            Id = 3,
            ArticleCategoryId = 103,
            ArticleCategoryName = "Best Practices",
            UserName = "Carol Nguyen",
            Title = "How to Write Good Tickets",
            Description = "Tips for writing effective and clear tickets for faster resolution.",
            CreatedAt = DateTime.UtcNow.AddDays(-5),
            UpdatedAt = DateTime.UtcNow
        },
        new ArticleDto
        {
            Id = 1,
            ArticleCategoryId = 101,
            ArticleCategoryName = "Guides",
            UserName = "Alice Johnson",
            Title = "Getting Started with the System",
            Description = "Learn how to navigate the ticketing system efficiently.",
            CreatedAt = DateTime.UtcNow.AddDays(-10),
            UpdatedAt = DateTime.UtcNow.AddDays(-2)
        },
        new ArticleDto
        {
            Id = 2,
            ArticleCategoryId = 102,
            ArticleCategoryName = "Troubleshooting",
            UserName = "Bob Smith",
            Title = "Common Login Issues",
            Description = "A detailed guide to resolve frequent login problems.",
            CreatedAt = DateTime.UtcNow.AddDays(-7),
            UpdatedAt = DateTime.UtcNow.AddDays(-1)
        },
        new ArticleDto
        {
            Id = 3,
            ArticleCategoryId = 103,
            ArticleCategoryName = "Best Practices",
            UserName = "Carol Nguyen",
            Title = "How to Write Good Tickets",
            Description = "Tips for writing effective and clear tickets for faster resolution.",
            CreatedAt = DateTime.UtcNow.AddDays(-5),
            UpdatedAt = DateTime.UtcNow
        },
        new ArticleDto
        {
            Id = 1,
            ArticleCategoryId = 101,
            ArticleCategoryName = "Guides",
            UserName = "Alice Johnson",
            Title = "Getting Started with the System",
            Description = "Learn how to navigate the ticketing system efficiently.",
            CreatedAt = DateTime.UtcNow.AddDays(-10),
            UpdatedAt = DateTime.UtcNow.AddDays(-2)
        },
        new ArticleDto
        {
            Id = 2,
            ArticleCategoryId = 102,
            ArticleCategoryName = "Troubleshooting",
            UserName = "Bob Smith",
            Title = "Common Login Issues",
            Description = "A detailed guide to resolve frequent login problems.",
            CreatedAt = DateTime.UtcNow.AddDays(-7),
            UpdatedAt = DateTime.UtcNow.AddDays(-1)
        },
        new ArticleDto
        {
            Id = 3,
            ArticleCategoryId = 103,
            ArticleCategoryName = "Best Practices",
            UserName = "Carol Nguyen",
            Title = "How to Write Good Tickets",
            Description = "Tips for writing effective and ssdahgd jhvsdhfbfkjs sd gkssadb ksjlb kjlfg klsjb dkljfgd fkjlg dklj gkfgdkljfg lkdjfg f lkasdhgk ljhg lkfg hklfgh lkdfgh lkd kfgh kldhg lkdhgl kehg hg hg lhg lkehg klhgehgepaohg  heophg hg hg clear tickets for faster resolution.",
            CreatedAt = DateTime.UtcNow.AddDays(-5),
            UpdatedAt = DateTime.UtcNow
        }
    };
    }

    public async Task<bool> DeleteArticleAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/articles/{id}");

        if (!response.IsSuccessStatusCode)
        {
            // You could throw or just return false
            // throw new Exception($"Failed to delete article with ID {id}. Status: {response.StatusCode}");
            return false;
        }

        return true;
    }
    public async Task UpdateArticleAsync(int id, ArticleCreateRequest request)
    {
        var response = await _http.PutAsJsonAsync($"api/articles/{id}", request);
        response.EnsureSuccessStatusCode();
    }

}
