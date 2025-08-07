using MediatR;

namespace TicketingSystemBackend.Application.Commands.Tickets;

public record DeleteTicketCommand(int Id) : IRequest;

