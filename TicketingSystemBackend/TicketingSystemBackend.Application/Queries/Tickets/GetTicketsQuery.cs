using MediatR;
using TicketingSystemBackend.Domain.Entities;

namespace TicketingSystemBackend.Application.Queries.Tickets;

public record GetTicketsQuery : IRequest<List<Ticket>>;
