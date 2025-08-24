using MediatR;
using TicketingSystemBackend.Application.DTOs;
using TicketingSystemBackend.Domain.Entities;

namespace TicketingSystemBackend.Application.Queries.Tickets;

public record GetTicketsQuery(
    string? SortBy = null,
    string? SortOrder = null,
    int? CategoryId = null,
    string? Status = null,
    string? Priority = null,
    Guid? UserId = null
) : IRequest<List<TicketDto>>;
