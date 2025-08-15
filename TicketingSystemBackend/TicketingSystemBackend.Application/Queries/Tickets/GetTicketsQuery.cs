using MediatR;
using TicketingSystemBackend.Application.DTOs;
using TicketingSystemBackend.Domain.Entities;

namespace TicketingSystemBackend.Application.Queries.Tickets;

public record GetTicketsQuery : IRequest<List<TicketDto>>;
