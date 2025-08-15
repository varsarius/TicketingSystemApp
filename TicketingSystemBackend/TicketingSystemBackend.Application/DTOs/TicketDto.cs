using System.ComponentModel.DataAnnotations;
using TicketingSystemBackend.Domain.Enums;

namespace TicketingSystemBackend.Application.DTOs;
public record TicketDto(
    int Id,
    Guid UserId,
    Guid? AgentId,
    int TicketCategoryId,
    [Required] string Title,
    [Required] string Description,
    Priority Priority,
    Status Status,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);