using System.ComponentModel.DataAnnotations;

namespace TicketingSystemBackend.Application.DTOs;

public record ArticleDto(
    int Id,
    int ArticleCategoryId,
    string ArticleCategoryName,
    Guid UserId,
    string UserName,
    [Required] string Title,
    [Required] string Description,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
