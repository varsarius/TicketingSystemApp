using MediatR;
using TicketingSystemBackend.Domain.Entities;

namespace TicketingSystemBackend.Application.Queries.Tickets;

public record GetTicketByIdQuery(int Id) : IRequest<Ticket>;

