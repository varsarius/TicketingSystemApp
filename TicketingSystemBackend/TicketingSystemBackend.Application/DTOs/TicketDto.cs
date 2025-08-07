namespace TicketingSystemBackend.Application.DTOs;
public record TicketDto(
    int Id,
    string Title,
    string Description,
    string Priority,
    string CategoryName,
    DateTime CreatedAt
);