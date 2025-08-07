using MediatR;

namespace TicketingSystemBackend.Application.Commands.Tickets;

public record DeleteTicketByIdCommand(int Id) : IRequest;

