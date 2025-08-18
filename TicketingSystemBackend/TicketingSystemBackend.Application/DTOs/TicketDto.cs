using System.ComponentModel.DataAnnotations;
using TicketingSystemBackend.Domain.Enums;

namespace TicketingSystemBackend.Application.DTOs;
public record TicketDto(
    int Id,
    Guid UserId,
    string UserName,
    Guid? AgentId,
    string AgentName,
    int TicketCategoryId,
    [Required] string Title,
    [Required] string Description,
    string TicketCategoryName,
    Priority Priority,
    Status Status,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);